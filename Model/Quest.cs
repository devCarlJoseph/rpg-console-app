using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
    /// <summary>
    /// Base quest with rewards and completion handling.
    /// </summary>
    public abstract class Quest : IQuest
    {
        // -- Basic Quest Information --
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public bool IsCompleted { get; protected set; }

        // -- Rewards --
        public int RewardXP { get; private set; }
        public int RewardGold { get; private set; }

        /// <summary>
        /// Initializes core quest details and rewards.
        /// </summary>
        protected Quest(string id, string title, string description, int xp, int gold)
        {
            Id = id;
            Title = title;
            Description = description;
            RewardXP = xp;
            RewardGold = gold;
            IsCompleted = false;
        }

        /// <summary>
        /// Implemented by subclasses to decide when the quest is complete.
        /// </summary>
        public abstract void CheckProgress(Player player);

        /// <summary>
        /// Marks the quest complete, awards rewards, and prints a summary.
        /// </summary>
        public virtual void Complete(Player player)
        {
            if (IsCompleted)
            {
                return;
            }

            IsCompleted = true;

            player.GainXP(RewardXP);
            player.AddGold(RewardGold);

            Console.WriteLine($"Quest Completed: {Title}");
            Console.WriteLine($"Rewards: {RewardXP} XP, {RewardGold} Gold");
        }
    }

    //Example Quest Type: Kill Quest
    /// <summary>
    /// Quest requiring a number of kills of a target enemy.
    /// </summary>
    public class KillQuest : Quest
    {
        public string TargetEnemyName { get; private set; }
        public int RequiredKills { get; private set; }
        public int CurrentKills { get; private set; }

        /// <summary>
        /// Creates a kill quest with target enemy and kill requirement.
        /// </summary>
        public KillQuest(string id, string title, string description, string targetEnemy, int RequiredKills, int xp, int gold)
        : base(id, title, description, xp, gold)
        {
            TargetEnemyName = targetEnemy;
            this.RequiredKills = RequiredKills;
            CurrentKills = 0;
        }

        /// <summary>
        /// Increments kill count when the target enemy is defeated and completes if threshold met.
        /// </summary>
        public void IncrementKills(string enemyName, Player player)
        {
            if (IsCompleted)
            {
                return;
            }

            if (enemyName == TargetEnemyName)
            {
                CurrentKills++;

                Console.WriteLine($"{Title} Progress: {CurrentKills}/{RequiredKills}");

                if (CurrentKills >= RequiredKills)
                {
                    Complete(player);
                }
            }
        }

        // Required override from Quest
        /// <summary>
        /// Checks whether required kills have been met.
        /// </summary>
        public override void CheckProgress(Player player)
        {
            if (CurrentKills >= RequiredKills)
            {
                Complete(player);
            }
        }
    }

    /// <summary>
    /// Quest that completes once a specific dungeon id is cleared.
    /// </summary>
    public class ClearDungeonQuest : Quest
    {
        public string DungeonId { get; }

        /// <summary>
        /// Creates a clear-dungeon quest linked to a dungeon id.
        /// </summary>
        public ClearDungeonQuest(string id, string title, string description, string dungeonId, int xp, int gold)
            : base(id, title, description, xp, gold)
        {
            DungeonId = dungeonId;
        }

        /// <summary>
        /// Progress handled externally; no internal checks required.
        /// </summary>
        public override void CheckProgress(Player player)
        {
            // Progress is driven externally via QuestManager.NotifyDungeonCleared(...)
        }

        /// <summary>
        /// Marks quest complete when the matching dungeon is reported cleared.
        /// </summary>
        public void NotifyCleared(string dungeonId, Player player)
        {
            if (IsCompleted)
            {
                return;
            }

            if (string.Equals(dungeonId, DungeonId, StringComparison.OrdinalIgnoreCase))
            {
                Complete(player);
            }
        }
    }
}
