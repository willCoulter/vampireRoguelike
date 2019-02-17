using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private float attackLag;
    public float startLag;

    public float range;
    public int damage;
    public int finalHitDamage;
    public Transform attackPos;
    public LayerMask enemiesMask;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(attackLag <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, range, enemiesMask);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().takeDamage(damage);
                }
                attackLag = startLag;
            }
            
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, range);

    }
}
