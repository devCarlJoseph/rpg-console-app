namespace ConsoleRPG.Model
{

  /// <summary>
  /// Represents a dungeon with queued enemy waves and a boss.
  /// </summary>
  public class Dungeon
  {
    public string Name { get; private set; }
    public int RecommendedLevel { get; private set; }
    public bool IsCleared { get; private set; }

    // Use a queue to model waves of enemies that are fought in order.
    private readonly Queue<Enemy> _enemyWaves;
    public Enemy Boss { get; private set; }

    /// <summary>
    /// Creates a dungeon with predefined waves and boss.
    /// </summary>
    public Dungeon(string name, int recommendedLevel, List<Enemy> enemyWaves, Enemy boss)
    {
      Name = name;
      RecommendedLevel = recommendedLevel;
      _enemyWaves = new Queue<Enemy>(enemyWaves);
      Boss = boss;
      IsCleared = false;
    }

    /// <summary>
    /// Announces entry and prevents entry if player level is too low.
    /// </summary>
    public void Enter(Player player)
    {
      if (player.Level < RecommendedLevel)
      {
        Console.WriteLine($"You are not strong enough to enter {Name}. Return when you are level {RecommendedLevel}.");
        return;
      }

      Console.WriteLine($"You have entered {Name}.");

    }

    /// <summary>
    /// Marks the dungeon as cleared and reports to the player.
    /// </summary>
    public void ClearDungeon()
    {
      IsCleared = true;
      Console.WriteLine($"You have cleared {Name}.");
    }

    /// <summary>
    /// Peeks at the next enemy wave without removing it.
    /// </summary>
    public Enemy? PeekNextEnemy()
    {
      if (_enemyWaves.Count == 0)
      {
        return null;
      }

      return _enemyWaves.Peek();
    }

    /// <summary>
    /// Removes and returns the next enemy wave if available.
    /// </summary>
    public Enemy? DequeueNextEnemy()
    {
      if (_enemyWaves.Count == 0)
      {
        return null;
      }

      return _enemyWaves.Dequeue();
    }

    /// <summary>
    /// Runs a simple back-and-forth battle simulation against a single enemy.
    /// </summary>
    public void Battle(Player player, Enemy enemy)
    {
      while (player.IsAlive && enemy.IsAlive)
      {
        player.Attack(enemy);
        if (enemy.IsAlive)
        {
          player.TakeDamage(enemy.Attack());
        }
      }

      if (player.IsAlive)
      {
        Console.WriteLine($"You have defeated {enemy.Name}.");
        ClearDungeon();
      }
      else
      {
        Console.WriteLine($"You have been defeated by {enemy.Name}.");
      }
    }
  }
}
