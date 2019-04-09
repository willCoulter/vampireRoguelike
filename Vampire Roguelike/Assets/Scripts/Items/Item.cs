using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public int itemID;
    public string itemName = "New Item";
    public string desc = "New Desc";
    public Sprite itemSprite;

    public abstract void Initialize();

}
