namespace ConsoleRPG
{
    public interface IQuest
    {
        string Name { get; }
        string Description { get; }
        int RewardXP { get; }
        int RewardGold { get; }
        void CompleteQuest(){ }
    }
}