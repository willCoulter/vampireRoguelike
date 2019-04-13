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
        //Runs these check methods on a fixed update
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
        //check to see if an attack is in progress
        if (attacking == false)
        {
            //then updates the hit attack delay and checks if he's able to attack again
            if (attackLag <= 0)
            {
                //Grab an ubdate distance based on the both targets positions
                float distance = Vector3.Distance(target.position, transform.position);
                //if the player is in close range make melee attacks
                if (distance <= miniumRange)
                {
                    StartCoroutine(AttackCo());
                //if the player is too far away shot fireballs
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
    //Randomly picks an ability to use then delays the statements by 0.5 seconds for the animation.
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
    //Method that makes a random int based on a min and max value
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

    //Makes a blast based on the players blast ability by first making the effect then checking a radius around the enemy.
    public void TriggerBlast()
    {
        GameObject blast = Instantiate(blastFx, transform.position, Quaternion.identity);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemiesMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<PlayerController>().takeDamage(bloodBlastDamage);
        }
    }

    public GameObject bloodBoltPrefab;

    //Spawns a projectile. 
    public void TriggerBolt()
    {
        GameObject bullet = Instantiate(bloodBoltPrefab, swordPrefab.transform.position, swordPrefab.transform.rotation);
        bullet.GetComponent<arrow>().damage = damage;
        Debug.Log("Instantiated");

    }
}
