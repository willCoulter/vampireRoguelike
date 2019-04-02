using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodblast : MonoBehaviour
{
    public float damage = 10;
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
        Debug.Log(name + " triggered");
        Debug.Log(playerPos.position);
        GameObject blast = Instantiate(blastFx, playerPos.position, Quaternion.identity);
        //Vector3 positionOfCircle;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(playerPos.position, radius, enemiesMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("Enemy hit");
            enemies[i].GetComponent<Enemy>().takeDamage(damage);
        }
        //Destroy(blast);
    }
}
