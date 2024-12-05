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
                        .AddMaxHeart(100)
                        .AddJuice(100)
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
            for (int round = 1; round < 4; round++)
            {
                Story.StoryPath(player1, round);
            }      
            Story.FinalBossEncounter(player1);
        }

        public static void UseInventoryItem()
        {
            string prompt = "\n=== Inventory ===\nChoose an item to use:";
            string[] inventoryItems = inventory.GetInventoryItems();
            Menu inventoryMenu = new Menu(prompt, inventoryItems);

            int choice = inventoryMenu.Run();
            if (choice == inventoryItems.Length - 1)
            {
                Console.WriteLine("Exiting Inventory.");
                return;
            }

            inventory.UseItem(choice, player1); 
        }

        public static void ShowStatTradeShop()
        {
            StatTradeShop statTradeShop = new StatTradeShop();
            bool shopping = true;

            while (shopping)
            {
                string[] itemsForSale = statTradeShop.GetItemsForSale();
                Menu statTradeShopMenu = new Menu("\nChoose an item to buy", itemsForSale);
                statTradeShop.DisplayItems();
                int choice = statTradeShopMenu.Run();

                if (choice == itemsForSale.Length - 1)
                {
                    shopping = false;
                    Console.WriteLine("Exiting stat trade shop.");
                }
                else
                {
                    statTradeShop.BuyItem(choice, player1, inventory);
                }
            }
        }

        private static int GetValidInput(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine($"Please enter a valid number between {min} and {max}.");
            }
        }
    }
}
