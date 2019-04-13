using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePickup : SkillPickup
{
    public int price;

    public override void PickUp()
    {
        if(PlayerController.instance.gold >= price)
        {
            bool wasPickedUp = SkillInventory.instance.Add(skill);

            if (wasPickedUp)
            {
                PlayerController.instance.gold -= skill.skillPrice;

                UIManager.instance.hideSkillPopup();
                playerInPickupRange = false;

                Destroy(gameObject);
            }
        }
    }
}
