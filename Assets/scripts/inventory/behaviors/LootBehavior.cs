using UnityEngine;


public abstract class LootBehavior : MonoBehaviour
{
    public abstract string verb { get; }
    public abstract string description { get; }
    public Loot loot;

    abstract public void Perform();
}
