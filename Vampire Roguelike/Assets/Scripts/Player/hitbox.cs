

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
    PlayerController playerInfo;

    void Awake()
    {
        //Grabs the swords active sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Sets the sword to it's normal sprite
        spriteRenderer.sprite = normal;
        //Calls the attack facing method to make sure the sowrd is following the mouse
        attackFacing();
        //Checks to make sure the player can attack
        if (attackLag <= 0)
        {
            //Checks thier inputs
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Sets the attack lag preventing spam attacks
                attackLag = startLag;
                spriteRenderer.sprite = stab;
                //Checks to make sure the collider has an active trigger
                if (trigger == true)
                {
                    //grabs a copy of the playerController
                    playerInfo = PlayerController.instance;
                    //Grabs the script from the current enemy and calls the damage function which accepts a float for the damage amount
                    enemyToHit.GetComponent<Enemy>().takeDamage(playerInfo.attackDamage);
                    //Resets the triggers to defaults
                    enemyToHit = null;
                    trigger = false;
                }



            }
        }
        else
        {
            //decreases the attack lag
            attackLag -= Time.deltaTime;
        }
    }

    private void attackFacing()
    {
        //grabs the current mouse cords
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //grabs the differnce in the swords positions and the mouses
        Vector2 direction = new Vector2(
         mousePosition.x - transform.position.x,
         mousePosition.y - transform.position.y
         );
        //updates the swords transfrom from the right
        sword.transform.right = direction;

    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        //Resets the trigger
        trigger = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //The object triggering needs to have the tag enemy
        if (collision.gameObject.tag == "Enemy")
        {
            trigger = true;
            //Grab the enemy that caused the trigger
            enemyToHit = collision.gameObject;
        }
    }
}
