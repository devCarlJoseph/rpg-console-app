namespace ConsoleRPG.Model
{
    public class Shadow
    {
        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }
        public int Defense { get; }

        public Shadow(string name, int level, int strength, int defense)
        {
            Name = name;
            Level = level;
            Strength = strength;
            Defense = defense;
        }
    }
}

