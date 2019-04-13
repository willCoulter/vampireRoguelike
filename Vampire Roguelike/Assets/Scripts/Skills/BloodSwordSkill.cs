using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BloodSwordSkill")]
public class BloodSwordSkill : Skill
{
    public float multipler;
    public float dur;

    public override void Initialize(GameObject obj)
    {

        

        if (obj != null)
        {

        }
    }

    public override void TriggerSkill()
    {
        Bloodbladebuff.Instance.BuffDamage(multipler,dur);
    }
}
