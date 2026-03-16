using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public class QuestManager
    {
        private int _day = 1;
        private readonly QuestDataService.QuestDb _questDb;
        private readonly Dictionary<string, QuestDataService.QuestDefinition> _questById;
        private readonly Dictionary<string, string> _enemyNameById;

        public QuestManager()
        {
            _questDb = QuestDataService.Load();
            _questById = _questDb.Quests.ToDictionary(q => q.Id, q => q, StringComparer.OrdinalIgnoreCase);

            // Used to turn TargetEnemyId into a name for KillQuest progress.
            var enemyDb = EnemyDataService.Load();
            _enemyNameById = enemyDb.Enemies.Normal
                .Concat(enemyDb.Enemies.Elite)
                .Concat(enemyDb.Enemies.Boss)
                .GroupBy(e => e.Id, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Name, StringComparer.OrdinalIgnoreCase);
        }

        public void NewDay(Player player)
        {
            _day++;
            AssignDailyQuest(player);
        }

        public void AssignDailyQuest(Player player)
        {
            // Minimal daily quest: kill 3 goblins.
            if (player.ActiveQuests.Any(q => q.Id == "daily_kill_goblin"))
            {
                return;
            }

            var quest = new KillQuest(
                id: "daily_kill_goblin",
                title: "Daily Quest: Goblin Cleanup",
                description: "Defeat 3 Goblins to complete today's assignment.",
                targetEnemy: "Goblin",
                RequiredKills: 3,
                xp: 40,
                gold: 25);

            player.ActiveQuests.Add(quest);
            SystemMessageService.System($"Daily Quest Assigned: {quest.Title}");
        }

        public IReadOnlyList<QuestDataService.QuestDefinition> GetAllQuestDefinitions()
            => _questDb.Quests;

        public bool TryAcceptQuest(Player player, string questId)
        {
            if (player.ActiveQuests.Any(q => q.Id.Equals(questId, StringComparison.OrdinalIgnoreCase)) ||
                player.CompletedQuests.Any(q => q.Id.Equals(questId, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (!_questById.TryGetValue(questId, out var def))
            {
                return false;
            }

            var quest = CreateQuestFromDefinition(def);
            player.ActiveQuests.Add(quest);
            SystemMessageService.System($"Quest Accepted: {quest.Title}");
            return true;
        }

        public void AssignDungeonQuests(Player player, IEnumerable<string> questIds)
        {
            foreach (var id in questIds)
            {
                TryAcceptQuest(player, id);
            }
        }

        public void NotifyEnemyDefeated(Player player, Enemy enemy)
        {
            foreach (var quest in player.ActiveQuests.OfType<KillQuest>())
            {
                quest.IncrementKills(enemy.Name.Contains("Goblin", StringComparison.OrdinalIgnoreCase) ? "Goblin" : enemy.Name, player);
            }

            var completed = player.ActiveQuests.Where(q => q.IsCompleted).ToList();
            foreach (var quest in completed)
            {
                player.ActiveQuests.Remove(quest);
                player.CompletedQuests.Add(quest);
            }
        }

        public void NotifyDungeonCleared(Player player, string dungeonId)
        {
            foreach (var quest in player.ActiveQuests.OfType<ClearDungeonQuest>())
            {
                quest.NotifyCleared(dungeonId, player);
            }

            var completed = player.ActiveQuests.Where(q => q.IsCompleted).ToList();
            foreach (var quest in completed)
            {
                player.ActiveQuests.Remove(quest);
                player.CompletedQuests.Add(quest);
            }
        }

        private Quest CreateQuestFromDefinition(QuestDataService.QuestDefinition def)
        {
            if (def.Type.Equals("ClearDungeon", StringComparison.OrdinalIgnoreCase))
            {
                return new ClearDungeonQuest(
                    id: def.Id,
                    title: def.Title,
                    description: def.Description,
                    dungeonId: def.DungeonId ?? string.Empty,
                    xp: def.RewardXP,
                    gold: def.RewardGold);
            }

            // Default: Kill quest
            var targetName = def.TargetEnemyId is not null && _enemyNameById.TryGetValue(def.TargetEnemyId, out var n)
                ? n
                : (def.TargetEnemyId ?? "Unknown");

            return new KillQuest(
                id: def.Id,
                title: def.Title,
                description: def.Description,
                targetEnemy: targetName,
                RequiredKills: Math.Max(1, def.RequiredKills),
                xp: def.RewardXP,
                gold: def.RewardGold);
        }
    }
}

