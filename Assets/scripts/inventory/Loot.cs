using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewLoot", menuName = "Loot Item", order = 1)]
public class Loot : ScriptableObject {
    public string title;
    public float weight = 1f;
    public List<LootBehavior> behaviors;
    public bool stackable = true;
    public Sprite sprite;
    public GameObject gameObject;
    private InventorySlot slot;

    public void AddToInventorySlot(InventorySlot slot)
    {
        this.slot = slot;
    }

    public void ResetBehaviors()
    {
        foreach (LootBehavior behavior in behaviors)
        {
            behavior.loot = this;
        }
    }

    public void Consume()
    {
        slot.RemoveItem(this);
    }

    public GameObject Drop()
    {
        Transform bulletSpawn = GameManager.instance.localPlayer.GetComponent<PlayerController>().bulletSpawn;
        GameObject instance = (GameObject) Instantiate(gameObject, bulletSpawn.position, bulletSpawn.rotation);
        slot.RemoveItem(this);
        NetworkServer.Spawn(instance);
        return instance;
    }
}
