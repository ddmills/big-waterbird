using UnityEngine;
using System.Collections;
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
}
