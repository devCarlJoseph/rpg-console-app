using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;
using ConsoleRPG.Services;
using ConsoleRPG.View;

namespace ConsoleRPG.Core
{
    
    public class GameEngine
    {
        private readonly Player _player;
        private readonly ShopService _shop = new();
        private readonly QuestManager _quests = new();

        
        // Initializes the engine with the active player state.
        public GameEngine(Player player)
        {
            _player = player;
        }


        // Main game loop that routes menu selections until exit.
        public void Run()
        {
            ConsoleUi.SafeClear();
            SystemMessageService.System($"Welcome, {_player.Name}. The System is now online.");
            _quests.AssignDailyQuest(_player);

            while (true)
            {
                var choice = MainMenuView.Show(_player);
                switch (choice)
                {
                    case MainMenuView.MainMenuChoice.Status:
                        PlayerStatusView.Show(_player);
                        break;

                    case MainMenuView.MainMenuChoice.Inventory:
                        InventoryView.Show(_player);
                        break;

                    case MainMenuView.MainMenuChoice.Shop:
                        ShopView.Show(_shop, _player);
                        break;

                    case MainMenuView.MainMenuChoice.Quests:
                        QuestBoardView.Show(_quests, _player);
                        break;

                    case MainMenuView.MainMenuChoice.EnterDungeon:
                        EnterDungeon();
                        break;

                    case MainMenuView.MainMenuChoice.Save:
                        SaveLoadService.Save(_player);
                        SystemMessageService.System("Game saved to Data/savegame.json");
                        break;

                    case MainMenuView.MainMenuChoice.Exit:
                        SystemMessageService.System("Exiting to title screen...");
                        return;

                    default:
                        ConsoleUi.ErrorMessage("Invalid option.");
                        break;
                }
            }
        }

        // Drives the dungeon selection, combat waves, and reward distribution.
        private void EnterDungeon()
        {
            var db = DungeonDataService.Load();
            var chosen = DungeonSelectView.ChooseDungeon(db);
            if (chosen is null)
            {
                return;
            }
            var raid = DungeonRaidService.BuildRaid(chosen);

            var gate = new Dungeon(raid.Name, raid.RecommendedLevel, raid.Waves, raid.Boss);
            SystemMessageService.System($"Gate selected: {gate.Name} (Recommended Lv {gate.RecommendedLevel})");

            _quests.AssignDungeonQuests(_player, raid.QuestIds);

            while (_player.IsAlive)
            {
                var enemy = gate.DequeueNextEnemy();
                if (enemy is null)
                {
                    break;
                }

                SystemMessageService.Warning($"Wave encounter: {enemy.Name}");
                RunCombat(enemy);
            }

            if (_player.IsAlive)
            {
                SystemMessageService.Warning($"Boss encounter: {gate.Boss.Name}!");
                RunCombat(gate.Boss);
                if (_player.IsAlive)
                {
                    gate.ClearDungeon();

                    _player.GainXP(Math.Max(0, raid.Rewards.XP));
                    _player.AddGold(Math.Max(0, raid.Rewards.Gold));

                    var itemsById = ItemDataService.LoadItemsById();
                    foreach (var itemId in raid.Rewards.ItemIds)
                    {
                        if (itemsById.TryGetValue(itemId, out var item))
                        {
                            _player.Inventory.Add(item);
                        }
                    }

                    _quests.NotifyDungeonCleared(_player, raid.DungeonId);

                    SystemMessageService.Success($"Dungeon rewards: +{raid.Rewards.XP} XP, +{raid.Rewards.Gold}g");
                }
            }

            if (!_player.IsAlive)
            {
                SystemMessageService.Error("You were defeated... The System restores your health.");
                _player.Heal(_player.MaxHP);
            }
        }

