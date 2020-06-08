using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast.TrepiClasses
{
    class TrepiEnemy : TrepEntity
    {
        public TrepiEnemy(string name, float maxHealth, float atkDamage, double blockChance, double hitChance) : base(name, maxHealth, atkDamage, blockChance, hitChance)
        {

        }
    }
}
