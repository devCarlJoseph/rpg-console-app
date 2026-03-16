using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.Core
{
    /// <summary>
    /// High-level game loop for the console RPG.
    /// 
    /// This first version focuses on:
    /// - A simple command parser (type commands like "help", "stats", "hunt", "exit").
    /// - A very small combat loop when you choose "hunt".
    /// - Clear, guided console output so you always know what to type next.
    /// 
    /// As you implement more systems (quests, shadows, gates),
    /// you can extend this class with new commands and menus.
    /// </summary>
    public class GameEngine
    {
        private readonly Player _player;
        private readonly ShopService _shop = new();
        private readonly QuestManager _quests = new();
        private Enemy? _lastDefeatedEnemy;

        public GameEngine(Player player)
        {
            _player = player;
        }

        public void Run()
        {
            SafeClear();
            SystemMessageService.System($"Welcome, {_player.Name}. The System is now online.");
            _quests.AssignDailyQuest(_player);

            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Main Menu ===");
                Console.ResetColor();
                Console.WriteLine("  1. Character Status");
                Console.WriteLine("  2. Inventory");
                Console.WriteLine("  3. Shop");
                Console.WriteLine("  4. Quests");
                Console.WriteLine("  5. Enter Dungeon");
                Console.WriteLine("  6. Save Game");
                Console.WriteLine("  7. Exit Game");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nSelect: ");
                Console.ResetColor();

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowStats();
                        break;

                    case "2":
                        InventoryMenu();
                        break;

                    case "3":
                        ShopMenu();
                        break;

                    case "4":
                        QuestsMenu();
                        break;

                    case "5":
                        EnterDungeon();
                        break;

                    case "6":
                        SaveLoadService.Save(_player);
                        SystemMessageService.System("Game saved to Data/savegame.json");
                        break;

                    case "7":
                        SystemMessageService.System("Exiting to title screen...");
                        return;

                    default:
                        SystemMessageService.Hint("Invalid option.");
                        break;
                }
            }
        }

        private static void SafeClear()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
            }
        }

        private void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== Player Status ===");
            Console.ResetColor();
            Console.WriteLine($"  Name : {_player.Name}");
            Console.WriteLine($"  Level: {_player.Level} (XP: {_player.XP})");
            Console.WriteLine($"  Gold : {_player.Gold}");
            Console.WriteLine($"  HP   : {Bar(_player.HP, _player.MaxHP)} {_player.HP}/{_player.MaxHP}");
            Console.WriteLine($"  MP   : {Bar(_player.MP, _player.MaxMP)} {_player.MP}/{_player.MaxMP}");
            Console.WriteLine($"  STR  : {_player.Strength}");
            Console.WriteLine($"  AGI  : {_player.Agility}");
            Console.WriteLine($"  INT  : {_player.Intelligence}");
            Console.WriteLine($"  DEF  : {_player.Defense}");
            Console.WriteLine($"  Shadows: {_player.Shadows.Count}");
        }

        private void InventoryMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Inventory ===");
                if (_player.Inventory.Items.Count == 0)
                {
                    Console.WriteLine("  (empty)");
                }
                else
                {
                    for (int i = 0; i < _player.Inventory.Items.Count; i++)
                    {
                        var item = _player.Inventory.Items[i];
                        Console.WriteLine($"  {i + 1}. {item.Name} (Value: {item.Value}g)");
                    }
                }

                Console.WriteLine("\n  U <#>  - Use item");
                Console.WriteLine("  B      - Back");
                Console.Write("> ");

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (input.StartsWith("u", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2 || !int.TryParse(parts[1], out var idx))
                    {
                        SystemMessageService.Hint("Usage: U <#>");
                        continue;
                    }

                    if (!_player.Inventory.UseItem(idx - 1, _player))
                    {
                        SystemMessageService.Hint("Invalid item number.");
                        continue;
                    }

                    SystemMessageService.Success("Item used.");
                }
            }
        }

        private void ShopMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Shop ===");
                Console.WriteLine($"Gold: {_player.Gold}");
                if (_shop.Stock.Count == 0)
                {
                    Console.WriteLine("  (sold out)");
                }
                else
                {
                    for (int i = 0; i < _shop.Stock.Count; i++)
                    {
                        var item = _shop.Stock[i];
                        Console.WriteLine($"  {i + 1}. {item.Name} - {item.Value}g");
                    }
                }

                Console.WriteLine("\n  B      - Back");
                Console.WriteLine("  Buy <#> - Buy item");
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (input.StartsWith("buy", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2 || !int.TryParse(parts[1], out var idx))
                    {
                        SystemMessageService.Hint("Usage: Buy <#>");
                        continue;
                    }

                    var ok = _shop.TryBuy(_player, idx - 1);
                    if (!ok)
                    {
                        SystemMessageService.Warning("Purchase failed (invalid item or not enough gold).");
                        continue;
                    }

                    SystemMessageService.Success("Purchased!");
                }
            }
        }

        private void QuestsMenu()
        {
            Console.WriteLine("\n=== Quests ===");
            if (_player.ActiveQuests.Count == 0)
            {
                Console.WriteLine("  (no active quests)");
            }
            else
            {
                foreach (var q in _player.ActiveQuests)
                {
                    Console.WriteLine($"  - {q.Title}: {q.Description}");
                }
            }

            Console.WriteLine("\nCompleted:");
            if (_player.CompletedQuests.Count == 0)
            {
                Console.WriteLine("  (none)");
            }
            else
            {
                foreach (var q in _player.CompletedQuests)
                {
                    Console.WriteLine($"  - {q.Title}");
                }
            }
        }

        private void EnterDungeon()
        {
            var gate = GateGenerationService.Generate(_player);
            SystemMessageService.System($"A Gate has appeared: {gate.Name} (Recommended Lv {gate.RecommendedLevel})");

            // Fight through waves
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

            // Boss
            if (_player.IsAlive)
            {
                SystemMessageService.Warning($"Boss encounter: {gate.Boss.Name}!");
                RunCombat(gate.Boss);
                if (_player.IsAlive)
                {
                    gate.ClearDungeon();
                }
            }

            if (!_player.IsAlive)
            {
                SystemMessageService.Error("You were defeated... The System restores your health.");
                _player.Heal(_player.MaxHP);
            }
        }

        private void RunCombat(Enemy enemy)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nEnemy: {enemy.Name} (Lv {enemy.Level}) HP {enemy.HP}/{enemy.MaxHP}");
            Console.ResetColor();

            while (_player.IsAlive && enemy.IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[COMBAT] 1) Attack  2) Use Item  3) Retreat");
                Console.ResetColor();
                Console.Write("> ");
                var action = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }

                if (action == "3")
                {
                    SystemMessageService.Hint("You retreated from the gate.");
                    return;
                }

                if (action == "2")
                {
                    if (_player.Inventory.Items.Count == 0)
                    {
                        SystemMessageService.Hint("No items.");
                        continue;
                    }

                    Console.Write("Use item #: ");
                    var num = Console.ReadLine();
                    if (!int.TryParse(num, out var idx) || !_player.Inventory.UseItem(idx - 1, _player))
                    {
                        SystemMessageService.Hint("Invalid item number.");
                        continue;
                    }

                    SystemMessageService.Success("Item used.");
                }
                else
                {
                    var playerResult = CombatService.CalculatePlayerPhysicalDamage(_player, enemy);
                    ResolveDamage(playerResult, enemy);
                }

                if (!enemy.IsAlive)
                {
                    _lastDefeatedEnemy = enemy;

                    var xp = 25 + enemy.Level * 10;
                    var gold = 10 + enemy.Level * 3;

                    _player.GainXP(xp);
                    _player.AddGold(gold);

                    SystemMessageService.Success($"Defeated {enemy.Name}! +{xp} XP, +{gold}g");
                    _quests.NotifyEnemyDefeated(_player, enemy);

                    OfferExtraction(enemy);
                    return;
                }

                var enemyResult = CombatService.CalculateEnemyPhysicalDamage(enemy, _player);
                ResolveDamage(enemyResult, _player);
            }
        }

        private void OfferExtraction(Enemy defeatedEnemy)
        {
            Console.WriteLine("\nArise? (type 'arise' to attempt extraction, or press Enter to skip)");
            Console.Write("> ");
            var input = Console.ReadLine()?.Trim();
            if (!string.Equals(input, "arise", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                if (ShadowExtractionService.TryExtract(_player, defeatedEnemy, out var shadow) && shadow is not null)
                {
                    SystemMessageService.System($"Extraction successful. {shadow.Name} has joined your Shadow Army.");
                    return;
                }

                SystemMessageService.Warning($"Extraction failed. ({attempt}/3)");
            }
        }

        private static void ResolveDamage(CombatResult result, IEntity defender)
        {
            if (!result.IsHit)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{result.Source}'s attack missed! (Evaded)");
                Console.ResetColor();
                return;
            }

            defender.HP -= result.DamageDealt;
            if (defender.HP < 0)
            {
                defender.HP = 0;
            }

            ConsoleColor color = result.Source == "Player" ? ConsoleColor.Cyan : ConsoleColor.Red;
            Console.ForegroundColor = color;

            var critText = result.IsCritical ? " (CRIT!)" : string.Empty;
            Console.WriteLine($"{result.Source} deals {result.DamageDealt} damage{critText}!");
            Console.ResetColor();
        }

        private static string Bar(int current, int max, int width = 20)
        {
            if (max <= 0)
            {
                return "[--------------------]";
            }

            current = Math.Clamp(current, 0, max);
            var filled = (int)Math.Round((double)current / max * width);
            filled = Math.Clamp(filled, 0, width);
            return "[" + new string('█', filled) + new string('-', width - filled) + "]";
        }
    }
}

