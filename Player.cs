﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Player : Entity
    {
        public bool isBlocking;
        public Player(string name) : base(name, 100, 10, 0.2, 0.35)
        {
            isBlocking = false;
        }
    }
}
