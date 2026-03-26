namespace ConsoleRPG.Interfaces
{
    /// <summary>
    /// Contract for quests that track progress and grant rewards.
    /// </summary>
    public interface IQuest
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }

        bool IsCompleted { get; }

        int RewardXP { get; }
        int RewardGold { get; }

        /// <summary>
        /// Evaluates current progress and marks completion if met.
        /// </summary>
        void CheckProgress(Model.Player player);
        /// <summary>
        /// Grants rewards and flags completion.
        /// </summary>
        void Complete(Model.Player player);
    }
}
