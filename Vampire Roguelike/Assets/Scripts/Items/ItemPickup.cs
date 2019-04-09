using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool playerInPickupRange;
    private Controls controls1 = new Controls();
    private Dictionary<string, KeyCode> playerControls = new Dictionary<string, KeyCode>();

    private void Start()
    {
        playerControls = controls1.playerControls();
    }

    private void Update()
    {
        //If in range, and player presses e, pick up skill
        if (playerInPickupRange && Input.GetKeyDown(playerControls["Interact"]))
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
