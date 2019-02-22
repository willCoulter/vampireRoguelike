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
                Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(rangeX,rangeY), attackPos.transform.localRotation.z, enemiesMask);
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
        Gizmos.DrawWireCube(transform.position, new Vector3(rangeX,rangeY, 1));

    }
    private void attackFacing()
    {
        
        //Change orientation based on mouse location
        Vector3 playerScreenPoint = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouse = Input.mousePosition;
        Vector3 convert = Camera.main.ScreenToViewportPoint(mouse);

        Vector2 vector2 = mousePos - transform.position;
        angleBetween = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        Quaternion rotatation = Quaternion.AngleAxis(angleBetween, Vector3.forward);
        //Debug.Log(rotatation);
        attackPos.transform.localRotation = rotatation;
        

//        Vector2 direction = new Vector2(
//            mousePos.x - transform.position.x,
//            mousePos.y - transform.position.y);


        /**
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
    **/

    }
}
