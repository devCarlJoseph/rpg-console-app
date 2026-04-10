namespace ConsoleRPG.Interfaces
{
    // Contract for quests that track progress and grant rewards.
    public interface IQuest
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }

        bool IsCompleted { get; }

        int RewardXP { get; }
        int RewardGold { get; }

        // Evaluates current progress and marks completion if met.
        void CheckProgress(Model.Player player);

        // Grants rewards and flags completion.
        void Complete(Model.Player player);
    }
}
