using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private State state;
    private Vector3 lastMoveDirection;
    private float slideSpeed;

    private enum State
    {
        Normal,
        Dashing,
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    private bool CanMove(Vector3 moveDirection)
    {
        //Create layermask on player layer
        int layerMask = 1 << 8;

        //Invert layermask to all layers other than player
        layerMask = ~layerMask;

        //Raycast in movement direction to check for walls
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDirection, speed * Time.deltaTime, layerMask);

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

        if(CanMove(moveDirection)){
            transform.position += moveDirection * speed * Time.deltaTime;
            lastMoveDirection = moveDirection;
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
        transform.position += lastMoveDirection * slideSpeed * Time.deltaTime;

        //Reduce speed over time
        slideSpeed -= slideSpeed * 5f * Time.deltaTime;

        //If slow enough, change state back to normal
        if(slideSpeed < 3f)
        {
            state = State.Normal;
        }
    }
}
