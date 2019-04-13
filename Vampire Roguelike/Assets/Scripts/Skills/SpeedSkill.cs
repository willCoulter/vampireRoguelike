using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SpeedBuffSkill")]
public class SpeedSkill : Skill
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
        speedBuffScript.Instance.BuffSpeed(multipler, dur);
    }
}
