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
            Console.WriteLine("You Open your eyes.");
            Console.WriteLine("What is this feeling?");
            Console.WriteLine("It's like there should be... more?");
            Console.WriteLine("You exist.");
            Console.WriteLine("And that is all...");

            WaitToContinue();

            Console.WriteLine("You hear a voice, seemingly comming from every direction.");
            Console.WriteLine("Voice: \"What is your name young child?\"");
            Console.WriteLine();
            Console.WriteLine("Enter your name:");

            player = new Player(Console.ReadLine());

            Console.WriteLine();
            if (player.Name.ToLower().Contains("dorang"))
            {
                Console.WriteLine("Voice: \"Ahh. " + player.Name + ". A mighty name!\"");
            }
            else
            {
                Console.WriteLine("Voice: \"Hmmmm... I do not like \"" + player.Name + "\".\nYou will now be called...\"\n\nVoice: \"[Dorang]\"");
            }

            

            WaitToContinue();
            Console.WriteLine("Voice: \"It wont be entertaining to watch you die instantly...\"\nVoice: \"I shall embedd you with my power to give you a fighting chance.\"\nVoice: \"For your own sake, lets hope you put on one hell of a show.\"\n\nVoice: \"Break a leg...\"");

            WaitToContinue();

            player.LevelUp(10);

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

        public static void Seperator()
        {
            Console.WriteLine("\n--=====================--\n");
        }

        public static void TitleBlock(string text)
        {
            Console.WriteLine("--======[ " + text + " ]======--");
        }
    }
}
