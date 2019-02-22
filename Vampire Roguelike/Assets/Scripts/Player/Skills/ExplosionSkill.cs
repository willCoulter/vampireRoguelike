using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ExplosionSkill")]
public class ExplosionSkill : Skill
{
    public int damage = 10;
    public float radius = 1f;
    private Transform spawnLocation;

    public override void Initialize(GameObject obj)
    {
        if (obj != null){
            spawnLocation = obj.transform;
        }
    }

    public override void TriggerSkill()
    {
        Debug.Log(skillName + " triggered");
    }
}
