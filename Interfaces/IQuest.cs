namespace ConsoleRPG.Interfaces
{
    public interface IQuest
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }

        bool IsCompleted { get; }

        int RewardXP { get; }
        int RewardGold { get; }

        void CheckProgress(Model.Player player);
        void Complete(Model.Player player);
    }
}