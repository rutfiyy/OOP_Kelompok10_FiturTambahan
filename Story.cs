using System;
using System.Collections.Generic;
using static System.Console;

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
            string prompt = "\n=== Introduction ===\nYou find yourself in a surreal dreamscape. Shadows flicker, and voices call to you...";
            string[] options = { "Follow the voices.", "Ignore them and explore the dark corners."};
            Menu storyMenu = new Menu(prompt, options);
            int choice = storyMenu.Run();

            if (choice == 0)
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

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        public static void StoryPath(Player player, int round)
        {
            string prompt = "\n=== Story Path ===\nAfter wandering, you encounter a forked path in the dreamscape.";
            List<string> options = new List<string>{ "Approach the broken clock tower", "Enter the desolate flower field", "Cross the foggy bridge"};

            if (round == 2) 
            {
                options.Add("Negotiate with the souls of the lost");
            }
            
            Menu pathMenu = new Menu(prompt, options.ToArray());
            int choice = pathMenu.Run();

            if (choice == 3)
            {
                Console.WriteLine("\nYou attempt to negotiate with the lost souls.");
                Console.WriteLine("\nThe souls offer you a trade.");
                Program.ShowStatTradeShop();
                return;
            }
            else 
            {
                var selectedPath = _storyPaths[choice + 1];
                Console.WriteLine($"\nYou chose to {selectedPath.PathDescription.ToLower()}.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                // Create the enemy based on the selected path
                List<Enemy> enemies = EnemyFactory.CreateEnemies(selectedPath.EnemyNames);

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
            BattleSystem battleSystem = new BattleSystem(player, enemies, new List<ISkillStrategy>
            {
                new Pierce(),
                new Heal(),
                new Annoy(),
                new Calm()
            });

            SkillMenu skillMenu = new SkillMenu(battleSystem, new List<ISkillStrategy>
            {
                new Pierce(),
                new Heal(),
                new Annoy(),
                new Calm()
            });

            bool battleOngoing = true;
            int expReward = 0;
            foreach (var enemy in enemies)
            {
                expReward += enemy.Exp;
            }

            while (battleOngoing)
            {
                // Display health status
                String prompt = $"\n=== Battle Start ===\n{player.Name.ToUpper()} Heart: {player.Heart}, Juice: {player.Juice}, Emotion: {player.EmotionType}\nEnemies: ";
                foreach (var enemy in enemies)
                {
                    prompt += $"\n{enemy.Name.ToUpper()} Heart: {enemy.Heart}, Emotion: {enemy.EmotionType}";
                }

                prompt += "\nChoose an action:";
                string[] options = { "Attack", "Skill", "Use Item from Inventory", "Escape"};
                Menu battleMenu = new Menu(prompt, options);
                int choice = battleMenu.Run();

                switch (choice)
                {
                    case 0:
                        // Select which enemy to attack (with exit option)
                        List<string> attackOptionsList = new List<string>();
                        prompt = "Select an enemy to attack (or enter 0 to cancel):";
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            attackOptionsList.Add(enemies[i].Name);
                        }
                        attackOptionsList.Add("Exit");

                        string[] attackOptions = attackOptionsList.ToArray();
                        Menu attackMenu = new Menu(prompt, attackOptions);
                        int enemyChoice = attackMenu.Run();
                        if (enemyChoice == enemies.Count)
                        {
                            Console.WriteLine("Action canceled.");
                            continue; // Return to the main menu
                        }

                        battleSystem.ExecuteAttackStrategy(enemyChoice); // Attack the selected enemy
                        break;

                    case 1:
                        bool skillUsed = skillMenu.Display(player, enemies); // Open skill menu and check if a skill was used
                        if (!skillUsed)
                        {
                            continue; // Player canceled, so the turn doesn't end
                        }
                        break;

                    case 2:
                        Program.UseInventoryItem(); // Placeholder for inventory item usage
                        continue;

                    case 3:
                        // Attempt to escape
                        if (AttemptEscape(player))
                        {
                            Console.WriteLine("You successfully escaped the battle.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Escape attempt failed!");
                        }
                        break;

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
                    player.isAlive = false;
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

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
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
                player.GainExp(expReward); // Award EXP after defeating an enemy
            }
        }

        private static bool AttemptEscape(Player player)
        {
            Random random = new Random();
            int escapeChance = random.Next(0, 100);
            return escapeChance < player.Speed;
        }
    }
}
