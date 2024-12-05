using System;
using static System.Console;

namespace OOP_Kelompok2
{
    public class MainMenu
    {
        public static void ShowMenu()
        {
            string prompt = @"
.___                                                                             .         _                       .                      
/   `  .___    ___    ___  , _ , _     ____   ___    ___  \,___,   ___          /|      ___/ _   __   ___  , __   _/_   ,   . .___    ___ 
|    | /   \ .'   `  /   ` |' `|' `.  (     .'   `  /   ` |    \ .'   `        /  \    /   | |   /  .'   ` |'  `.  |    |   | /   \ .'   `
|    | |   ' |----' |    | |   |   |  `--.  |      |    | |    | |----'       /---'\  ,'   | `  /   |----' |    |  |    |   | |   ' |----'
/---/  /     `.___, `.__/| /   '   / \___.'  `._.' `.__/| |`---' `.___,     ,'      \ `___,'  \/    `.___, /    |  \__/ `._/| /     `.___,
                                                        \                                `                                              ";
            string[] options = { "Play Game", "ReadMe", "Credits", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Program.StartGame(); // Memulai permainan
                    break;
                case 1:
                    ShowReadMe();
                    break;
                case 2:
                    ShowCredits();
                    break;
                case 3:
                    ExitGame();
                    break;
                default:
                    break;
            }
        }

        private static void ShowReadMe()
        {
            Clear();
            WriteLine("=== ReadMe ===");
            WriteLine("Dreamscape Adventure adalah game RPG berbasis teks yang terinspirasi oleh game Omori. Pemain akan menjelajahi dunia mimpi yang penuh teka-teki, bertarung melawan musuh yang mencerminkan emosi dan konflik batin, serta mengalami perubahan emosi yang mempengaruhi kemampuan mereka dalam pertempuran.");
            WriteLine("This is a game where you can play, explore, and have fun!");

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\nPress any key to return to the Main Menu...");
            ResetColor();
            ReadKey(true);
            ShowMenu();
        }

        private static void ShowCredits()
        {
            Clear();
            WriteLine("=== Credits ===");
            WriteLine("Developed by Kelompok 2");
            WriteLine("Muhammad Bryan Farras");
            WriteLine("Rowen Rodotua Harahap");
            WriteLine("Musyaffa Iman Supriadi");
            WriteLine("Ryan Adidaru Excel Barnabi");
            WriteLine("===============");
            WriteLine("Added Feature by Kelompok 10");
            WriteLine("Ganendra Garda Pratama (2306250642)");
            WriteLine("Mirza Adi Raffiansyah (2306210323)");
            WriteLine("Muhammad Raihan Mustofa (2306161946)");
            WriteLine("Salim (2306204604)");

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\nPress any key to return to the Main Menu...");
            ReadKey(true);
            ResetColor();
            ShowMenu();
        }

        private static void ExitGame()
        {
            Clear();
            WriteLine("Exiting the game. Goodbye!");
            Environment.Exit(0);
        }
    }
}
