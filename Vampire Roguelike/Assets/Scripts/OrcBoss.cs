using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBoss : Enemy
{
    public Transform weapon;
    private float attackLag;
    public float startLag;
    private bool attacking = false;
    public float slamDamage;
    public float slamCooldown;
    private float cooldownCounter;
    public float slamRadius;
    public GameObject slam;
    public GameObject swordPrefab;
    public Animator swordAnim;

    private void FixedUpdate()
    {
        AttackFacing();
        NormalAttack();
        
        SlamAttack();
    }


    private void AttackFacing()
    {
        if (attacking == false) {
            //grabs the differnce in the swords positions and the mouses
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the swords transfrom from the right
            swordPrefab.transform.right = direction;
        }
    }

    private void NormalAttack()
    {
        if (attacking == false)
        {
            if (attackLag <= 0)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= miniumRange)
                {
                    //GameObject sword = Instantiate(swordPrefab, weapon.position, weapon.rotation,weapon.transform);
                    StartCoroutine(AttackCo());

                }
            }
            else
            {
                attackLag -= Time.deltaTime;
            }
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

    public IEnumerator AttackCo()
    {
        swordPrefab.SetActive(true);
        attacking = true;
        swordAnim.SetBool("swing", true);
        yield return new WaitForSeconds(0.5f);
        swordAnim.SetBool("swing", false);
        attacking = false;
        swordPrefab.SetActive(false);
        attackLag = startLag;

    }

}



