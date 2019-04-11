using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vampireBoss : Enemy
{
    private float attackLag;
    public float startLag;
    private bool attacking = false;
    public GameObject swordPrefab;
    public Animator character;
    public Animator swordAnim;
    public float bloodBlastDamage;

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
                    StartCoroutine(AttackCo());
                } else if (distance <= agroRadius)
                {
                    TriggerBolt();
                    attackLag = startLag;
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
        
        attacking = true;

        switch (RandomNumber(0, 4))
        {
            case 0:
                swordPrefab.SetActive(true);
                swordAnim.SetBool("swing", true);
                yield return new WaitForSeconds(0.5f);
                swordAnim.SetBool("swing", false);
                break;

            case 1:
                swordPrefab.SetActive(true);
                swordAnim.SetBool("stab", true);
                yield return new WaitForSeconds(0.5f);
                swordAnim.SetBool("stab", false);
                break;

            case 2:
                TriggerBlast();
                yield return new WaitForSeconds(0.5f);  
                break;

            case 3:
                TriggerBolt();
                yield return new WaitForSeconds(0.5f);
                break;

        }
        attacking = false;
        swordPrefab.SetActive(false);
        attackLag = startLag;

    }

    public int RandomNumber(int min, int max)
    {
        int randomInt = Random.Range(min, max);
        return randomInt;
    }

    public LayerMask enemiesMask;
    public float radius = 1f;
    public GameObject blastFx;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

    public void TriggerBlast()
    {
        GameObject blast = Instantiate(blastFx, transform.position, Quaternion.identity);
        //Vector3 positionOfCircle;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemiesMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("Enemy hit");
            enemies[i].GetComponent<PlayerController>().takeDamage(bloodBlastDamage);
        }
        //Destroy(blast);
    }

    public GameObject bloodBoltPrefab;


    public void TriggerBolt()
    {
        GameObject bullet = Instantiate(bloodBoltPrefab, swordPrefab.transform.position, swordPrefab.transform.rotation);
        bullet.GetComponent<arrow>().damage = damage;
        Debug.Log("Instantiated");

    }
}
