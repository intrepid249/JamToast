using System;
using System.Collections.Generic;

namespace JamToast.TrepiClasses
{
    class TrepiGame
    {
        bool GameRunning;

        TrepiPlayer Player;
        List<TrepiEnemy> EnemyTypes = new List<TrepiEnemy>();

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
            InitEnemyTypes();

            RunGame();
        }

        void InitPlayer()
        {
            Console.WriteLine("Enter name:");
            Player = new TrepiPlayer(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Welcome, " + Player.Name + " to an unforgiving world of pain. Please enjoy =)");
        }

        void InitEnemyTypes()
        {
            EnemyTypes.Add(new TrepiEnemy("Dorru", 10000, 1, 0.6, 1));
            EnemyTypes.Add(new TrepiEnemy("Mindless Servant", 60, 5, 0.1, 0.45));
            EnemyTypes.Add(new TrepiEnemy("Flying Pumpkin Head", 15, 20, 0.75, 0.3));
            EnemyTypes.Add(new TrepiEnemy("Wall Construct", 250, 10, 0.2, 0.2));
        }

        void RunGame()
        {
            while (GameRunning)
            {
                Console.WriteLine("You are in a forest. A thick fog permeates the atmosphere, penetrating your very soul\n[Press Enter to continue]");
                Console.ReadKey();

                TrepiEncounter encounter = new TrepiEncounter();
                encounter.SetOptions("Yes", "No", "Maybe");

                int Choice = encounter.GetAndDisplayOptionsChoice();
                Console.WriteLine("You chose option " + Choice + " which is: " + encounter.Options[Choice]);
            }
        }

        
    }
}
