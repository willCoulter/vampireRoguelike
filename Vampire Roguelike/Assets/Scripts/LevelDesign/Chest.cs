using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool hasOpened;
    bool playerInPickupRange;
    public List<Item> allItems;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            UIManager.instance.displayChestPopup(transform.position);
            playerInPickupRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            UIManager.instance.hideChestPopup();
            playerInPickupRange = false;
        }
    }

    //public Item openChest(){

    //}
}
