using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    enum EncounterType
    {
        None,
        Enemy
    }
    class Encoutner : IEncounter
    {
        public List<String> Options { get; private set; }
        public List<Action> Results { get; private set; }



        private bool HasEnded = false;
        private EncounterType TypeOfEncounter = EncounterType.None;

        List<Enemy> enemyTypes = new List<Enemy>();
        private List<Entity> Spawnables = new List<Entity>();

        private Player player;


        public Encoutner(Player player)
        {
            Options = new List<String>();
            Results = new List<Action>();

            this.player = player;

            InitEnemyTypes();
            InitSpawnables();
        }

        void InitEnemyTypes()
        {
            enemyTypes.Add(new Enemy("One Dorru", 10000, 1, 0.6, 1));
            enemyTypes.Add(new Enemy("Mindless Servent", 60, 5, 0.1, 0.45));
            enemyTypes.Add(new Enemy("Flying Pumpkin Head", 15, 20, 0.75, 0.3));
            enemyTypes.Add(new Enemy("Wool Construct", 250, 10, 0.2, 0.2));
            enemyTypes.Add(new Enemy("Morphed Monstrocity", 300, 20, 0, 0.7));

        }

        void InitSpawnables()
        {
            foreach (var enemy in enemyTypes)
            {
                Spawnables.Add(enemy);
            }
        }

        public void SetOptions(params string[] options)
        {
            Options.Clear();
            foreach (string text in options)
            {
                Options.Add(text);
            }
        }

        public void SetResults(params Action[] results)
        {
            Results.Clear();
            foreach (Action action in results)
            {
                Results.Add(action);
            }
        }

        public int GetAndDisplayOptionsChoice()
        {
            int i = 1;
            foreach (string text in Options)
            {
                Console.WriteLine(i + ". " + text + "\n");

                ++i;
            }

            return ValidateInput();
        }



        public int ValidateInput()
        {
            bool ValidResult = false;
            int ChoiceIndex = -1;

            while (!ValidResult)
            {
                Console.WriteLine("Enter a number:\n"); //prompt the player

                //check to see if the input is a number and it is a valid number for the list of choices
                if (int.TryParse(Console.ReadLine(), out ChoiceIndex) && ChoiceIndex > 0 && ChoiceIndex <= Options.Count)
                {
                    ValidResult = true;
                    Console.WriteLine("\n====================\n\n");
                }
            }

            return ChoiceIndex - 1;
        }

        public void RunEncounter()
        {

            Entity entity = Spawnables[GetRandomIndex(Spawnables.Count)]; //choose a random entity from the list


            //check for the entity type and set all the needed text to fit the result
            if (entity is Enemy) //enemy encounter
            {
                TypeOfEncounter = EncounterType.Enemy;
                SetOptions("Fight", "Flee");
                //SetResults("You ready yourself and let out a warcry!", "You run away, embarressed by how scared you are...");
                SetResults
                    (
                    () => { Console.WriteLine("You ready yourself and let out a warcry!"); },
                    () => { Console.WriteLine("You run away, embarressed by how scared you are..."); }
                    );
                Console.WriteLine("A " + entity.Name + " rushes at you!");


                int resultIndex = GetAndDisplayOptionsChoice(); //get player input and store it
                Results[resultIndex]();
                Console.WriteLine();
                if (resultIndex == 0)
                {
                    Console.WriteLine("--========[Fight!]========--\n");
                    SetOptions("Attack", "Block", "Flee");

                    

                    

                    SetResults
                    (
                    () => 
                    {
                        List<string> hitResults = new List<string>();
                        hitResults.Add("You swing your fist and hit the " + entity.Name + "!");
                        hitResults.Add("You flail your arms towards the " + entity.Name + ". The blow lands!");
                        hitResults.Add("You Kick the " + entity.Name + "!");

                        List<string> missResults = new List<string>();
                        missResults.Add("You flail your arms out. The " + entity.Name + " moves back a little and just watches...");
                        missResults.Add("You lose balence as you push your hand forwards. You miss the " + entity.Name + ".");
                        missResults.Add("You hesitate for long enough that the " + entity.Name + "knew exactly how to dodge you.");

                        if (player.MakeAttack())
                        {
                            if (entity.BlockAttack())
                            {
                                Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)] + "\nBut they Blocked it.\n\n====================\n\n");
                            }
                            else
                            {
                                Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)] + "\n\n====================\n\n");
                                entity.TakeDamage(player.AttackDamage);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine(missResults[GetRandomIndex(missResults.Count)] + "\n\n====================\n\n");
                        }
                    },
                    () => 
                    {
                        Console.WriteLine("You Brace yourself!");
                        player.isBlocking = true;
                    },
                    () =>
                    {
                        Console.WriteLine("You run away with your tail between your legs\n\n====================\n\n");
                        HasEnded = true;
                        Encoutner encounter = new Encoutner(player);
                        encounter.RunEncounter();
                    }

                    );

                    while (!HasEnded)
                    {
                        Console.WriteLine("========[ Player Health: " + player.Health + " ]");
                        
                        if (player.Health > 0)
                        {
                            if (entity.Health > 0)
                            {
                                int combatChoice = GetAndDisplayOptionsChoice();
                                Results[combatChoice]();

                                if (entity.MakeAttack())
                                {
                                    if (player.isBlocking)
                                    {
                                        if (player.BlockAttack())
                                        {
                                            Console.WriteLine("The " + entity.Name + " attacked you but you blocked the hit!\n\n");
                                        }
                                        else if(!player.BlockAttack())
                                        {
                                            Console.WriteLine("You tried to block as the " + entity.Name + " attacked, but they managed to hit you!\n\n");
                                            player.TakeDamage(entity.AttackDamage);
                                        }
                                        player.isBlocking = false;
                                    }
                                    else if (!player.isBlocking)
                                    {
                                        Console.WriteLine("The " + entity.Name + " attacked you and hit!\n\n");
                                        player.TakeDamage(entity.AttackDamage);
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("The " + entity.Name + " completely missed you\n\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n\nYou killed the " + entity.Name + ".");
                                Game.WaitToContinue();
                                Encoutner encounter = new Encoutner(player);
                                encounter.RunEncounter();
                                HasEnded = true;
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\n  [ You have died...]");
                            Game.EndGame(); //TODO things will break at this point.. for now. gotta properly restart game later
                        }

                    }
                    
                        
                    
                }
                else if (resultIndex == 1)
                {
                    HasEnded = true;
                    Game.WaitToContinue();
                    Encoutner encounter = new Encoutner(player);
                    encounter.RunEncounter();
                }
            }


        }

        private int GetRandomIndex(int max)
        {
            Random ran = new Random();
            return ran.Next(0, max);
        }

    }
}
