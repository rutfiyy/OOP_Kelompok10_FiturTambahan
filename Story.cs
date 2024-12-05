using System;
using System.Collections.Generic;

namespace OOP_Kelompok2
{
    public class Story
    {
        private static readonly Dictionary<int, (string PathDescription, List<string> EnemyNames)> _storyPaths =
            new()
            {
                { 1, ("Approach the broken clock tower", new List<string> {"space ex"}) },
                { 2, ("Enter the desolate flower field", new List<string> {"lost petal", "lost petal"}) },
                { 3, ("Cross the foggy bridge", new List<string> {"haunting shade", "haunting shade", "haunting shade"}) }
            };

        public static void Introduction(Player player)
        {
            Console.WriteLine("\n=== Introduction ===");
            Console.WriteLine("You find yourself in a surreal dreamscape. Shadows flicker, and voices call to you...");
            Console.WriteLine("Choose your reaction:");
            Console.WriteLine("1. Follow the voices.");
            Console.WriteLine("2. Ignore them and explore the dark corners.");
            int choice = GetValidInput(1, 2);

            if (choice == 1)
            {
                Console.WriteLine("\nYou follow the voices, feeling a strange sense of comfort.");
                player.EmotionType = Emotion.Happy;
                Console.WriteLine("You feel happy! Current Emotion: " + player.EmotionType);
            }
            else
            {
                Console.WriteLine("\nIgnoring the voices, you feel a chill down your spine.");
                player.Heart -= 5;
                Console.WriteLine("Health decreased by -5. Current Health: " + player.Heart);
            }
        }

        public static void StoryPath(Player player, int round)
        {
            Console.WriteLine("\n=== Story Path ===");
            Console.WriteLine("After wandering, you encounter a forked path in the dreamscape.");
            foreach (var path in _storyPaths)
            {
                Console.WriteLine($"{path.Key}. {path.Value.PathDescription}");
            }

            int choice;
            if (round == 2) 
            {
                Console.WriteLine("4. Negotiate with the souls of the lost."); 
                choice = GetValidInput(1, 4);
            }
            else choice = GetValidInput(1, _storyPaths.Count);
            

            if (choice == 4)
            {
                Console.WriteLine("\nYou attempt to negotiate with the lost souls.");
                Console.WriteLine("\nThe souls offer you a trade.");
                Program.ShowStatTradeShop();
                return;
            }
            else 
            {
                var selectedPath = _storyPaths[choice];
                Console.WriteLine($"\nYou chose to {selectedPath.PathDescription.ToLower()}.");

                // Create the enemy based on the selected path
                List<Enemy> enemies = EnemyFactory.CreateEnemies(selectedPath.EnemyNames);

                //Console.WriteLine($"You encounter *{enemy.Name}*!");

                // Start the battle with the selected path's enemy
                StartBattle(player, enemies);
            }
        }

        public static void FinalBossEncounter(Player player)
        {
            Console.WriteLine("\n=== Boss Encounter ===");
            Console.WriteLine("BOSS ENEMY: *Memento Keeper* emerges from the shadows, wielding painful memories as weapons!");

            // Create the final boss
            Enemy boss = EnemyFactory.CreateEnemy("memento keeper");
            List<Enemy> enemies = new List<Enemy> { boss }; // Wrap the boss in a list

            // Start the final battle with the boss
            StartBattle(player, enemies, isFinalBattle: true);
        }

        private static void StartBattle(Player player, List<Enemy> enemies, bool isFinalBattle = false)
        {
            Console.WriteLine("\n=== Battle Start ===");

            BattleSystem battleSystem = new BattleSystem(player, enemies, new List<ISkillStrategy>
            {
                new DoubleSlash(),
                new Heal(),
                new Annoy(),
                new Calm()
            });

            SkillMenu skillMenu = new SkillMenu(battleSystem, new List<ISkillStrategy>
            {
                new DoubleSlash(),
                new Heal(),
                new Annoy(),
                new Calm()
            });

            bool battleOngoing = true;

            while (battleOngoing)
            {
                // Display health status
                Console.WriteLine($"\n{player.Name.ToUpper()} Heart: {player.Heart}, Juice: {player.Juice}, Emotion: {player.EmotionType}\nEnemies: ");
                foreach (var enemy in enemies)
                {
                    Console.WriteLine($"{enemy.Name.ToUpper()} Heart: {enemy.Heart}, Emotion: {enemy.EmotionType}");
                }

                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Skill");
                Console.WriteLine("3. Use Item from Inventory");
                Console.WriteLine("4. Escape");

                int choice = GetValidInput(1, 4);

                switch (choice)
                {
                    case 1:
                        // Select which enemy to attack (with exit option)
                        Console.WriteLine("Select an enemy to attack (or enter 0 to cancel):");
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {enemies[i].Name}");
                        }

                        int enemyChoice = GetValidInput(0, enemies.Count);
                        if (enemyChoice == 0)
                        {
                            Console.WriteLine("Action canceled.");
                            continue; // Return to the main menu
                        }

                        battleSystem.ExecuteAttackStrategy(enemyChoice - 1); // Attack the selected enemy
                        break;

                    case 2:
                        bool skillUsed = skillMenu.Display(player, enemies); // Open skill menu and check if a skill was used
                        if (!skillUsed)
                        {
                            continue; // Player canceled, so the turn doesn't end
                        }
                        break;

                    case 3:
                        Program.UseInventoryItem(); // Placeholder for inventory item usage
                        continue;

                    case 4:
                        Console.WriteLine("You escaped the battle.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please select a valid option.");
                        break;
                }

                // Check if all enemies are defeated
                if (battleSystem.AreAllEnemiesDefeated())
                {
                    Console.WriteLine("All enemies have been defeated! You win!");
                    battleOngoing = false;
                }

                // Enemies take their turn to attack the player
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    if (enemies[i].Heart <= 0)
                    {
                        enemies.RemoveAt(i);
                    }
                    else
                    {
                        enemies[i].AttackPlayer(player);
                    }
                }

                // Check if the player is defeated
                if (player.Heart <= 0)
                {
                    Console.WriteLine("You have been defeated. Game over.");
                    battleOngoing = false;
                }

                // Special behavior for final battle
                if (isFinalBattle)
                {
                    foreach (var enemy in enemies)
                    {
                        if (!enemy.IsStunned)
                        {
                            Console.WriteLine($"{enemy.Name} uses a special attack, reducing your attack power!");
                            player.Attack = Math.Max(player.Attack - 5, 5);
                        }
                    }
                }
            }

            // Award experience points for defeated enemies
            if (isFinalBattle)
            {
                if (player.Heart > 0)
                {
                    Console.WriteLine("You have completed the game! Congratulations!");
                }
            }
            else
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.Heart <= 0)
                    {
                        player.GainExp(enemy.Exp); // Award EXP after defeating an enemy
                    }
                }
            }
        }

        private static int GetValidInput(int min, int max)
        {
            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }
    }
}
