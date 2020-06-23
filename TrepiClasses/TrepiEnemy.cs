using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast.TrepiClasses
{
    class TrepiEnemy : TrepiEntity
    {
        public TrepiEnemy(string name, int str, int dex, int con) : base(-1, name, str, dex, con)
        {
            LevelScale();
            CalculateStats();
        }

        public void SetLevel(int value)
        {
            Level = value;
        }

        private void LevelScale()
        {
            // TODO: Calculate str|dex|con to scale with the level
        }
    }
}
