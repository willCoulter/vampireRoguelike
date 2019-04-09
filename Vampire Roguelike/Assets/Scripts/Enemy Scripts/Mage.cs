using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    private float attackLag;
    public float startLag;
    public float buffSpellRadius;
    public GameObject spellFx;
    public float circleTime;
    private float circleDur;
    private GameObject buffCircle;

    private void FixedUpdate()
    {
        heal();
    }

    public void heal()
    {
        if (attackLag <= 0)
        {
            if (buffCircle != null)
            {
                Destroy(buffCircle);
            }
            buffCircle = Instantiate(spellFx, transform.position, Quaternion.identity);
            circleDur += Time.deltaTime;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, buffSpellRadius);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Enemy"))
                {
                    //Run the buff here
                    enemies[i].GetComponent<Enemy>().heal(2);
                }
            }        
            attackLag = startLag;
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }
}
