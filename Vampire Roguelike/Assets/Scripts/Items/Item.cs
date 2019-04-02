using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string desc = "New Desc";
    public Sprite itemSprite;

    public abstract void Initialize();

}
