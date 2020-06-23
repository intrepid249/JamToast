using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Spawnable
    {

        public string Name { get; private set; }

        public Spawnable(string name)
        {
            Name = name;
        }
    }
}
