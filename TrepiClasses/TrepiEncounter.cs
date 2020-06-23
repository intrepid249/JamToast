using System;
using System.Collections.Generic;

namespace JamToast.TrepiClasses
{
    enum TrepiEncounterType
    {
        None,
        Enemy,
        Consumable
    }

    class TrepiEncounter : ITrepiEnounter
    {
        public List<string> Options { get; private set; }
        public List<Action> Results { get; private set; }


        private List<TrepiEnemy> Enemies = new List<TrepiEnemy>();
        private List<TrepiConsumable> Consumables = new List<TrepiConsumable>();
        private List<TrepiLoreEntity> LoreEntities = new List<TrepiLoreEntity>();
        private List<TrepiSpawnable> Spawnables = new List<TrepiSpawnable>();

        private int PreviousEntity = -1;

        private bool HasEnded = false;
        private TrepiEncounterType TypeOfEncounter = TrepiEncounterType.None;
        private TrepiPlayer Player;

        public TrepiEncounter(TrepiPlayer player)
        {
            Player = player;

            InitEnemies();
            InitConsumables();
            InitLoreEntities();
            InitSpawnables();
            
            Options = new List<string>();
            Results = new List<Action>();
        }

        void InitEnemies()
        {
            Enemies.Add(new TrepiEnemy("Mindless Servant", 1, 2, 3));
            Enemies.Add(new TrepiEnemy("Flying Pumpkin Head", 2, 1, 4));
            Enemies.Add(new TrepiEnemy("Wall Construct", 0, 1, 10));
            Enemies.Add(new TrepiEnemy("Morphed Monstrocity", 4, 3, 1));
            Enemies.Add(new TrepiEnemy("Mound of Bones", 4, 2, 6)); // Pile of Struggling bones -> Cobbled Skeleton -> Skeleton
        }

        void InitConsumables()
        {
            TrepiConsumable HealthPot = new TrepiConsumable("Health Potion", 15);
            HealthPot.AddDiscoveredFlairText("A strange vial holding bright red liquid rolls out from under a bush...\nWhat do you want to do?");
            HealthPot.AddDiscoveredFlairText("Something glints in the leaves at the corner of your eyes. Upon investigation it is a small round bottle holding a bright red liquid");
            HealthPot.AddConsumedFlairText("You feel a strange tingle as the liquid enters your body, soothing your aches and pains");
            HealthPot.AddConsumedFlairText("As you drink the liquid, it feels as though life is entering your body, empowering you and revitalising your wounds");
            HealthPot.AddDeclinedFlairText("The crimson liquid makes you feel somewhat unsettled. You don't feel good about consuming the strange bubbling liquid");
            HealthPot.AddDeclinedFlairText("Science was never your strong suit, neither was your curiosity. Better to let sleeping cats lie");
            Consumables.Add(HealthPot);
        }

        void InitLoreEntities()
        {
            LoreEntities.Add(new TrepiLoreEntity("As time passes, the noise of the forest seems to die down... " +
                "Every snap of a twig sounds deafening in the silence"));
            LoreEntities.Add(new TrepiLoreEntity("An ungodly shriek echoes through the dense forest, sending chills down your spine. " +
                "Twigs snap in the near distance, just beyond your sight"));
        }

        void InitSpawnables()
        {
            foreach (TrepiEntity enemy in Enemies)
            {
                Spawnables.Add(enemy);
            }

            foreach (TrepiConsumable consumable in Consumables)
            {
                Spawnables.Add(consumable);
            }

            foreach (TrepiLoreEntity loreEntity in LoreEntities)
            {
                Spawnables.Add(loreEntity);
            }
        }

        public void SetOptions(params string[] options)
        {
            // Clear the Options list of any data that may previously exist
            Options.Clear();

            // Append the elements of the input parameter list to the Options field
            for (int i = 0; i < options.Length; i++)
            {
                Options.Add(options[i]);
            }
        }

        public void SetResults(params Action[] results)
        {
            // Clear the Results list of any data that may previously exist
            Results.Clear();

            // Append the elements of the input parameter list to the Results field
            for (int i = 0; i < results.Length; i++)
            {
                Results.Add(results[i]);
            }
        }

        public int GetAndDisplayOptionsChoice()
        {
            // Make sure the choices are on a new line
            Console.WriteLine();

            // Display the choices to the console
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine((i+1) + ". " + Options[i]);
            }

