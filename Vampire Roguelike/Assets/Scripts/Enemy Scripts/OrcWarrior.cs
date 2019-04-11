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
            //grabs the differnce in the swords positions and the mouses
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the swords transfrom from the right
            spearPrefab.transform.right = direction;
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

    public IEnumerator AttackCo()
    {
        spearPrefab.SetActive(true);
        attacking = true;
        spearAnim.SetBool("swing", true);
        yield return new WaitForSeconds(0.5f);
        spearAnim.SetBool("swing", false);
        attacking = false;
        spearPrefab.SetActive(false);
        attackLag = startLag;

    }
}
