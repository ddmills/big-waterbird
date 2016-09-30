using System;
using UnityEngine;

[System.Serializable]
public class DrinkHealthPotionBehavior : LootBehavior
{
    public override string verb
    {
        get
        {
            return "Drink";
        }
    }

    public override string description
    {
        get
        {
            return "Drink up soldier";
        }
    }
    
    public override void Perform()
    {
        Debug.Log("Drink this thing");
    }
}
