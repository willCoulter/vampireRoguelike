using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBoss : Enemy
{
    public Transform weapon;
    private float attackLag;
    public float startLag;
    private bool attacking;
    public float slamDamage;
    public float slamCooldown;
    private float cooldownCounter;
    public float slamRadius;
    public GameObject slam;

    private void FixedUpdate()
    {
        NormalAttack();
        AttackFacing();
        SlamAttack();
    }


    private void AttackFacing()
    {
        if (attacking != true)
        {
            //grabs the differnce in the swords positions and the mouses
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the swords transfrom from the right
            weapon.transform.up = direction;
        }

    }

    private void NormalAttack()
    {
        if (attackLag <= 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            
            if (distance <= miniumRange)
            {

                attacking = true;
                //weapon.transform.Rotate(new Vector3(0, 0, -180));
                anim.SetTrigger("Attack");
                attackLag = startLag;
            }
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }

    private void SlamAttack()
    {
        if (attackLag <= 0 && cooldownCounter <= 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= miniumRange)
            {
                Instantiate(slam, transform.position, Quaternion.identity);
                Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, slamRadius);
                for (int i = 0; i < player.Length; i++)
                {
                    if (player[i].CompareTag("Player"))
                    {
                        //Run the buff here
                        player[i].GetComponent<PlayerController>().takeDamage(slamDamage);
                    }
                }
                attackLag = startLag;
                cooldownCounter = slamCooldown;
            }
        }
        else
        {
            //attackLag -= Time.deltaTime;
            cooldownCounter -= Time.deltaTime;
        }
    }
}



