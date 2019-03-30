using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Monster : Enemy
{
    public GameObject explosionFx;
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= miniumRange)
        {
            blowUp();
        }
    }

    private void blowUp()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, miniumRange);
        GameObject buff = Instantiate(explosionFx, transform.position, transform.rotation);
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].CompareTag("Player"))
            {
                
                player[i].GetComponent<PlayerController>().takeDamage(90);
                
            }
        }
        Die();
    }
}
