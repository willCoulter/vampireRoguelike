using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ExplosionSkill")]
public class ExplosionSkill : Skill
{
    public int damage = 10;
    public LayerMask enemiesMask;
    public float radius = 1f;
    private Transform spawnLocation;
    private Bloodblast bloodblast;

    public override void Initialize(GameObject obj)
    {

        bloodblast = PlayerController.instance.GetComponent<Bloodblast>();
        //bloodblast = obj.GetComponent<Bloodblast>();

        if (obj != null)
        {
            spawnLocation = obj.transform;
        }
    }

    public override void TriggerSkill()
    {
        bloodblast.TriggerBlast();
    }


}
