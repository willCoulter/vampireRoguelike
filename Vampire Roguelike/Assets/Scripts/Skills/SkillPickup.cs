using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickup : MonoBehaviour
{

    public Skill skill;
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
            UIManager.instance.displaySkillPopup(skill, transform.position);
            playerInPickupRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            UIManager.instance.hideSkillPopup();
            playerInPickupRange = false;
        }
    }

    void PickUp()
    {
        Debug.Log("Picked up " + skill.name);
        bool wasPickedUp = SkillInventory.instance.Add(skill);

        if (wasPickedUp)
        {
            UIManager.instance.hideSkillPopup();
            playerInPickupRange = false;

            Destroy(gameObject);
        }
    }
}
