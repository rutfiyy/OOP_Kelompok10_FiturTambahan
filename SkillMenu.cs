namespace OOP_Kelompok2
{
    public class SkillMenu
    {
        private BattleSystem _battleSystem;
        private List<ISkillStrategy> _skills;

        public SkillMenu(BattleSystem battleSystem, List<ISkillStrategy> skills)
        {
            _battleSystem = battleSystem;
            _skills = skills;
        }

        public bool Display(Player player, List<Enemy> enemies)
        {
            bool skillMenuActive = true;

            while (skillMenuActive)
            {
                Console.WriteLine("\nChoose a skill (or enter 0 to exit):");
                for (int i = 0; i < _skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_skills[i].GetType().Name} - Juice Cost: {_skills[i].GetCost()}");
                }

                int choice = GetValidInput(0, _skills.Count); // Allow user to enter 0 to exit

                if (choice == 0)
                {
                    Console.WriteLine("Exiting skill menu.");
                    return false; // Return false to indicate the player canceled the skill action
                }

                var selectedSkill = _skills[choice - 1];

                if (player.Juice < selectedSkill.GetCost())
                {
                    Console.WriteLine("Not enough juice to use this skill! Choose a different skill.");
                }
                else
                {
                    // Ask player to choose the enemy for the skill
                    Console.WriteLine("Select an enemy to target (or enter 0 to cancel):");
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {enemies[i].Name}");
                    }

                    int enemyChoice = GetValidInput(0, enemies.Count);
                    if (enemyChoice == 0)
                    {
                        Console.WriteLine("Action canceled.");
                        return false; // Return false to indicate the action was canceled
                    }

                    // Execute the selected skill on the chosen enemy
                    _battleSystem.ExecuteSkillStrategy(player, enemyChoice - 1, selectedSkill);
                    return true; // Return true to indicate a skill was successfully used
                }
            }
            return false;
        }

        private int GetValidInput(int min, int max)
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
