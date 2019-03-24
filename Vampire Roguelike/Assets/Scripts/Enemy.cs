﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float speed;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            player.GetComponent<PlayerController>().gainGold(2);
            Destroy(gameObject);
            
        }
    }

    public void takeDamage(float damage)
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Damage Taken");
        sprite.color = Color.red;
        player.GetComponent<PlayerController>().gainBlood(2);
    }
}
