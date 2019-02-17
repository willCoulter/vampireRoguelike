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
    public GameObject attackPos;
    public LayerMask enemiesMask;
    

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
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.transform.position, range, enemiesMask);
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
        Gizmos.DrawWireSphere(attackPos.transform.position, range);

    }
    private void attackFacing()
    {
        //Change orientation based on mouse location
        Vector3 playerScreenPoint = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouse = Input.mousePosition;
        Vector3 convert = Camera.main.ScreenToViewportPoint(mouse);
        Debug.Log(convert);
        //Face Down
        if (mousePos.y < playerScreenPoint.y && convert.y > convert.x)
        {
            attackPos.transform.localPosition = new Vector3(0, -0.63f, 0);
        }
        //Face Up
        else if (mousePos.y > playerScreenPoint.y && convert.y > convert.x)
        {
            attackPos.transform.localPosition = new Vector3(0, 0.63f, 0);
        }
        //Face Left
        else if (mousePos.x < playerScreenPoint.x)
        {
            attackPos.transform.localPosition = new Vector3(-0.63f, 0, 0);
           // Debug.Log("Faced Left");
        }
        //Face Right
        else if  (mousePos.x > playerScreenPoint.x)
        {
            attackPos.transform.localPosition = new Vector3(0.63f, 0, 0);
            //Debug.Log("Faced Right");
        }
        
    }
}
