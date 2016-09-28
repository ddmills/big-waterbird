using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour {
    private List<InventoryItem> items = new List<InventoryItem>();
    private Image image;
    private Text text;

    void Start()
    {
        image = transform.FindChild("Image").GetComponent<Image>();
        text = transform.FindChild("Text").GetComponent<Text>();
    }

    public bool IsEmpty()
    {
        return items.Count <= 0;
    }

    public void AddItem(InventoryItem item)
    {
        this.items.Add(item);
        image.sprite = item.sprite;
        text.text = "" + items.Count;
    }

    public bool CanAddItem(InventoryItem item)
    {
        return IsEmpty() || (items[0].stackable && items[0].Equals(item));
    }

    public bool SetOrAdd(InventoryItem item)
    {
        if (CanAddItem(item))
        {
            AddItem(item);
            return true;
        }

        return false;
    }
}
