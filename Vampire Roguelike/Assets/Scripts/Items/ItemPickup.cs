using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool playerInPickupRange;
    private Controls controls1 = new Controls();
    public KeyCode interactKey;

    private void Start()
    {
       interactKey = PlayerController.instance.playerControls["Interact"];
    }

    private void Update()
    {
        //If in range, and player presses e, pick up skill
        if (playerInPickupRange && Input.GetKeyDown(interactKey))
        {
            PickUp();
        }

        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.itemSprite;
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
            GameManager.instance.itemsGathered++;
            Destroy(gameObject);
        }
    }
}
