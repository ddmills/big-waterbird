using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public abstract class LootBehavior
{
    public abstract string verb { get; }
    public abstract string description { get; }

    abstract public void Perform();
}
