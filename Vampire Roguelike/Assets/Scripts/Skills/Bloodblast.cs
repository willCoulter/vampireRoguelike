using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodblast : MonoBehaviour
{

    public LayerMask enemiesMask;
    public float radius = 1f;
    public Transform playerPos;
    public GameObject blastFx;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos.position, radius);

    }

    public void TriggerBlast()
    {
        //makes the game effect
        GameObject blast = Instantiate(blastFx, playerPos.position, Quaternion.identity);
        //Checks for enemies in the radius
        Collider2D[] enemies = Physics2D.OverlapCircleAll(playerPos.position, radius, enemiesMask);
        //loopd through all collosions loking for enemies and dealing damage
        for (int i = 0; i < enemies.Length; i++)
        {
            //Debug.Log("Enemy hit");
            enemies[i].GetComponent<Enemy>().takeDamage(PlayerController.instance.magicDamage);
        }
    }
}
