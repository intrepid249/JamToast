using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Game
    {
        Player player;
        bool GameRunning;

        List<Enemy> enemyTypes;
        

        public Game()
        {
            Console.Title = "Dorang";
            InitGame();
        }



        void InitGame()
        {
            GameRunning = true;
            InitPlayer();
            InitEnemyTypes();
            RunGame();
        }

        void InitEnemyTypes()
        {
            enemyTypes.Add(new Enemy("dorru", 10000, 1, 0.6, 1));
            enemyTypes.Add(new Enemy("Mindless Servent", 60, 5, 0.1, 0.45));
            enemyTypes.Add(new Enemy("Flying Pumpkin Head", 15, 20, 0.75, 0.3));
            enemyTypes.Add(new Enemy("Wool Construct", 250, 10, 0.2, 0.2));

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
        }

        void RunGame()
        {
            while (GameRunning)
            {
                Console.WriteLine("You are in a forrest. A thick fog permeates the atmosphere. Penetrating your very soul.\n[Press Any Key to Continue]");
                Console.ReadKey();
                
            }
        }

        
    }
}
