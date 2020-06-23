using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Player : Entity
    {
        public bool isBlocking;

        public int MaxStamina { get; protected set; }                   //max stamina, can effect things like hit chance
        public int Stamina { get; protected set; }                      //current stamina

        //stats that effect player health, damage, etc.

        public Player(string name) : base( 0, name, 1, 1, 1)
        {
            isBlocking = false;
            Stamina = MaxStamina = 3;
        }

        public void LevelUp(int points)
        {
            ++Lvl;
            Console.WriteLine("---===== [YOU HAVE LEVELED UP!] =====---");
            Console.WriteLine((Lvl - 1) + " --> " + Lvl);
            Game.WaitToContinue();
            int[] stats = QueryStatsAllocation(points);
            ApplyStatChanges(stats[0], stats[1], stats[2]);
            CalculateStats();
            Console.Clear();
            Console.WriteLine("---===== [YOUR NEW STATS ARE] =====---");
            Console.WriteLine("[Level " + Lvl + "]");
            PreviewPlayerStats(0,0,0);
            Console.WriteLine();
            Console.WriteLine("Health: " + Health);
            Console.WriteLine("Attack Damage: " + AttackDamage);
            Console.WriteLine("Hit Chance: " + (int)(HitChance * 100) + "%");
            Console.WriteLine("Block Chance: " + (int)(BlockChance * 100) + "%"); 

            Game.WaitToContinue();
        }

        int[] QueryStatsAllocation(int skillpool) //ask the player to add skillpoints to stats
        {
            int[] output = new int[3];
            //output[0] = str
            //output[1] = dex
            //output[2] = con

            int skillPointsRemaining = skillpool;

            while (skillPointsRemaining > 0)
            {
                for (bool strAssigned = false; !strAssigned;)
                {
                    PreviewPlayerStats(output[0], output[1], output[2]);
                    Console.WriteLine("You have [" + skillPointsRemaining + "] remaining.\nHow many would you like to put into [Strength]?");
                    int tempSTR;
                    if (int.TryParse(Console.ReadLine(), out tempSTR) && tempSTR <= skillPointsRemaining && tempSTR >= 0)
                    {
                        output[0] = tempSTR;
                        skillPointsRemaining -= tempSTR;
                        strAssigned = true;
                    }
                    
                    
                    

                }

                for (bool dexAssigned = false; !dexAssigned;)
                {
                    PreviewPlayerStats(output[0], output[1], output[2]);
                    Console.WriteLine("You have [" + skillPointsRemaining + "] remaining.\nHow many would you like to put into [Dexterity]?");
                    int tempDEX;
                    if (int.TryParse(Console.ReadLine(), out tempDEX) && tempDEX <= skillPointsRemaining && tempDEX >= 0)
                    {
                        output[1] = tempDEX;
                        skillPointsRemaining -= tempDEX;
                        dexAssigned = true;
                    }

                    
                }

                for (bool conAssigned = false; !conAssigned;)
                {
                    PreviewPlayerStats(output[0], output[1], output[2]);
                    Console.WriteLine("You have [" + skillPointsRemaining + "] remaining.\nHow many would you like to put into [Constitution]?");
                    int tempCON;
                    if (int.TryParse(Console.ReadLine(), out tempCON) && tempCON <= skillPointsRemaining && tempCON >= 0)
                    {
                        output[2] = tempCON;
                        skillPointsRemaining -= tempCON;
                        conAssigned = true;
                    }

                    
                }

                Console.WriteLine();
                PreviewPlayerStats(output[0], output[1], output[2]);

                Console.WriteLine("Are you satisfied?");
                if (skillPointsRemaining > 0) Console.WriteLine("|Warning| - You still have " + skillPointsRemaining + " points remaining, These will be lost!");
                
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                int result;
                if (int.TryParse(Console.ReadLine(), out result) && result > 0 && result < 3)
                {
                    if (result == 1)
                    {
                        skillPointsRemaining = 0;
                    }
                    if (result == 2)
                    {
                        output[0] = output[1] = output[2] = 0;
                        skillPointsRemaining = skillpool;
                        Console.Clear();
                    }
                }
            }


            return output;
        }

        private void ApplyStatChanges(int str, int dex, int con)
        {
            Str += str;
            Dex += dex;
            Con += con;
        }



        public void PreviewPlayerStats(int allocatedSTR, int allocatedDEX, int allocatedCON)
        {
            Console.WriteLine("STR: " + (Str + allocatedSTR) + " || DEX: " + (Dex + allocatedDEX) + " || CON: " + (Con + allocatedCON));
        }


        public override bool MakeAttack()
        {
            bool Attack = base.MakeAttack();
            HitChance += Stamina * 10.0f / 100;
            --Stamina;
            Math.Clamp(Stamina, 0, MaxStamina);
            CalculateHitChance();
            return Attack;
        }

        public override bool BlockAttack()
        {
            ++Stamina;
            Math.Clamp(Stamina, 0, MaxStamina);

            return base.BlockAttack();
        }
    }
}
