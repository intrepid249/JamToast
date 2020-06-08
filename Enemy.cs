using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Enemy : Entity
    {
        public Enemy(string name, float maxHealth, float attackDamage, double blockChance, double hitChance) : base(name, maxHealth, attackDamage, blockChance, hitChance)
        {
        }
    }
}
