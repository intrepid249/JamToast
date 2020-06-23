using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    enum EncounterType
    {
        None,
        Enemy,
        Consumable
    }
    class Encoutner : IEncounter
    {
        public List<String> Options { get; private set; }
        public List<Action> Results { get; private set; }



        private bool HasEnded = false;
        private EncounterType TypeOfEncounter = EncounterType.None;

        private List<Enemy> Enemies = new List<Enemy>();
        private List<Consumable> Consumables = new List<Consumable>();
        private List<Spawnable> Spawnables = new List<Spawnable>();

        private Player player;


        public Encoutner(Player player)
        {
            Options = new List<String>();
            Results = new List<Action>();

            this.player = player;

            InitEnemies();
            InitConsumables();
            InitSpawnables();
        }

        void InitEnemies()
        {
            
            Enemies.Add(new Enemy("Mindless Servent", 1, 2, 3));
            //Enemies.Add(new Enemy("Flying Pumpkin Head", 2, 4, 1));
            //Enemies.Add(new Enemy("Wool Construct", 0, 1, 10));
            //Enemies.Add(new Enemy("Morphed Monstrocity", 4, 3, 1));
            //Enemies.Add(new Enemy("Mound of Bones", 4, 2, 6)); //TODO: potentially make the name change at higher levels

        }

        void InitConsumables()
        {

            Consumable HealthPot = new Consumable("Health Potion", 15);
            HealthPot.AddDiscoveredFlareText("You spot something shining in the corner of your eyes.\nAfter digging around in a bush for a bit, you find a glass phial full of red liquid!\nPopping off the old cork, a rancid smell fills your nose.\nShould you drink it?");
            HealthPot.AddDiscoveredFlareText("As you walk through the dense foliage, you come accross a small pedestal.\nSitting on top is a Glass phail full of red liquid.\nTaking it off the pedestal, you notice that the liquid is thick and lumpy.\nPerhaps you chould drink it?");
            HealthPot.AddConsumedFlareText("It Tastes disguasting... But you are pleasantly suprised.\nAs the thick red liquid slides down your throat, you feel oddly energised and refreshed.\n");
            HealthPot.AddConsumedFlareText("You block your nose to make drinking it easier... It worked up until you felt the clumpy liquid touch your tounge.\nYou instantly feel more refreshed and energised after drinking it!");
            HealthPot.AddDeclinedFlareText("You decide it might be better for your health not to drink a strange, clumpy, red liquid that smells like sweat...");
            HealthPot.AddDeclinedFlareText("After some careful consideration... You decide not to risk drinking a mysterious liquid");
            Consumables.Add(HealthPot);



        }

        void InitSpawnables()
        {
            foreach (var enemy in Enemies)
            {
                Spawnables.Add(enemy);
            }
            foreach(Consumable consumable in Consumables)
            {
                Spawnables.Add(consumable);
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
            Console.WriteLine();

            int i = 1;
            foreach (string text in Options)
            {
                Console.WriteLine(i + ". " + text);

                ++i;
            }
            Console.WriteLine();
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
                    Game.Seperator();
                }
            }

            return ChoiceIndex - 1;
        }

        public void RunEncounter()
        {

            Spawnable entity = Spawnables[GetRandomIndex(Spawnables.Count)]; //choose a random entity from the list


            //check for the entity type and set all the needed text to fit the result
            if (entity is Enemy) //enemy encounter
            {
                Enemy enemy = (Enemy)entity;

                TypeOfEncounter = EncounterType.Enemy;
                SetOptions("Fight", "Flee");
               
                SetResults
                    (
                    //fight
                    () => { Console.WriteLine("You ready yourself and let out a warcry!"); },
                    //flee
                    () => { Console.WriteLine("You run away, embarressed by how scared you are..."); }
                    );
                Console.WriteLine("A " + entity.Name + " rushes at you!");


                int resultIndex = GetAndDisplayOptionsChoice(); //get player input and store it
                Results[resultIndex]();
                Console.WriteLine();
                if (resultIndex == 0)
                {
                    Game.TitleBlock("Fight!");
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
                        missResults.Add("You hesitate for long enough that the " + entity.Name + " knew exactly how to dodge you.");

                        if (player.MakeAttack())
                        {
                            if (enemy.BlockAttack())
                            {
                                Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)] + "\nBut they Blocked it.");
                                
                            }
                            else
                            {
                                Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)]);
                                enemy.TakeDamage(player.AttackDamage);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine(missResults[GetRandomIndex(missResults.Count)]);
                        }
                    },
                    () => 
                    {
                        Console.WriteLine("You Brace yourself!");
                        player.isBlocking = true;
                        player.RegenerateStamina();
                    },
                    () =>
                    {
                        Console.WriteLine("You run away with your tail between your legs");
                        Game.Seperator();
                        HasEnded = true;
                        Encoutner encounter = new Encoutner(player);
                        encounter.RunEncounter();
                    }

                    );

                    while (!HasEnded)
                    {
                        Console.WriteLine("========[ Player Health: " + player.Health + " ]");
                        Console.WriteLine("========[ Player Stamina: " + player.Stamina + " ]");

                        if (player.Health > 0)
                        {
                            if (enemy.Health > 0)
                            {
                                int combatChoice = GetAndDisplayOptionsChoice();
                                Results[combatChoice]();

                                if (enemy.MakeAttack())
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
                                            player.TakeDamage(enemy.AttackDamage);
                                        }
                                        player.isBlocking = false;
                                    }
                                    else if (!player.isBlocking)
                                    {
                                        Console.WriteLine("The " + entity.Name + " attacked you and hit!\n\n");
                                        player.TakeDamage(enemy.AttackDamage);
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("The " + entity.Name + " completely missed you\n\n");
                                }
                            }
                            else
                            {
                                Game.Seperator();
                                Console.WriteLine("\nYou killed the " + entity.Name + ".");
                                Game.WaitToContinue();
                                Encoutner encounter = new Encoutner(player);
                                encounter.RunEncounter();
                                HasEnded = true;
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\n    [ You have died...]");
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

            if(entity is Consumable)
            {
                Consumable item = (Consumable)entity;

                TypeOfEncounter = EncounterType.Consumable;

                Console.WriteLine(item.GetRandomDiscoveredFlare());

                SetOptions("Consume Item", "Do not Consume Item");

                SetResults
                    (
                    //consume item
                    () => 
                    {
                        Console.WriteLine(item.GetRandomConsumedFlare());
                        if(item is IHealthItem)
                        {
                            
                            item.heal(player);

                            Game.WaitToContinue();
                            Encoutner encounter = new Encoutner(player);
                            encounter.RunEncounter();
                        }
                    
                    },
                    //do not consume
                    () => 
                    {
                        Console.WriteLine(item.GetRandomDeclinedFlare());
                        Game.WaitToContinue();
                        Encoutner encounter = new Encoutner(player);
                        encounter.RunEncounter();

                    }
                    
                    );
                int resultIndex = GetAndDisplayOptionsChoice(); //get player input and store it
                Results[resultIndex]();

            }


        }

        private int GetRandomIndex(int max)
        {
            Random ran = new Random();
            return ran.Next(0, max);
        }

    }
}
