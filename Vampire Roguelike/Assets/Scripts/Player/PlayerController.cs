using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;

    public float speed = 7f;
    public float maxHealth = 100;
    public float health;
    public float maxBlood;
    public float blood;
    public float attackDamage;
    public float magicDamage;
    
    
    public int gold = 0;

    public float interactRadius = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Image healthBar;
    public Image bloodBar;
    public Text goldText;

    private State state;
    private Vector3 lastMoveDirection;
    private float slideSpeed;

    private enum State
    {
        Normal,
        Dashing,
    }

    public static PlayerController instance;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        goldText.text = "Gold: " + gold;

    }

    // Update is called once per frame
    void Update()
    {
        //checkInteract();
        switch (state)
        {
            case State.Normal:
                Move();
                ChangeDirection();
                HandleDash();
                break;
            case State.Dashing:
                Dash();
                break;
        }
        
    }

    private void ChangeDirection()
    {
        //Change orientation based on mouse location
        Vector3 playerScreenPoint = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < playerScreenPoint.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private bool CanMove(Vector3 moveDirection, float distance)
    {
        //Create layermask on player layer
        //int layerMask = 1 << 8;

        //Invert layermask to all layers other than s
        //layerMask = ~layerMask;

        //Raycast in movement direction to check for walls
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDirection, distance * Time.deltaTime, layerMask);

        return raycastHit.collider == null;
    }

    private void Move()
    {
        //Reset movement
        float moveX = 0f;
        float moveY = 0f;
        
        //Handle based on key
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        //Move character
        Vector3 moveDirection = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            //play idle animation
        }
        else
        {
            if (CanMove(moveDirection, speed))
            {
                //Can move, did not collide
                transform.position += moveDirection * speed * Time.deltaTime;
                lastMoveDirection = moveDirection;
            }
            else
            {
                //Cannot move, test horizontal
                Vector3 testMoveDirection = new Vector3(moveDirection.x, 0f).normalized;
                Vector3 targetMovePosition = transform.position + testMoveDirection * speed * Time.deltaTime;

                if (CanMove(testMoveDirection, speed))
                {
                    //Can move horizontally
                    lastMoveDirection = testMoveDirection;
                    transform.position = targetMovePosition;
                }
                else
                {
                    //Cannot move horizontally, test vertical
                    testMoveDirection = new Vector3(0f, moveDirection.y).normalized;
                    targetMovePosition = transform.position + testMoveDirection * speed * Time.deltaTime;

                    if (CanMove(testMoveDirection, speed))
                    {
                        //Can move vertically
                        lastMoveDirection = testMoveDirection;
                        transform.position = targetMovePosition;
                    }
                    else
                    {
                        //Cannot move, play idle animation
                    }
                }
            }
        }
        
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Dashing;
            slideSpeed = 17f;
        }
    }

    private void Dash()
    {
        //Slide player by slideSpeed amount
        if (CanMove(lastMoveDirection, slideSpeed))
        {
            transform.position += lastMoveDirection * slideSpeed * Time.deltaTime;

            sr.color = new Color(1f, 1f, 1f, .5f);

            //Reduce speed over time
            slideSpeed -= slideSpeed * 5f * Time.deltaTime;
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            state = State.Normal;
        }
        

        //If slow enough, change state back to normal
        if (slideSpeed < 3f)
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            state = State.Normal;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
    }

    public void gainBlood(float bloodGained)
    {
        blood += bloodGained;
        bloodBar.fillAmount = blood / 100f;
    }

    public void gainGold(int goldGained)
    {
        gold += goldGained;
        goldText.text = "Gold: " + gold;
    }
    //public void checkInteract()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        Collider2D[] things = Physics2D.OverlapCircleAll(transform.position, interactRadius);
    //        if (things != null)
    //        {
    //            things[0].GetComponent<skillShop>().openShop();
    //        }      
    //    }
    //}
}
