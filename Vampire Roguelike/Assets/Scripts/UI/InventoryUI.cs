﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public Transform itemsParent;
    ItemInventory inventory;

    InventorySlot[] slots;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory UI instance");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = ItemInventory.instance;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory() {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}