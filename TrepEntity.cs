using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class TrepEntity
    {
        protected float Health, MaxHealth;
        protected float AtkDamage;
        protected double BlockChance, HitChance;
        public String Name { get; private set; }

        public TrepEntity(float maxHealth, float atkDamage, double blockChance, double hitChance)
        {
            Health = MaxHealth = maxHealth;
            AtkDamage = atkDamage;
            BlockChance = blockChance;
            HitChance = hitChance;
        }

        bool MakeAttack(TrepEntity target)
        {
            Random rand = new Random();
            if (rand.NextDouble() <= HitChance)
            {
                // We hit the target
                return true;
            }
            // You whiffed you dummy!!
            return false;
        }

        bool BlockAttack()
        {
            Random rand = new Random();
            if (rand.NextDouble() <= BlockChance)
            {
                // Ayy we blocked
                return true;
            }
            // Ow that hurts. They poked me
            return false;
        }

        void TakeDamage(float amount)
        {
            Health -= amount;
        }
    }
}
