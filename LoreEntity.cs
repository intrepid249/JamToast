using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class LoreEntity : Spawnable,ILore
    {
        public LoreEntity(string lore) : base(lore)
        {

        }

        public void ShowLore()
        {
            Console.WriteLine(Name); //name = lore
        }

    }
}
