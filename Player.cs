namespace OOP_Kelompok2
{
    public class Player
    {
        public string? Name { get; set; }
        public int MaxHeart { get; set; }
        public int Heart { get; set; }
        public int MaxJuice { get; set; }
        public int Juice { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public int HitRate { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public Emotion EmotionType { get; set; } = Emotion.Neutral;
        private Random _random = new Random();
        public bool isAlive = true;

        public void AttackEnemy(Enemy enemy)
        {
            if (_random.Next(0, 100) > HitRate) // Miss chance
            {
                Console.WriteLine($"{Name} misses the attack!");
                return;
            }

            int damage = EmotionDamageCalculator.CalculateDamage(Attack, EmotionType, enemy.EmotionType);
            string addMessage = EmotionDamageCalculator.AttackEffect(EmotionType, enemy.EmotionType);

            Console.WriteLine($"{Name} attacks {enemy.Name}. {addMessage}");
            if (_random.Next(0, 100) < Luck) // Critical hit chance
            {
                damage *= 2;
                Console.WriteLine("Critical Hit!");
            }

            damage = Math.Max(damage - enemy.Defense, 5);
            enemy.Heart = Math.Max(enemy.Heart - damage, 0); // Subtract calculated damage from enemy's health
            Console.WriteLine($"{enemy.Name} takes {damage} damage. Remaining Health: {enemy.Heart}");
        }
        
        public void GainExp(int amount)
        {
            Exp += amount;
            Console.WriteLine($"{Name} gained {amount} EXP. Total EXP: {Exp}");
            if (Exp >= Level * 10) // Threshold level-up
            {
                Console.WriteLine("\n=== Level Up ===");
                LevelUp();
            }
        }

        private void LevelUp()
        {
            while (Exp >= Level * 10)
            {
                Exp -= Level * 10;
                Level++;
                MaxHeart += 10;
                Heart = MaxHeart;
                MaxJuice += 5;
                Juice = MaxJuice;
                Attack += 2;
                Defense += 2;
                Speed += 5;
                Luck += 3;
                HitRate += 2;
                
                Console.WriteLine($"{Name} leveled up! Now at Level {Level} with enhanced attributes.");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Health: {Heart}");
            Console.WriteLine($"Juice: {Juice}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Speed: {Speed}");
            Console.WriteLine($"Luck: {Luck}");
            Console.WriteLine($"Emotion: {EmotionType}");
        }
    }
}
