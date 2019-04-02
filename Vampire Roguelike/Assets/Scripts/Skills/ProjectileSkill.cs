using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    public float damage = 10;
    public float speed = 1f;
    private Bloodbolt bloodBolt;
    private Transform spawnLocation;


    public override void Initialize(GameObject obj)
    {

        bloodBolt = PlayerController.instance.GetComponent<Bloodbolt>();
        //bloodblast = obj.GetComponent<Bloodblast>();

        if (obj != null)
        {
            spawnLocation = obj.transform;
        }
    }

    public override void TriggerSkill()
    {
        bloodBolt.TriggerBolt();
    }
}
