using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    public int damage = 10;
    public float speed = 1f;

    public override void Initialize(GameObject obj)
    {

    }

    public override void TriggerSkill()
    {
        Debug.Log(skillName + " triggered");
    }
}
