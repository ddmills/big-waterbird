﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract class LootBehavior
{
    public string verb;
    public string description;

    abstract public void Perform();
}
