using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    private float attackLag;
    public float startLag;
    public float buffSpellRadius;
    public GameObject spellFx;

    private void FixedUpdate()
    {
        heal();
    }

    public void heal()
    {
        if (attackLag <= 0)
        {
            Debug.Log("fired");
            attackLag = startLag;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, buffSpellRadius);
            GameObject buff = Instantiate(spellFx, transform.position, transform.rotation);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Enemy"))
                {
                    //Run the buff here
                    enemies[i].GetComponent<Enemy>().takeDamage(0);
                }
            }
            //Destroy(buff);
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }
}