        // Runs a single encounter until someone retreats or is defeated.
        private void RunCombat(Enemy enemy)
        {
            string? log = null;
            while (_player.IsAlive && enemy.IsAlive)
            {
                CombatView.RenderEncounter(_player, enemy, $"Wave Encounter", log);
                var action = CombatView.PromptAction(_player);

                if (action == CombatView.CombatAction.Retreat)
                {
                    return;
                }

                if (action == CombatView.CombatAction.UseItem)
                {
                    var idx = CombatView.PromptItemIndex(_player);
                    if (idx is null)
                    {
                        log = "Item use cancelled.";
                        continue;
                    }

                    var itemIndex = idx.Value;
                    if (itemIndex < 0 || itemIndex >= _player.Inventory.Items.Count)
                    {
                        log = "Invalid item selection.";
                        continue;
                    }

                    var item = _player.Inventory.Items[itemIndex];
                    var ok = _player.Inventory.UseItem(itemIndex, _player);
                    if (ok)
                    {
                        ItemUseView.Show("Item Used", _player, item);
                        log = $"Used {item.Name}.";
                    }
                    else
                    {
                        log = "Invalid item selection.";
                    }
                }
                else if (action == CombatView.CombatAction.Regenerate)
                {
                    var hp = Math.Max(1, 3 + _player.Intelligence / 3);
                    var mp = Math.Max(1, 2 + _player.Intelligence / 4);
                    _player.Heal(hp);
                    _player.RestoreMana(mp);
                    log = $"You focused and regenerated +{hp} HP, +{mp} MP.";
                }
                else if (action == CombatView.CombatAction.Attack)
                {
                    var attackType = CombatView.PromptAttackType(_player);
                    if (attackType == CombatView.AttackType.Back)
                    {
                        log = "Attack cancelled.";
                        continue;
                    }

                    if (attackType == CombatView.AttackType.Physical)
                    {
                        var playerResult = CombatService.CalculatePlayerPhysicalDamage(_player, enemy);
                        log = ApplyDamageAndDescribe(playerResult, enemy);
                    }
                    else if (attackType == CombatView.AttackType.Magic)
                    {
                        const int mpCost = 5;
                        if (_player.MP < mpCost)
                        {
                            log = "Not enough MP for magic attack.";
                            continue;
                        }

                        _player.ConsumeMana(mpCost);
                        var raw = Math.Max(1, _player.Intelligence * 2);
                        enemy.TakeDamage(raw);
                        log = $"You cast a magic blast for {raw} damage. (-{mpCost} MP)";
                    }
                    else
                    {
                        var skill = CombatView.PromptSkill(_player);
                        if (skill is null)
                        {
                            log = "Skill cancelled.";
                            continue;
                        }

                        if (_player.MP < skill.ManaCost)
                        {
                            log = $"Not enough MP to use {skill.Name}.";
                            continue;
                        }

                        var beforeHp = enemy.HP;
                        var beforeMp = _player.MP;
                        skill.Execute(_player, enemy);
                        var dealt = Math.Max(0, beforeHp - enemy.HP);
                        var spent = Math.Max(0, beforeMp - _player.MP);
                        log = $"Used {skill.Name}. {enemy.Name} took {dealt} damage. (-{spent} MP)";
                    }
                }
                else
                {
                    log = "Invalid action.";
                    continue;
                }

                if (!enemy.IsAlive)
                {
                    var xp = 25 + enemy.Level * 10;
                    var gold = 10 + enemy.Level * 3;

                    _player.GainXP(xp);
                    _player.AddGold(gold);

                    CombatOutcomeView.ShowVictory(_player, enemy, xp, gold, log);
                    _quests.NotifyEnemyDefeated(_player, enemy);

                    OfferExtraction(enemy);
                    return;
                }

                var enemyResult = CombatService.CalculateEnemyPhysicalDamage(enemy, _player);
                var enemyLine = ApplyDamageAndDescribe(enemyResult, _player);
                log = string.IsNullOrWhiteSpace(log) ? enemyLine : (log + "  |  " + enemyLine);
            }

            if (!_player.IsAlive)
            {
                CombatOutcomeView.ShowDefeat(_player, enemy);
            }
        }

        // Applies computed combat damage and returns a short description string.
        private static string ApplyDamageAndDescribe(CombatResult result, IEntity defender)
        {
            if (!result.IsHit)
            {
                return $"{result.Source} missed. (Evaded)";
            }

            defender.HP -= result.DamageDealt;
            if (defender.HP < 0)
            {
                defender.HP = 0;
            }

            var critText = result.IsCritical ? " (CRIT!)" : string.Empty;
            return $"{result.Source} deals {result.DamageDealt} damage{critText}.";
        }

        // Prompts the player to attempt a shadow extraction after victory.
        private void OfferExtraction(Enemy defeatedEnemy)
        {
            const int maxAttempts = 3;
            if (!CombatOutcomeView.PromptExtraction())
                return;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                if (ShadowExtractionService.TryExtract(_player, defeatedEnemy, out var shadow) && shadow is not null)
                {
                    AddShadowToPlayer(shadow);
                    CombatOutcomeView.ShowExtractionResult(true, attempt, maxAttempts, shadow);
                    return;
                }

                CombatOutcomeView.ShowExtractionResult(false, attempt, maxAttempts, null);
            }
        }

        // Adds a new shadow to the player and grants its combat skill if missing.
        private void AddShadowToPlayer(Shadow shadow)
        {
            _player.Shadows.Add(shadow);

            var skillName = $"Shadow: {shadow.Name}";
            var alreadyHas = _player.ActiveSkills
                .OfType<ActiveSkill>()
                .Any(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (!alreadyHas)
            {
                _player.ActiveSkills.Add(new ShadowStrikeSkill(shadow));
            }
        }

    }
}

