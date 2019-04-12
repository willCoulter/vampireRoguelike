using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarrior : Enemy
{
    private float attackLag;
    public float startLag;
    private bool attacking = false;
    public GameObject spearPrefab;
    public Animator spearAnim;

    private void FixedUpdate()
    {
        AttackFacing();
        NormalAttack();
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
            //updates the objects transfrom from the right
            spearPrefab.transform.right = direction;
        }
    }
    //Checks if the enemy can make an attack based on a delay
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

    public IEnumerator AttackCo()
    {
        //Tells the script an attack is going on
        attacking = true;
        //Actiave the object and set it's damage
        spearPrefab.SetActive(true);
        spearPrefab.GetComponent<SwordHit>().damage = damage;
        //Start an animation and then wait to disable it
        spearAnim.SetBool("swing", true);
        yield return new WaitForSeconds(0.5f);
        spearAnim.SetBool("swing", false);
        //Reset to defaults
        attacking = false;
        spearPrefab.SetActive(false);
        attackLag = startLag;

    }
}
