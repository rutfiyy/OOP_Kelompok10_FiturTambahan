namespace OOP_Kelompok2
{
    public class EnemyFactory
    {
        public static List<Enemy> CreateEnemies(List<string> enemyTypes)
        {
            var enemies = new List<Enemy>();

            foreach (var enemyType in enemyTypes)
            {
                enemies.Add(CreateEnemy(enemyType));
            }
            return enemies;
        }

        public static Enemy CreateEnemy(string enemyType)
        {
            switch (enemyType.ToLower())
            {
                case "space ex":
                    return new Enemy
                    {
                        Name = "Space Ex",
                        Heart = 60,
                        AttackPower = 15,
                        Exp = 30,
                        HitRate = 80,
                        Luck = 10,
                        EmotionType = Emotion.Sad
                    };
                case "lost petal":
                    return new Enemy
                    {
                        Name = "Lost Petal",
                        Heart = 40,
                        AttackPower = 10,
                        Exp = 10,
                        HitRate = 90,
                        Luck = 5,
                        EmotionType = Emotion.Happy
                    };
                case "haunting shade":
                    return new Enemy
                    {
                        Name = "Haunting Shade",
                        Heart = 30,
                        AttackPower = 8,
                        Exp = 8,
                        HitRate = 95,
                        Luck = 2,
                        EmotionType = Emotion.Angry
                    };
                case "memento keeper":
                    return new Enemy
                    {
                        Name = "Memento Keeper",
                        Heart = 120,
                        AttackPower = 25,
                        Exp = 50,
                        HitRate = 90,
                        Luck = 30,
                        EmotionType = Emotion.Neutral
                    };
                default:
                    Console.WriteLine($"Unknown enemy type: {enemyType}. Defaulting to a basic enemy.");
                    return new Enemy
                    {
                        Name = "Unknown Entity",
                        Heart = 30,
                        AttackPower = 5,
                        Exp = 5,
                        HitRate = 70,
                        Luck = 5,
                        EmotionType = Emotion.Neutral
                    };
            }
        }
    }
}
