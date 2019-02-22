using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Skill
{
    public int damage = 10;
    public float speed = 1f;

    public override void Initialize(GameObject obj)
    {

    }

    public override void TriggerAbility()
    {
        Debug.Log(skillName + " triggered");
    }
}
