using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{

    public static ItemInventory instance;
    public List<Item> allItems;
    public int slots = 15;

    void Awake()
    {
        instance = this;
    }

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (items.Count >= slots)
        {
            Debug.Log("Not enough slots");
            return false;
        }

        items.Add(item);
        item.Initialize();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ClearAllItems()
    {
        items = new List<Item>();
    }
}
