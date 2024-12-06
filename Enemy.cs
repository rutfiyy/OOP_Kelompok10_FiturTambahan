namespace OOP_Kelompok2
{
    public class Enemy
    {
        public string? Name { get; set; }
        public int Heart { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Exp { get; set; }
        public int HitRate { get; set; }
        public int Luck { get; set; }
        public Emotion EmotionType { get; set; }
        public bool IsStunned { get; set; } = false;
        public Random _random = new Random();

        public void AttackPlayer(Player player)
        {
            if (IsStunned)
            {
                Console.WriteLine($"{Name} is stunned and cannot attack!");
                IsStunned = false;
                return;
            }

            if (_random.Next(0, 100) > HitRate)
            {
                Console.WriteLine($"{Name} misses the attack!");
                return;
            }
        
            int damage = EmotionDamageCalculator.CalculateDamage(AttackPower, EmotionType, player.EmotionType);
            string addMessage = EmotionDamageCalculator.AttackEffect(EmotionType, player.EmotionType);

            Console.WriteLine($"{Name} attacks {player.Name}. {addMessage}");

            if (_random.Next(0, 100) < Luck)
            {
                damage *= 2;
                Console.WriteLine("Critical Hit!");
            }
            damage = Math.Max(damage - player.Defense, 5);
            player.Heart = Math.Max(player.Heart - damage, 0); // Subtract calculated damage from player health
            Console.WriteLine($"{player.Name} takes {damage} damage. Remaining Health: {player.Heart}");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Enemy Name: {Name}");
            Console.WriteLine($"Heart: {Heart}");
            Console.WriteLine($"Attack Power: {AttackPower}");
            Console.WriteLine($"Emotion: {EmotionType}");
        }

        public void PerformEmotionEffect()
        {
            switch (EmotionType)
            {
                case Emotion.Angry:
                    Console.WriteLine($"{Name} become Angry! Attack power increased!");
                    AttackPower += 5;
                    break;
                case Emotion.Sad:
                    Console.WriteLine($"{Name} become Sad. Defense increased but attack reduced.");
                    Heart += 10;
                    AttackPower -= 2;
                    break;
                case Emotion.Happy:
                    Console.WriteLine($"{Name} become Happy! Faster speed and higher chance to crit.");
                    break;
                case Emotion.Neutral:
                    Console.WriteLine($"{Name} calms down. No special effects.");
                    break;
            }
        }
    }
}
