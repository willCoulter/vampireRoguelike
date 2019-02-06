using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float dashDistance = 15f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

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
        Move();
        ChangeDirection();
    }

    private bool CanMove(Vector3 direction, float distance)
    {
        return Physics2D.Raycast(transform.position, direction, distance).collider == null;
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
            sr.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            sr.flipX = false;
        }

        //Move character
        Vector3 moveDirection = new Vector3(moveX, moveY).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
