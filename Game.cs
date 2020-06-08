using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Game
    {
        Player player;
        public static bool GameRunning;

        
        

        public Game()
        {
            Console.Title = "Dorang";
            InitGame();
        }



        void InitGame()
        {
            GameRunning = true;
            InitPlayer();
            
            RunGame();
        }

        

        void InitPlayer()
        {
            Console.WriteLine("What is your name young child?");
            player = new Player(Console.ReadLine());

            Console.WriteLine();
            if (player.Name.ToLower().Contains("dorang"))
            {
                Console.WriteLine("Ahh. " + player.Name + " is a mighty name!");
            }
            else
            {
                Console.WriteLine("Hmmmm... I do not like the name of \"" + player.Name + "\".\nYou will now be called Dorang.");
            }

            WaitToContinue();
        }

        void RunGame()
        {
            Encoutner encounter = new Encoutner(player);
            while (GameRunning)
            {
                Console.WriteLine("You are in a forest. A thick fog permeates the atmosphere. Penetrating your very soul.");
                WaitToContinue();

                
                encounter.RunEncounter();



                EndGame();
            }
        }

       

        static public void WaitToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("--==[Press Enter to Continue]==--");
            Console.WriteLine("=================================");
            Console.ReadKey();
            Console.Clear();
        }

        public static void EndGame() //TODO make it restart properly
        {
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("--==[Press Enter to Restart Game]==--");
            Console.WriteLine("=====================================");
            Console.ReadKey();

            GameRunning = false;
        }

        
    }
}
