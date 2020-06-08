using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Entity
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }          //amount of health the entity currently has aswell as thier max health
        public float AttackDamage { get; protected set; }               //amount of damage you can deal
        public string Name { get; private set; }    //name of entity

        public double BlockChance { get; protected set; }               //chance to block an attack that has hit you
        public double HitChance { get; protected set; }                 //chance to hit another entity


        public Entity(string name, float maxHealth, float attackDamage, double blockChance, double hitChance)
        {
            Name = name;

            //set maxhealth and use that to set current health
            MaxHealth = maxHealth;
            Health = MaxHealth;

            //set attack damage
            AttackDamage = attackDamage;

            //set block and hit chance
            BlockChance = blockChance;
            HitChance = hitChance;
            
        }

        public bool MakeAttack()
        {
            //make new random instance
            Random rand = new Random();
            if (rand.NextDouble() <= HitChance)  //if it hits
            {
                return true;    //hits
            }
            return false;       //misses
        }

        public bool BlockAttack()
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
