namespace ConsoleRPG.Interfaces
{
    public interface IEntity
    {
        int HP { get; set; }
        int MaxHP { get; }
        void TakeDamage(int damage);
    }
}