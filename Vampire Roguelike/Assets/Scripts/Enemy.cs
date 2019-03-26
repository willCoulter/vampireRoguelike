using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float speed;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;

    public UnityEvent OnDestroy;

    private Animator anim;
    public Room room;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Grab an active player from the scene
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Check the enemies health
        if (health <= 0)
        {
            //If they have less than or equal to 0 health kill the enemy with the die function and reward the player with gold
            player.GetComponent<PlayerController>().gainGold(2);
            Die();
        }
    }
    //Called by other gameobjects like the player accepts a damage amount of type float
    public void takeDamage(float damage)
    {
        //Spawn a bloodparticle
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        //Decrease the health of the enemy by the players damage
        health -= damage;
        sprite.color = Color.red;
        player.GetComponent<PlayerController>().gainBlood(2);
    }

    private void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();

        Destroy(gameObject);
    }
}
