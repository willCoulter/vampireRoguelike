using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float speed;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;
    public float agroRadius = 10f;

    public UnityEvent OnDestroy;

    private Animator anim;
    private Transform target;
    public Room room;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Grab an active player from the scene
        player = GameObject.Find("Player");
        target = player.transform;
        //target = PlayerController.instance.transform;
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

        //Movement
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= agroRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
    }

    private void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();

        Destroy(gameObject);
    }
}
