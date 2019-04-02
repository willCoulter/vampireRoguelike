using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BloodSwordSkill")]
public class BloodSwordSkill : Skill
{

    private Transform spawnLocation;
    private Bloodblast bloodblast;
    private float swordDamage;
    private float swordDamageModifed;
    public float multipler;
    public float dur;

    public override void Initialize(GameObject obj)
    {

        

        if (obj != null)
        {
            spawnLocation = obj.transform;
        }
    }

    public override void TriggerSkill()
    {
        PlayerController.instance.sword.GetComponent<SpriteRenderer>().color = Color.red;
        swordDamage = PlayerController.instance.attackDamage;
        swordDamageModifed = swordDamage * multipler;
        PlayerController.instance.attackDamage = swordDamageModifed;
    }


}
