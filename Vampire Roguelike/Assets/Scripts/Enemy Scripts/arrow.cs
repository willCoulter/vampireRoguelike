﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject arrowObject;

        
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("");
            collision.gameObject.GetComponent<PlayerController>().takeDamage(2);
            Destroy(arrowObject);


        }
        
    }

}
