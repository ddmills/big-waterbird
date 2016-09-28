using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
    private bool owned = false;

    public string title;
    public int weight = 1;
    public bool stackable = true;
    public Sprite sprite;
    public GameObject prefab;

    public bool Equals(InventoryItem other)
    {
        return other.title == title;
    }
}
