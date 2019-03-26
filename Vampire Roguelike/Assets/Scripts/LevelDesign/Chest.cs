using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool hasOpened;
    bool playerInPickupRange;

    public GameObject pickupPrefab;
    public List<Item> allItems;

    private void Update()
    {
        if (!hasOpened && playerInPickupRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.hideChestPopup();
            OpenChest();
            hasOpened = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !hasOpened)
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

    public void OpenChest(){
        GameObject itemDrop = Instantiate(pickupPrefab, transform.position, transform.rotation);

        ItemPickup itemScript = itemDrop.GetComponent<ItemPickup>();

        //Generate random number and set itempickup item
        int rand = Random.Range(0, allItems.Count);
        itemScript.item = allItems[rand];

    }
}
