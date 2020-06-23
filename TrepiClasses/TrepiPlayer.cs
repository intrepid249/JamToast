using System;

namespace JamToast.TrepiClasses
{
    class TrepiPlayer : TrepiEntity
    {
        public bool isBlocking;
        public int Stamina { get; protected set; }
        public int MaxStamina { get; protected set; }

        public TrepiPlayer(string name) : base(0, name, 1, 1, 1)
        {
            isBlocking = false;

            Stamina = MaxStamina = 3;
        }

        public override bool MakeAttack()
        {
            HitChance += (Stamina * 10.0f) / 100;
            Stamina = Math.Clamp(Stamina - 1, 0, MaxStamina);
            
            bool attack = base.MakeAttack();
            CalculateHitChance();

            return attack;
        }

        public override bool BlockAttack()
        {
            return base.BlockAttack();
        }

        public void RechargeStamina()
        {
            Stamina = Math.Clamp(Stamina + 1, 0, MaxStamina);
        }

        int[] QueryStatAllocation(int skillPool)
        {
            int[] output = new int[3];
            // output[0] = str
            // output[1] = dex
            // output[2] = con

            output[0] = output[1] = output[2] = 0;

            int pointsRemaining = skillPool;

            while (pointsRemaining > 0)
            {
                PreviewStatChanges(0, 0, 0);

                for (bool strAssigned = false; !strAssigned;)
                {
                    Console.WriteLine("You have " + pointsRemaining + " skill points remaining");
                    Console.WriteLine("How many points will you assign to [Strength]?");
                    int tempStr;
                    if (int.TryParse(Console.ReadLine(), out tempStr) && tempStr <= pointsRemaining && tempStr >= 0)
                    {
                        output[0] = tempStr;
                        pointsRemaining -= tempStr;
                        strAssigned = true;
                    } 
                }

                Console.Clear();
                PreviewStatChanges(output[0], output[1], output[2]);

                for (bool dexAssigned = false; !dexAssigned;)
                {
                    Console.WriteLine("You have " + pointsRemaining + " skill points remaining");
                    Console.WriteLine("How many points will you assign to [Dexterity]?");
                    int tempDex;

                    

                    if (int.TryParse(Console.ReadLine(), out tempDex) && tempDex <= pointsRemaining && tempDex >= 0)
                    {
                        output[1] = tempDex;
                        pointsRemaining -= tempDex;
                        dexAssigned = true;
                    } 
                }

                Console.Clear();
                PreviewStatChanges(output[0], output[1], output[2]);

                for (bool conAssigned = false; !conAssigned;)
                {
                    Console.WriteLine("You have " + pointsRemaining + " skill points remaining");
                    Console.WriteLine("How many points will you assign to [Constitution]?");
                    int tempCon;
                    if (int.TryParse(Console.ReadLine(), out tempCon) && tempCon <= pointsRemaining && tempCon >= 0)
                    {
                        output[2] = tempCon;
                        pointsRemaining -= tempCon;
                        conAssigned = true;
                    }
                }

                Console.Clear();
                PreviewStatChanges(output[0], output[1], output[2]);

                Console.WriteLine("Are you satisfied with your point allocation?");

                if (pointsRemaining > 0)
                    Console.WriteLine("WARNING -- You have " + pointsRemaining + " points remaining. These will be lost!");

                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                int result;
                if (int.TryParse(Console.ReadLine(), out result) && result > 0 && result < 3)
                {
                    if (result == 1)
                    {
                        pointsRemaining = 0;
                    }
                    if (result == 2)
                    {
                        output[0] = output[1] = output[2] = 0;
                        pointsRemaining = skillPool;
                        Console.Clear();
                    }
                }
            }

            return output;
        }

        void PreviewStatChanges(int allocStr, int allocDex, int allocCon)
        {
            Console.WriteLine("Strength: " + (Strength + allocStr));
            Console.WriteLine("Dexterity: " + (Dexterity + allocDex));
            Console.WriteLine("Constitution: " + (Constitution + allocCon));
            Console.WriteLine();
        }

        public void DisplayCombatStats()
        {
            Console.WriteLine("Health: " + Health + "/" + MaxHealth);
            Console.WriteLine("Chance to Hit: " + (int)((HitChance * 100) + (Stamina * 10)) + "%");
            Console.WriteLine("Chance to Block: " + (int)(BlockChance * 100) + "%");

            Console.WriteLine();
            Console.WriteLine("Stamina: " + Stamina + "/" + MaxStamina);

            Console.WriteLine();
        }

        void ApplyStatChanges(int str, int dex, int con)
        {
            Strength += str;
            Dexterity += dex;
            Constitution += con;
        }

        public void LevelUp(int pointsAvailable)
        {
            Level++;

            Console.WriteLine();
            TrepiGame.WriteTitle("LEVEL UP");
            Console.WriteLine();

            Console.WriteLine("Level: " + (Level - 1) + " -> " + Level);
            TrepiGame.WaitToContinue();

            int[] stats = QueryStatAllocation(pointsAvailable);
            ApplyStatChanges(stats[0], stats[1], stats[2]);
            CalculateStats();

            Console.Clear();
            Console.WriteLine("Your stats are now:");
            PreviewStatChanges(0, 0, 0);
            Console.WriteLine();
            DisplayCombatStats();

            Console.WriteLine();
            TrepiGame.WaitToContinue();
        }
    }
}
