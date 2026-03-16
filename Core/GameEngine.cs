using ConsoleRPG.Interfaces;
using ConsoleRPG.Model;
using ConsoleRPG.Services;

namespace ConsoleRPG.Core
{
    /// <summary>
    /// High-level game loop for the console RPG.
    /// 
    /// This first version focuses on:
    /// - A simple command parser (type commands like "help", "stats", "hunt", "exit").
    /// - A very small combat loop when you choose "hunt".
    /// - Clear, guided console output so you always know what to type next.
    /// 
    /// As you implement more systems (quests, shadows, gates),
    /// you can extend this class with new commands and menus.
    /// </summary>
    public class GameEngine
    {
        private readonly Player _player;
        private readonly Random _rng = new();

        public GameEngine(Player player)
        {
            _player = player;
        }

        /// <summary>
        /// Starts the main in-city loop where the player can type commands.
        /// </summary>
        public void Run()
        {
            Console.Clear();
            WriteSystemMessage($"Welcome, {_player.Name}. The System is now online.");
            WriteHint("Type 'help' to see available commands.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n> ");
                Console.ResetColor();

                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                var command = input.Trim().ToLowerInvariant();

                switch (command)
                {
                    case "help":
                        ShowHelp();
                        break;

                    case "stats":
                        ShowStats();
                        break;

                    case "hunt":
                        RunSimpleHunt();
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "exit":
                    case "quit":
                        WriteSystemMessage("Exiting to title screen...");
                        return;

                    default:
                        WriteHint("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Available Commands ===");
            Console.ResetColor();
            Console.WriteLine("  help   - Show this list of commands.");
            Console.WriteLine("  stats  - View your current level and core stats.");
            Console.WriteLine("  hunt   - Enter a quick fight against a random enemy.");
            Console.WriteLine("  clear  - Clear the console window.");
            Console.WriteLine("  exit   - Return to the title screen.");
        }

        private void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== Player Status ===");
            Console.ResetColor();
            Console.WriteLine($"  Name : {_player.Name}");
            Console.WriteLine($"  Level: {_player.Level} (XP: {_player.XP})");
            Console.WriteLine($"  HP   : {_player.HP}/{_player.MaxHP}");
            Console.WriteLine($"  MP   : {_player.MP}/{_player.MaxMP}");
            Console.WriteLine($"  STR  : {_player.Strength}");
            Console.WriteLine($"  AGI  : {_player.Agility}");
            Console.WriteLine($"  INT  : {_player.Intelligence}");
            Console.WriteLine($"  DEF  : {_player.Defense}");
        }

        /// <summary>
        /// Very small guided combat encounter.
        /// This is meant to give you an immediate, visible gameplay loop:
        /// - Player and Enemy take turns attacking until one is defeated.
        /// - Uses CombatService so you can later plug in more complex math.
        /// </summary>
        private void RunSimpleHunt()
        {
            var enemy = CreateRandomEnemyForPlayer();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nA wild {enemy.Name} (Lv {enemy.Level}) appears!");
            Console.ResetColor();

            while (_player.IsAlive && enemy.IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[COMBAT] Choose action: (a)ttack  (s)tats  (r)un");
                Console.ResetColor();
                Console.Write("> ");
                var action = Console.ReadLine()?.Trim().ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }

                if (action.StartsWith("r"))
                {
                    WriteHint("You retreat from the fight and return to the city.");
                    return;
                }

                if (action.StartsWith("s"))
                {
                    ShowStats();
                    continue;
                }

                // --- Player turn ---
                var playerResult = CombatService.CalculatePlayerPhysicalDamage(_player, enemy);
                ResolveDamage(playerResult, enemy, _player);

                if (!enemy.IsAlive)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nYou defeated {enemy.Name}!");
                    Console.ResetColor();

                    var xpGained = 25 + enemy.Level * 10;
                    _player.GainXP(xpGained);

                    WriteSystemMessage($"+{xpGained} XP gained.");
                    return;
                }

                // --- Enemy turn ---
                var enemyResult = CombatService.CalculateEnemyPhysicalDamage(enemy, _player);
                ResolveDamage(enemyResult, _player, enemy);

                if (!_player.IsAlive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou were defeated...");
                    Console.ResetColor();
                    WriteSystemMessage("The System will restore you to full health for now.");
                    _player.Heal(_player.MaxHP);
                    return;
                }
            }
        }

        private Enemy CreateRandomEnemyForPlayer()
        {
            // Very small pool of enemies for now.
            var enemyLevel = Math.Max(1, _player.Level);
            var roll = _rng.Next(0, 3);

            return roll switch
            {
                0 => new Goblin("Goblin Scout", enemyLevel),
                1 => new Skeleton("Skeleton Soldier", enemyLevel),
                _ => new WildWolf("Wild Wolf", enemyLevel),
            };
        }

        private static void ResolveDamage(CombatResult result, IEntity defender, IEntity attacker)
        {
            if (!result.IsHit)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{result.Source}'s attack missed! (Evaded)");
                Console.ResetColor();
                return;
            }

            // Defender here is either Player or Enemy; we want to apply final damage directly.
            defender.HP -= result.DamageDealt;
            if (defender.HP < 0)
            {
                defender.HP = 0;
            }

            ConsoleColor color = result.Source == "Player" ? ConsoleColor.Cyan : ConsoleColor.Red;
            Console.ForegroundColor = color;

            var critText = result.IsCritical ? " (CRIT!)" : string.Empty;
            Console.WriteLine($"{result.Source} deals {result.DamageDealt} damage{critText}!");
            Console.ResetColor();
        }

        private static void WriteSystemMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[SYSTEM] {message}");
            Console.ResetColor();
        }

        private static void WriteHint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[Hint] {message}");
            Console.ResetColor();
        }
    }
}

