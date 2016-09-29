using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "loot/NewLoot", menuName = "Loot Item", order = 1)]
public class Loot : ScriptableObject {
    public string title;
    public float weight = 1f;
    public bool stackable = true;
    public Sprite sprite;
    public GameObject gameObject;
}
