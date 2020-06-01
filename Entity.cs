using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Entity
    {
        protected float Health, MaxHealth;
        protected float AttackDamage;
        public string Name { get; private set; }

        protected double BlockChance;
        protected double HitChance;
        

        public Entity(float maxHealth, float attackDamage, double blockChance, double hitChance)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;

            AttackDamage = attackDamage;

            BlockChance = blockChance;
            HitChance = hitChance;
            
        }

        bool MakeAttack(Entity target)
        {
            //make new random instance
            Random rand = new Random();
            if (rand.NextDouble() <= HitChance)  //if it hits
            {
                return true;    //hits
            }
            return false;       //misses
        }

        bool BlockAttack()
        {//make new random instance
            Random rand = new Random();
            if (rand.NextDouble() <= BlockChance)   //if it blocks
            {
                return true;    //blocked
            }
            return false;       //attack hit

        }


        void TakeDamage(float Ammount)
        {
            Health -= Ammount;  //deal damage
        }

      
    }
}
