using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int damage;
    public float speed;
    public float currentSpeed;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;
    public GameObject healEffectParticlePrefab;
    public GameObject bloodPool;
    public float agroRadius;
    public float miniumRange;
    private GameObject healingEffect;
    public UnityEvent OnDestroy;

    public Animator anim;
    public Transform target;
    public Room room;
    private GameManager gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        //Init hp from maxHp
        health = maxHealth;
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
            GameManager.instance.enemiesSlain++;
            Die();
        }
        if (canFollow() == true)
        {
            followPlayer();
        }
        particleStopper();
        ChangeDirection();
    }
    //Called by other gameobjects like the player accepts a damage amount of type float
    public void takeDamage(float damage)
    {
        //Spawn a bloodparticle
        healingEffect = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        //Decrease the health of the enemy by the players damage
        health -= damage;
        sprite.color = Color.red;
        player.GetComponent<PlayerController>().gainBlood(2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
        Gizmos.DrawWireSphere(transform.position, miniumRange);
    }

    public void Die()
    {
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        GameObject bloodpool = Instantiate(bloodPool, transform.position, Quaternion.identity);
        bloodpool.GetComponent<bloodPool>().bloodAmount = maxHealth/2;
        Destroy(gameObject);
    }

    public bool canFollow()
    {
        
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= miniumRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void followPlayer()
    {
        //Movement
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agroRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    public void heal(float healAmount)
    {
        if (health < maxHealth)
        {
            health += healAmount;
            healingEffect = Instantiate(healEffectParticlePrefab,transform.position,Quaternion.identity);

            if(health >= maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public void particleStopper()
    {
        if (healingEffect != null && healingEffect.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(healingEffect);
        }
    }

    private void ChangeDirection()
    {

        if (player.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
