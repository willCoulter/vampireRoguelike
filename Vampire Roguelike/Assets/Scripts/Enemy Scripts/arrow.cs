﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject arrowObject;
    public float damage;

        
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool dealtDamage = collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            if (dealtDamage)
            {
                Destroy(arrowObject);
            }
            
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Bloodpool"))
        {

        }
        else
        {
            Destroy(arrowObject);
        }

    }

}
