using System;
using System.Collections.Generic;
using System.Drawing;

namespace JamToast.TrepiClasses
{
    class TrepiGame
    {
        static bool GameRunning;

        TrepiPlayer Player;

        public TrepiGame()
        {
            Console.Title = "Dorang";
            InitGame();
        }

        void InitGame()
        {
            Console.Clear();
            GameRunning = true;

            InitPlayer();

            RunGame();
        }

        void InitPlayer()
        {
            Console.WriteLine("Enter name:");

            Player = new TrepiPlayer(Console.ReadLine());
            Player.LevelUp(10);

            Console.WriteLine();
            Console.WriteLine("Welcome, " + Player.Name + " to an unforgiving world of pain. Please enjoy =)");
            WaitToContinue();
        }

        void RunGame()
        {           
            TrepiEncounter encounter = new TrepiEncounter(Player);
            while (GameRunning)
            {
                Console.WriteLine("You are in a forest. A thick fog permeates the atmosphere, penetrating your very soul");
                WaitToContinue();

                

                encounter.RunEncounter();
            }
        }

        public static void WaitToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("[Press Enter to continue]");
            Console.ReadKey();
            //Console.Clear();
        }

        public static void EndGame()
        {
            GameRunning = false;
        }

        public static void WriteTitle(string title)
        {
            int numOfChars = title.Length + 12; // 5 = on left and right + 2x Spaces
            string lineSeparator = "";
            for (int i = 0; i < numOfChars; i++)
            {
                lineSeparator += "=";
            }

            Console.WriteLine(lineSeparator);
            Console.WriteLine("===== " + title + " =====");
            Console.WriteLine(lineSeparator);
        }

        public static void WriteSeparator()
        {
            Console.WriteLine("\n==========================================\n");
        }
    }
}