            bool validResult = false;
            int choiceIndex = -1;

            // Hang the input in a while loop to check for valid input
            while (!validResult)
            {
                Console.WriteLine("Type the number corresponding to the choice:");

                // Check to make sure the input in a number, and the number corresponds to an index of an option in the list
                if (int.TryParse(Console.ReadLine(), out choiceIndex) && choiceIndex > 0 && choiceIndex <= Options.Count)
                {
                    validResult = true;
                }
            }

            TrepiGame.WriteSeparator();
            // Return the index of the option chosen
            return choiceIndex - 1;
        }

        public void RunEncounter() 
        {
            // Choose a random entity from the list of spawnable entities for the Encounter
            int entityIdx = GetRandomIndex(Spawnables.Count);
            Console.WriteLine("EntityIdx: " + entityIdx + " PreviousEntity: " + PreviousEntity);
            TrepiSpawnable entity = Spawnables[entityIdx];

            if (PreviousEntity != -1)
            {
                Console.WriteLine("Checking for the same generated entity...");
                int newIdx = -1;
                while (PreviousEntity == entityIdx)
                {
                    newIdx = GetRandomIndex(Spawnables.Count);
                    Console.WriteLine("newIdx: " + newIdx);
                    entity = Spawnables[newIdx];

                    Console.WriteLine("Setting PreviousEntity to newIdx: " + newIdx);
                    PreviousEntity = newIdx;
                }
            }

            // Check for the entity type and set appropriate options and results
            if (entity is TrepiEnemy)
            {
                TrepiEnemy enemy = (TrepiEnemy)entity;
                TypeOfEncounter = TrepiEncounterType.Enemy;

                Console.WriteLine("A " + entity.Name + " charges at you from the shadows, smashing branches out of its path!");

                Console.WriteLine();
                Console.WriteLine(Player.Name + ": " + Player.Health);
                Console.WriteLine();

                SetOptions("Fight", "Flee");
                SetResults(
                    // Fight
                    () => {Console.WriteLine("You raise your fist in anger and declare war!");}, 
                    // Flee
                    () => {Console.WriteLine("You run away, crying like a baby...");});

                int resultIndex = GetAndDisplayOptionsChoice();
                Results[resultIndex]();
                Console.WriteLine();
                if (resultIndex == 0)
                {
                    // If we choose to fight
                    SetOptions("Attack", "Block", "Flee");

                    SetResults(
                        // Attack
                        () => 
                        {
                            List<string> hitResults = new List<string>();
                            hitResults.Add("You swing a fist at the " + entity.Name + " and hit them square in the face!");
                            hitResults.Add("Poking a finger in their general direction, the " + entity.Name + " impales themself on your finger!");
                            hitResults.Add("Didn't anyone tell you pointing was rude? " + entity.Name + " just stabbed themself with your finger!");

                            List<string> missResults = new List<string>();
                            missResults.Add("You fix " + entity.Name + " with a deathly stare... Too bad you're crosseyed...");
                            missResults.Add("You trip over a leaf trying to throw a punch... Nice work =)");

                            if (Player.MakeAttack() && !enemy.BlockAttack())
                            {
                                Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)]);
                                Console.WriteLine();
                                enemy.TakeDamage(Player.AtkDamage);
                            } else
                            {
                                Console.WriteLine(missResults[GetRandomIndex(missResults.Count)]);
                                Console.WriteLine();
                            }
                        },
                        // Block
                        () => 
                        {
                            Player.isBlocking = true;
                            Player.RechargeStamina(); // TODO: Add this function to let the player recharge stamina
                            // Since the stamina wouldn't regenerate if the BlockAttack returns false in the if conditions

                            Console.WriteLine("You brace yourself for an attack!");
                            Console.WriteLine();
                        },
                        // Flee
                        () =>
                        {
                            Console.WriteLine("You run away, crying like a baby...");
                            HasEnded = true;
                            StartNewEncounter(entityIdx);
                        }

                    );

