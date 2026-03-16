using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Model
{
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

        protected Quest(string id, string title, string description, int xp, int gold)
        {
            Id = id;
            Title = title;
            Description = description;
            RewardXP = xp;
            RewardGold = gold;
            IsCompleted = false;
        }

        public abstract void CheckProgress(Player player);

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
    public class KillQuest : Quest
    {
        public string TargetEnemyName { get; private set; }
        public int RequiredKills { get; private set; }
        public int CurrentKills { get; private set; }

        public KillQuest(string id, string title, string description, string targetEnemy, int RequiredKills, int xp, int gold)
        : base(id, title, description, xp, gold)
        {
            TargetEnemyName = targetEnemy;
            this.RequiredKills = RequiredKills;
            CurrentKills = 0;
        }

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
        public override void CheckProgress(Player player)
        {
            if (CurrentKills >= RequiredKills)
            {
                Complete(player);
            }
        }
    }
}
