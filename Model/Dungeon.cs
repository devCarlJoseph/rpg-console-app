namespace ConsoleRPG.Model
{

  public class Dungeon
  {
    public string Name { get; private set; }
    public int RecommendedLevel { get; private set; }
    public bool IsCleared { get; private set; }

    private List<Enemy> _enemyWaves;
    public Enemy Boss { get; private set; }

    public Dungeon(string name, int recommendedLevel, List<Enemy> enemyWaves, Enemy boss)
    {
      Name = name;
      RecommendedLevel = recommendedLevel;
      _enemyWaves = enemyWaves;
      Boss = boss;
      IsCleared = false;
    }

    public void Enter(Player player)
    {
      if (player.Level < RecommendedLevel)
      {
        Console.WriteLine($"You are not strong enough to enter {Name}. Return when you are level {RecommendedLevel}.");
        return;
      }

      Console.WriteLine($"You have entered {Name}.");

    }

    public void ClearDungeon()
    {
      IsCleared = true;
      Console.WriteLine($"You have cleared {Name}.");
    }

    public void GetNextEnemy()
    {
      if (_enemyWaves.Count == 0)
      {
        ClearDungeon();
        return;
      }

      Enemy nextEnemy = _enemyWaves.Dequeue();
      Console.WriteLine($"You have encountered {nextEnemy.Name}.");
    }

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
