using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float soakRadius;
    
    
    public int gold = 0;

    public float interactRadius = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator anim;
    public GameObject sword;
    public GameObject bloodSoakFx;

    public Image healthBar;
    public Image bloodBar;
    public Text goldText;
    public Controls controls1 = new Controls();
    public Dictionary<string, KeyCode> playerControls = new Dictionary<string, KeyCode>();


    private State state;
    private Vector3 lastMoveDirection;
    private float slideSpeed;

    private enum State
    {
        Normal,
        Dashing,
        Dead
    }

    public static PlayerController instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
        state = State.Normal;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerControls = controls1.playerControls();
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;

        if(goldText != null)
        {
            goldText.text = "Gold: " + gold;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //checkInteract();
        switch (state)
        {
            case State.Normal:
                anim.SetBool("Rolling", false);
                //If pause menu not open, allow control of player
                if (UIManager.instance != null && !UIManager.instance.pauseMenuOpen) { 
                
                Move();
                ChangeDirection();
                HandleDash();
                bloodSoak();
                }
                
                break;
            case State.Dashing:
                Dash();
                break;
            case State.Dead:
                break;
        }

        if(health <= 0)
        {
            Die();
        }

        if(healthBar == null && SceneManager.GetActiveScene().name != "MainMenu")
        {
            healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        }

        if(bloodBar == null && SceneManager.GetActiveScene().name != "MainMenu")
        {
            bloodBar = GameObject.Find("BloodBar").GetComponent<Image>();
        }

        if(goldText == null && SceneManager.GetActiveScene().name != "MainMenu")
        {
            goldText = GameObject.Find("GoldCount").GetComponent<Text>();
        }
        
    }

    private void Die()
    {
        state = State.Dead;

        SaveSystem.SaveGame(this);
        UIManager.instance.DisplayDeathMenu();
        SaveSystem.SaveToGraveyard();
        Destroy(gameObject);
    }

    private void ChangeDirection()
    {
        //Change orientation based on mouse location
        Vector3 playerScreenPoint = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < playerScreenPoint.x)
        {
            sr.flipX = true;
            sword.transform.localPosition = new Vector3(-0.292f, -0.117f);
        }
        else
        {
            sr.flipX = false;
            sword.transform.localPosition = new Vector3(0.292f, -0.117f);
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
        if (Input.GetKey(playerControls["Up"]))
        {
            moveY = +1f;
        }
        if (Input.GetKey(playerControls["Down"]))
        {
            moveY = -1f;
        }
        if (Input.GetKey(playerControls["Left"]))
        {
            moveX = -1f;
        }
        if (Input.GetKey(playerControls["Right"]))
        {
            moveX = 1f;
        }

        //Move character
        anim.SetBool("Moving", true);
        Vector3 moveDirection = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            anim.SetBool("Moving",false);
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
                        anim.SetBool("Moving", false);
                    }
                }
            }
        }
        
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(playerControls["Dodge"]))
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
            anim.SetBool("Rolling", true);
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

    public void bloodSoak()
    {
        if (Input.GetKey(playerControls["Bloodsuck"])) {
            Instantiate(bloodSoakFx, transform.position, Quaternion.identity);
            Collider2D[] bloodPools = Physics2D.OverlapCircleAll(transform.position, soakRadius);
            for (int i = 0; i < bloodPools.Length; i++)
            {
                Debug.Log("Checked");
                if (bloodPools[i].CompareTag("Bloodpool"))
                {
                    Debug.Log("Bloods");
                    bloodPools[i].GetComponent<bloodPool>().sendBlood(this.gameObject);
                }
            }
        }
    }
}
