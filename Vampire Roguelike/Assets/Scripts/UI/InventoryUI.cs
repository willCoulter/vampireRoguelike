using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public Transform itemsParent;
    public GameObject itemPopupBox;
    public GameObject skillPopupBox;

    ItemInventory inventory;

    InventorySlot[] slots;

    void Awake()
    {
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
        if(itemPopupBox != null && Input.GetKeyDown("tab"))
        {
            Destroy(itemPopupBox);
        }

        if (skillPopupBox != null && Input.GetKeyDown("tab"))
        {
            Destroy(skillPopupBox);
        }
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
