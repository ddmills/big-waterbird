using UnityEngine;


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
        Health health = GameManager.instance.localPlayer.GetComponent<Health>();
        health.Heal(50);
        loot.Consume();
    }
}