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
                List<string> skillOptionsList = new List<string>();
                for (int i = 0; i < _skills.Count; i++)
                {
                    skillOptionsList.Add($"{_skills[i].GetType().Name} - Juice Cost: {_skills[i].GetCost()} - {_skills[i].Description()}");
                }
                skillOptionsList.Add("Exit");

                string[] skillOptions = skillOptionsList.ToArray();
                Menu skillMenu = new Menu("Select a skill to use:", skillOptions);
                int choice = skillMenu.Run();

                if (choice == _skills.Count)
                {
                    Console.WriteLine("Exiting skill menu.");
                    return false; // Return false to indicate the player canceled the skill action
                }

                var selectedSkill = _skills[choice];
                var isTargetEnemy = selectedSkill.IsTargetEnemy();

                if (player.Juice < selectedSkill.GetCost())
                {
                    Console.WriteLine("Not enough juice to use this skill! Choose a different skill.");
                }
                else
                {
                    if (isTargetEnemy)
                    {
                        // Ask player to choose the enemy for the skill
                        List<string> enemyOptions = new List<string>();
                        string prompt = "Select an enemy to attack (or enter 0 to cancel):";
                        
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemyOptions.Add(enemies[i].Name);
                        }
                        enemyOptions.Add("Exit");

                        Menu attackMenu = new Menu(prompt, enemyOptions.ToArray());
                        int enemyChoice = attackMenu.Run();

                        if (enemyChoice == enemies.Count)
                        {
                            Console.WriteLine("Action canceled.");
                            return false; // Return false to indicate the action was canceled
                        }
                        // Execute the selected skill on the chosen enemy
                        _battleSystem.ExecuteSkillStrategy(player, enemyChoice, selectedSkill);
                    }else
                    {
                        _battleSystem.ExecuteSkillStrategy(player, 0, selectedSkill);
                    }
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
