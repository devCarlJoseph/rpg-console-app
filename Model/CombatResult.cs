namespace ConsoleRPG.Model
{
    public class CombatResult
    {
        public int DamageDealt { get; set; }
        public bool IsHit { get; set; }
        public bool IsCritical { get; set; }
        public bool WasEvaded { get; set; }
        public bool WasBlocked { get; set; }

        // Simple identifier like "Player", "Enemy", or later "Shadow".
        public string Source { get; set; } = string.Empty;
    }
}