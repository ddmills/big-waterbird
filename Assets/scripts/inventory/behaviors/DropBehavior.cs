using UnityEngine;


public class DropBehavior : LootBehavior
{
    public override string verb
    {
        get
        {
            return "Drop";
        }
    }

    public override string description
    {
        get
        {
            return "Littering is bad";
        }
    }

    public override void Perform()
    {
        Debug.Log("Drop it like it's hot");
    }
}
