using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool playerInPickupRange;

    private void Update()
    {
        //If in range, and player presses e, pick up skill
        if (playerInPickupRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            UIManager.instance.displayItemPopup(item, transform.position);
            playerInPickupRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            UIManager.instance.hideItemPopup();
            playerInPickupRange = false;
        }
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);
        bool wasPickedUp = ItemInventory.instance.Add(item);

        if (wasPickedUp)
        {
            UIManager.instance.hideItemPopup();
            playerInPickupRange = false;

            Destroy(gameObject);
        }
    }
}
