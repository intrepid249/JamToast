using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast.TrepiClasses
{
    class TrepiLoreEntity : TrepiSpawnable, ITrepiLore
    {
        public TrepiLoreEntity(string lore) : base(lore)
        {

        }

        public void ShowLore()
        {
            // Because we are dumb, we are using Name to store the lore text of the instance
            Console.WriteLine(Name);
        }
    }
}
