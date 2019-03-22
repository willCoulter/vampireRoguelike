using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private float attackLag;
    public float startLag;

    public float rangeX;
    public float rangeY;
    public int damage;
    public int finalHitDamage;
    public GameObject attackPos;
    public GameObject sword;
    public LayerMask enemiesMask;
    private float angleBetween = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackFacing();
        if(attackLag <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Collider2D[] enemies = Physics2D.OverlapBoxAll(sword.transform.right, new Vector2(rangeX,rangeY), sword.transform.localRotation.z, enemiesMask);
               // for (int i = 0; i < enemies.Length; i++)
               // {
                   //' enemies[i].GetComponent<Enemy>().takeDamage(damage);
                //}
          

               

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
        Gizmos.DrawWireCube(sword.transform.localPosition, new Vector3(rangeX,rangeY, 1));

    }

    //Rotates the sword towards the mouse
    private void attackFacing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

       Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

       sword.transform.right = direction;

    }
}
