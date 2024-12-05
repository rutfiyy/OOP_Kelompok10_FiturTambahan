namespace OOP_Kelompok2
{
    public class BattleSystem
    {
        private List<ISkillStrategy> _skills;
        private List<Enemy> _enemies; // A list of enemies
        private Player _player;

        public BattleSystem(Player player, List<Enemy> enemies, List<ISkillStrategy> skills)
        {
            _player = player;
            _enemies = enemies;
            _skills = skills;
        }

        // A public method to get the list of enemies
        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }

        // Executes an attack for the player on a specific enemy
        public void ExecuteAttackStrategy(int enemyIndex)
        {
            if (enemyIndex >= 0 && enemyIndex < _enemies.Count)
            {
                var enemy = _enemies[enemyIndex]; // Select the target enemy
                _player.AttackEnemy(enemy); 
            }
            else
            {
                Console.WriteLine("Invalid enemy selection.");
            }
        }

        // Executes the selected skill on a specific enemy
        public void ExecuteSkillStrategy(Player player, int enemyIndex, ISkillStrategy skill)
        {
            if (enemyIndex >= 0 && enemyIndex < _enemies.Count)
            {
                var enemy = _enemies[enemyIndex]; // Select the target enemy
                skill.Execute(player, enemy); // Execute the skill on the selected enemy
                skill.Message(player, enemy);
            }
            else
            {
                Console.WriteLine("Invalid enemy selection.");
            }
        }

        // Displays the status of all enemies
        public void CheckBattleStatus()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Console.WriteLine($"Enemy #{i + 1}:");
                _enemies[i].DisplayStatus();
            }
        }

        // Returns true if all enemies are defeated
        public bool AreAllEnemiesDefeated()
        {
            return _enemies.All(e => e.Heart <= 0);
        }
    }
}
