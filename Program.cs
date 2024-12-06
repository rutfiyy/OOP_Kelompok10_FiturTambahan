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
                        .AddMaxJuice(100)
                        .AddAttack(20)
                        .AddDefense(8)
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
            Story.StoryLine(player1);
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
            StatTradeTreasure statTradeTreasure = new StatTradeTreasure();
            bool treasure = true;
            int itemsBrought = 0;
            const int maxItems = 3;

            while (treasure)
            {
                if (itemsBrought >= maxItems)
                {
                    Console.WriteLine("You have reached the maximum number of items you can buy");
                    break;
                }
                
                string[] treasureForGet = statTradeTreasure.GetTreasure();
                Menu statTradeTreasureMenu = new Menu($"\nChoose treasure (Max 3 treasure) :\n{maxItems - itemsBrought} Left", treasureForGet);
                statTradeTreasure.DisplayItems();
                int choice = statTradeTreasureMenu.Run();

                if (choice == treasureForGet.Length - 1)
                {
                    treasure = false;
                    Console.WriteLine("Exiting stat trade shop.");
                }
                else
                {
                    statTradeTreasure.BuyItem(choice, player1, inventory);
                    itemsBrought++;
                }
            }
        }
    }
}
