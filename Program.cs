using System;

namespace OOP_Kelompok2
{
    public class Program
    {
        static Player player1 = null!;
        static Inventory inventory = new Inventory(); 

        [STAThread]
        static void Main(string[] args)
        {
            MainMenu.ShowMenu(); 
        }

        public static void StartGame()
        {
            Awake();
            InitializeInventory(); 
            Start();
            Update();
        }

        static void Awake()
        {
            Console.WriteLine("Game is initializing...");
        }

        static void InitializeInventory()
        {
            
            inventory.AddItem(new Potion());
            inventory.AddItem(new AttackBoostDecorator(new Potion()));
            inventory.AddItem(new EmotionChangerDecorator(new HappyCandy(), "Happy"));

            Console.WriteLine("\n=== Inventory Initialized ===");
            inventory.DisplayInventory();
        }

        static void Start()
        {
            
            player1 = new PlayerBuild()
                        .AddName("Dreamer")
                        .AddHeart(100)
                        .AddJuice(50)
                        .AddAttack(20)
                        .AddDefense(15)
                        .AddSpeed(10)
                        .AddLuck(5)
                        .AddHitRate(90)
                        .Build();

            Console.WriteLine("\n=== Player Status ===");
            player1.DisplayStatus();

            
            Story.Introduction(player1);
        }

        static void Update()
        {
            
            Story.StoryPath(player1);            
            Story.FinalBossEncounter(player1);
        }

        public static void UseInventoryItem()
        {
            Console.WriteLine("\n=== Inventory ===");
            inventory.DisplayInventory();
            Console.WriteLine("Choose an item to use (enter the number):");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > inventory.Count)
            {
                Console.WriteLine("Invalid choice. Returning to battle.");
                return;
            }

            inventory.UseItem(choice - 1, player1); 
        }
    }
}
