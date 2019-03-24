
using UnityEngine;

public class hitbox : MonoBehaviour
{
    private float attackLag;
    public float startLag;
    public GameObject sword;
    public LayerMask enemiesMask;
    private GameObject enemyToHit;
    public Sprite normal;
    public Sprite stab;
    private SpriteRenderer spriteRenderer;
    public bool trigger = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sprite = normal;
        attackFacing();
        if (attackLag <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                attackLag = startLag;
                spriteRenderer.sprite = stab;
                if (trigger == true) {
                    playerInfo = PlayerController.instance;
                    //Destroy(collision.gameObject);
                    enemyToHit.GetComponent<Enemy>().takeDamage(playerInfo.attackDamage);
                    

                }
                


            }
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }

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


    PlayerController playerInfo;
    public bool canAttack = true;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        Debug.Log("Help " + canAttack);
        if (collision.gameObject.tag == "Enemy")
        {
            trigger = true;
            enemyToHit = collision.gameObject;
        }
    }
}
