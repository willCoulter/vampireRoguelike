
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    private float attackLag;
    public float startLag;
    public GameObject sword;
    public LayerMask enemiesMask;
    private List<GameObject> enemyToHit = new List<GameObject>();
    private GameObject fireBallHit;
    public Sprite normal;
    public Sprite stab;
    private SpriteRenderer spriteRenderer;
    public bool trigger = false;
    PlayerController playerInfo;
    private Controls controls1 = new Controls();
    private Dictionary<string, KeyCode> playerControls = new Dictionary<string, KeyCode>();

    void Awake()
    {
        //Grabs the swords active sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerInfo = PlayerController.instance;
        playerControls = controls1.playerControls();
    }

    void Update()
    {
        //Sets the sword to it's normal sprite
        spriteRenderer.sprite = normal;
        //Calls the attack facing method to make sure the sowrd is following the mouse, if pause menu not open
        if (!UIManager.instance.pauseMenuOpen)
        {
            attackFacing();
        }
            
        //Checks to make sure the player can attack
        if (attackLag <= 0)
        {
            //Checks thier inputs
            if (Input.GetKey(KeyCode.Mouse0))
            {
                playerInfo = PlayerController.instance;

                //Sets the attack lag preventing spam attacks
                attackLag = startLag;
                playerInfo.anim.SetTrigger("Attack");
                //spriteRenderer.sprite = stab;
                //Checks to make sure the collider has an active trigger
                if (trigger == true)
                {
                    //grabs a copy of the playerController
                    
                    for (int i = 0; i < enemyToHit.Count; i++) { 
                        //Grabs the script from the current enemy and calls the damage function which accepts a float for the damage amount
                        enemyToHit[i].GetComponent<Enemy>().takeDamage(playerInfo.attackDamage);
                        
                    }

                    if (fireBallHit != null)
                    {
                        fireBallHit.GetComponent<FollowingFireBall>().ChangeTarget();
                    }
                    //Resets the triggers to defaults
                    enemyToHit.Clear();
                    fireBallHit = null;
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
        enemyToHit.Clear();
        fireBallHit = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //The object triggering needs to have the tag enemy
        if (collision.gameObject.tag == "Enemy")
        {
            trigger = true;
            if (enemyToHit.Contains(collision.gameObject) == false) {
                //Grab the enemy that caused the trigger
                enemyToHit.Add(collision.gameObject);
            }
        } else if (collision.gameObject.tag == "WizardFireball")
        {
            trigger = true;
            fireBallHit = collision.gameObject;

        }

    }

    private void ReturnToSender()
    {

    }
}
