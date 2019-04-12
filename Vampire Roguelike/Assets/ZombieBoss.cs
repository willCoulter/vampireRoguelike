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
            //grabs the differnce in the objects positions and the target
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the objectss transfrom from the right
            rightFist.transform.up = direction;
            leftFist.transform.up = direction;
        }
    }
    //Checks if the monster can make an attack based on a timing delay
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

    //if the monster is below or at half health then double it's stats
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

    //This allows for managment of the melee attacks and thier animations
    public IEnumerator AttackCo()
    {
        attacking = true;
        //First update the fist objects damage based on the zombie 
        rightFist.transform.GetChild(0).GetComponent<SwordHit>().damage = damage;
        leftFist.transform.GetChild(0).GetComponent<SwordHit>().damage = damage;
        //Activate both gameobjects
        rightFist.SetActive(true);
        leftFist.SetActive(true);
        //Trigger the animations then wait for them to finish
        rightFistAnim.SetBool("swing", true);
        leftFistAnim.SetBool("swing", true);
        yield return new WaitForSeconds(0.5f);
        rightFistAnim.SetBool("swing", false);
        leftFistAnim.SetBool("swing", false);
        //attacking is now done, reset values
        attacking = false;
        rightFist.SetActive(false);
        leftFist.SetActive(false);
        attackLag = startLag;

    }
}
