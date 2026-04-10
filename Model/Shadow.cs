namespace ConsoleRPG.Model
{
    // Represents a captured shadow ally with level and stats.
    public class Shadow
    {
        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }
        public int Defense { get; }


        // Creates a shadow instance with immutable stats.

        public Shadow(string name, int level, int strength, int defense)
        {
            Name = name;
            Level = level;
            Strength = strength;
            Defense = defense;
        }
    }
}

