using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickup : MonoBehaviour
{
    public Skill skill;
    public bool playerInPickupRange;
    public KeyCode interactKey;

    private void Start()
    {
        interactKey = PlayerController.instance.playerControls["Interact"];
    }

    private void Update()
    {
        if (playerInPickupRange && Input.GetKeyDown(interactKey))
        {
            PickUp();
        }

        if(skill != null)
        {
            GetComponent<SpriteRenderer>().sprite = skill.skillSprite;
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

    virtual public void PickUp()
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
