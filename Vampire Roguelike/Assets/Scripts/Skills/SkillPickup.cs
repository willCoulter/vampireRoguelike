using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickup : MonoBehaviour
{

    public Skill skill;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Debug.Log("Picked up " + skill.name);
        bool wasPickedUp = SkillInventory.instance.Add(skill);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }

    }
}
