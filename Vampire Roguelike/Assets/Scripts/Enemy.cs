using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float speed;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;
    public float agroRadius = 10f;
    private Animator anim;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            player.GetComponent<PlayerController>().gainGold(2);
            Destroy(gameObject);
            
        }

        //Movement
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= agroRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
    }
}
