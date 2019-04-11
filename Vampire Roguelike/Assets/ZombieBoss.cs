using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : Enemy
{
    private float attackLag;
    public float startLag;
    private bool attacking = false;
    private bool rageMode = false;
    public GameObject rightFist;
    public GameObject leftFist;
    public Animator rightFistAnim;
    public Animator leftFistAnim;

    private void FixedUpdate()
    {
        AttackFacing();
        NormalAttack();
        Rage();
    }


    private void AttackFacing()
    {
        if (attacking == false)
        {
            //grabs the differnce in the swords positions and the mouses
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the swords transfrom from the right
            rightFist.transform.up = direction;
            leftFist.transform.up = direction;
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
                    StartCoroutine(AttackCo());
                }
            }
            else
            {
                attackLag -= Time.deltaTime;
            }
        }
    }

    private void Rage()
    {
        if (health <= (maxHealth/2) && rageMode != true)
        {
            anim.SetBool("Raging",true);
            rageMode = true;
            damage = damage * 2;
            speed = speed * 2;
        }
    }

    public IEnumerator AttackCo()
    {
        rightFist.transform.GetChild(0).GetComponent<SwordHit>().damage = damage;
        leftFist.transform.GetChild(0).GetComponent<SwordHit>().damage = damage;
        rightFist.SetActive(true);
        leftFist.SetActive(true);
        attacking = true;
        rightFistAnim.SetBool("swing", true);
        leftFistAnim.SetBool("swing", true);
        yield return new WaitForSeconds(0.5f);
        rightFistAnim.SetBool("swing", false);
        leftFistAnim.SetBool("swing", false);
        attacking = false;
        rightFist.SetActive(false);
        leftFist.SetActive(false);
        attackLag = startLag;

    }
}