                    while (!HasEnded) 
                    {
                        if (Player.Health > 0)
                        {

                            if (enemy.Health > 0)
                            {
                                Console.Clear();
                                TrepiGame.WriteTitle("BATTLE");

                                Console.WriteLine(Player.Name + " VS " + entity.Name);
                                TrepiGame.WriteSeparator();

                                Console.WriteLine();
                                Console.WriteLine(Player.Name + ":");
                                Player.DisplayCombatStats();
                                TrepiGame.WriteSeparator();

                                int combatChoice = GetAndDisplayOptionsChoice();
                                Results[combatChoice]();

                                TrepiGame.WaitToContinue();

                                if (enemy.MakeAttack())
                                {
                                    List<string> hitResults = new List<string>();
                                    hitResults.Add("The " + entity.Name + " swings a fist at you and hits you square in the face!");
                                    hitResults.Add("The " + entity.Name + " pokes a finger in your general direction, and you impale yourself on their finger!");
                                    hitResults.Add("Didn't anyone tell the " + entity.Name + " pointing was rude? You just stabbed yourself with their finger!");

                                    if (Player.isBlocking)
                                    {
                                        if (Player.BlockAttack())
                                        {
                                            // If the monsters attack is blocked by the player
                                            Console.WriteLine("You successfully block the attack, taking no damage");
                                            Console.WriteLine();
                                        } else
                                        {
                                            // If the monster hits the player while they are blocking
                                            Console.WriteLine("You try to block the attack, but alas!");
                                            Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)]);
                                            Console.WriteLine();
                                            Player.TakeDamage(enemy.AtkDamage);
                                        }
                                        Player.isBlocking = false;
                                    } else
                                    {
                                        // if the monster hits the player
                                        Console.WriteLine(hitResults[GetRandomIndex(hitResults.Count)]);
                                        Console.WriteLine();
                                        Player.TakeDamage(enemy.AtkDamage);
                                    }
                                } else
                                {
                                    // If the monster misses the player
                                    List<string> missResults = new List<string>();
                                    missResults.Add("The " + entity.Name + " fixes you with a deathly stare... Too bad they're crosseyed...");
                                    missResults.Add("The " + entity.Name + " trips over a leaf trying to throw a punch... Nice work =)");

                                    Console.WriteLine(missResults[GetRandomIndex(missResults.Count)]);
                                    Console.WriteLine();
                                }

                                TrepiGame.WaitToContinue();
                            } else
                            {
                                // If the player kills the monster
                                List<string> victories = new List<string>();
                                victories.Add("You have slain the " + entity.Name + "!");

                                Console.WriteLine(victories[GetRandomIndex(victories.Count)]);
                                StartNewEncounter(entityIdx);
                            }
                        } else
                        {
                            // If the player gets killed
                            Console.WriteLine();
                            Console.WriteLine(Player.Name + ": 0");
                            Console.WriteLine();

                            List<string> defeats = new List<string>();
                            defeats.Add("You have been killed by a " + entity.Name + ". I knew I should've chosen somebody else...");

                            Console.WriteLine(defeats[GetRandomIndex(defeats.Count)]);

                            TrepiGame.WaitToContinue();

                            HasEnded = true;
                            TrepiGame.EndGame();
                        }
                        
                    }
                }
                else if (resultIndex == 1)
                {
                    // If we choose to flee
                    HasEnded = true;
                    StartNewEncounter(entityIdx);
                }
            }

            if (entity is TrepiConsumable)
            {
                TrepiConsumable item = (TrepiConsumable)entity;

                Console.WriteLine(item.GetRandomDiscoveredFlair());

                SetOptions("Consume", "Leave Alone");
                SetResults(
                    // Consume
                    () => 
                    {
                        Console.WriteLine(item.GetRandomConsumedFlair());
                        if (item is ITrepiHealthItem)
                        {
                            Console.WriteLine("[You gain " + item.NutritionValue + "health]");
                            item.Heal(Player);
                        }

                        StartNewEncounter(entityIdx);
                    },
                    // Leave Alone
                    () => 
                    {
                        Console.WriteLine(item.GetRandomDeclinedFlair());
                        StartNewEncounter(entityIdx);
                    }
                    );

                int resultIndex = GetAndDisplayOptionsChoice();
                Results[resultIndex]();
            }

            if (entity is TrepiLoreEntity)
            {
                ((TrepiLoreEntity)entity).ShowLore();
                StartNewEncounter(entityIdx);
            }
        }

        private int GetRandomIndex(int Max)
        {
            Random rand = new Random();
            return rand.Next(0, Max);
        }

        private void StartNewEncounter(int previousEntityIdx)
        {
            Console.WriteLine("Setting PreviousEntity to entityIdx: " + previousEntityIdx);
            PreviousEntity = previousEntityIdx;

            TrepiGame.WaitToContinue();

            RunEncounter();
        }
    }
}
