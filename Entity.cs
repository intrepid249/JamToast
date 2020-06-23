using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Entity : Spawnable
    {
                               
        public int Lvl { get; protected set; }                            //level of entity


        public float Health { get; protected set; }                     //current health
        public float MaxHealth { get; protected set; }                  //amount of health the entity currently has aswell as thier max health
        public float AttackDamage { get; protected set; }               //amount of damage you can deal
        public double BlockChance { get; protected set; }               //chance to block an attack that has hit you
        public double HitChance { get; protected set; }                 //chance to hit another entity
        



        //generalised stats
        public int Str { get; protected set; }
        public int Dex { get; protected set; }
        public int Con { get; protected set; }


        public Entity(int lvl, string name, int str, int dex, int con) : base(name)
        {
            Lvl = lvl;
            
            Str = str;
            Dex = dex;
            Con = con;

            CalculateStats();

        }

        

        public void CalculateStats()  //calculate thing slike health, attack dmg, block chance, and hit chance based on CON, DEX, STR
        {
            //maxhealth = health
            MaxHealth = Health = Con * 10;
            

            //attack damage
            AttackDamage = Str + 2 + Lvl;

            //block chance
            BlockChance = Math.Clamp(Dex * 5.0f / 100, 0, 1);

            //hit chance
            HitChance = Math.Clamp(Dex * (5.0f + Dex + Lvl) / 100, 0, 1);

        }

        protected void CaluculateHealth()
        {
            //maxhealth = health
            MaxHealth = Health = Con * 10;
        }

        protected void CalculateAttackDamage()
        {
            //attack damage
            AttackDamage = Str + 2 + Lvl;
        }

        protected void CalculateBlockChance()
        {
            //block chance
            BlockChance = Math.Clamp(Dex * 5.0f / 100, 0, 1);
        }
        protected void CalculateHitChance()
        {
            //hit chance
            HitChance = Math.Clamp(Dex * (5.0f + Dex + Lvl) / 100, 0, 1);
        }


        public virtual bool MakeAttack()
        {
            //make new random instance
            Random rand = new Random();
            if (rand.NextDouble() <= HitChance)  //if it hits
            {
                return true;    //hits
            }
            return false;       //misses
        }

        public virtual bool BlockAttack()
        {
            //make new random instance
            Random rand = new Random();
            if (rand.NextDouble() <= BlockChance)   //if it blocks
            {
                return true;    //blocked
            }
            return false;       //attack hit

        }


        public void TakeDamage(float Ammount)
        {
            Health -= Ammount;  //deal damage
        }

        public void AddHealth(float Ammount)
        {
            Health += Ammount; //add health
        }
      
    }
}
