using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class TrepiEntity
    {
        public string Name { get; private set; }                        //name of entity
        public int Level { get; protected set; }                        //level of entity


        public float Health { get; protected set; }                     //current health
        public float MaxHealth { get; protected set; }                  //amount of health the entity currently has aswell as thier max health
        public float AtkDamage { get; protected set; }                  //amount of damage you can deal
        public double BlockChance { get; protected set; }               //chance to block an attack that has hit you
        public double HitChance { get; protected set; }                 //chance to hit another entity


        //generalised stats
        public int Strength { get; protected set; }
        public int Dexterity { get; protected set; }
        public int Constitution { get; protected set; }


        public TrepiEntity(int lvl, string name, int str, int dex, int con)
        {
            Level = lvl;

            Name = name;

            Strength = str;
            Dexterity = dex;
            Constitution = con;

            CalculateStats();

        }

        protected void CalculateHealth()
        {
            //maxhealth = health
            Health = MaxHealth = Constitution * 10;
        }

        protected void CalculateAtkDamage()
        {
            //attack damage
            AtkDamage = Strength + 2 + Level;
        }

        protected void CalculateBlockChance()
        {
            //block chance
            BlockChance = Math.Clamp(Dexterity * 5.0f / 100, 0, 1);
        }

        protected void CalculateHitChance()
        {
            //hit chance
            HitChance = Math.Clamp(Dexterity * (5.0f + Dexterity + Level) / 100, 0, 1);
        }

        public void CalculateStats()  //calculate thing slike health, attack dmg, block chance, and hit chance based on CON, DEX, STR
        {
            CalculateHealth();
            CalculateAtkDamage();
            CalculateBlockChance();
            CalculateHitChance();
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
    }
}