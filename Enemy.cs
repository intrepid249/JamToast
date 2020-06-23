using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Enemy : Entity
    {
        public Enemy(string name, int str, int dex, int con) : base( -1, name, str, dex, con)
        {
            LevelScale();
            CalculateStats();
        }

        private void LevelScale()
        {
            //TODO: make enemy stats scale with level
        }

        public void SetLvl(int value)
        {
            Lvl = value;
        }
    }
}
