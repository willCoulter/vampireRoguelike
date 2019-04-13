using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/StunSkill")]
public class StunSkill : Skill
{
    public override void Initialize(GameObject obj)
    {
        if (obj != null)
        {

        }
    }

    public override void TriggerSkill()
    {
        PlayerController.instance.sword.GetComponent<hitbox>().stunMode = true;
    }


}
