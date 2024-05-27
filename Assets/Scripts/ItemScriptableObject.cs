using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="New Shop Item", menuName="Shop Item")]
public class ItemScriptableObject : ScriptableObject
{
    public new string name; 
    public Sprite itemSprite;

    public float defense;
    public float attack;
    public float magic;

    public float cost;
    public float basePrice;

    public Category category;
    public enum Category {Shield, Weapon, Potion, Armor, Magic};

    public float perceivedValue;
    public float purchasePriority;

    public int itemSize;
}
