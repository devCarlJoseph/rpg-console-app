using ConsoleRPG.Model;

namespace ConsoleRPG.Services
{
    public class QuestManager
    {
        private int _day = 1;

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
    }
}

