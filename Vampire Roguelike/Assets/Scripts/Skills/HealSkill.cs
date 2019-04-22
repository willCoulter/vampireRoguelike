using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HealSkill")]
public class HealSkill : Skill
{
    private Transform spawnLocation;
    public float healAmount;

    public override void Initialize(GameObject obj)
    {
        if (obj != null)
        {
            spawnLocation = obj.transform;
        }
    }

    public override void TriggerSkill()
    {
        Debug.Log("Worked");
        PlayerHealSkill.HealPlayer(healAmount);
    }


}