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
    public int goldValue;
    public bool stunned = false;
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject bloodParticle;
    public GameObject healEffectParticlePrefab;
    public GameObject bloodPool;
    public float agroRadius;
    public float miniumRange;
    private GameObject healingEffect;
    public UnityEvent OnDestroy;

    public AudioSource enemySounds;
    public AudioClip hurtClip;
    public AudioClip deathClip;

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
        player = PlayerController.instance.gameObject;
        target = player.transform;
        this.GetComponent<Pathfinding.AIDestinationSetter>().target = target;
        //target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            //Check the enemies health
            if (health <= 0)
            {
                //If they have less than or equal to 0 health kill the enemy with the die function and reward the player with gold
                player.GetComponent<PlayerController>().gainGold(goldValue);
                GameManager.instance.enemiesSlain++;
                Die();
            }
            if (stunned == false) { 
            if (CanFollow() == true)
            {
                FollowPlayer();
            }
            //ParticleStopper();
            ChangeDirection();
            }
            else
            {
                gameObject.GetComponent<Pathfinding.AIPath>().canMove = false;
                gameObject.GetComponent<Pathfinding.AIPath>().canSearch = false;
            }
        }
    }
    //Called by other gameobjects like the player accepts a damage amount of type float
    public void takeDamage(float damage)
    {

        enemySounds.PlayOneShot(hurtClip);
        //Spawn a bloodparticle
        GameObject healingEffect = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        //Decrease the health of the enemy by the players damage
        health -= damage;
        sprite.color = Color.red;
        player.GetComponent<PlayerController>().gainBlood(2);
        StartCoroutine(DestroyParticle(healingEffect));
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
        Gizmos.DrawWireSphere(transform.position, miniumRange);
    }

    public void Die()
    {
        //play the death sound effect
        AudioManager.instance.audioSource.PlayOneShot(deathClip);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        //Drops a blood pool and give it blood equal to half the enemies hp
        GameObject bloodpool = Instantiate(bloodPool, transform.position, Quaternion.identity);
        bloodpool.GetComponent<bloodPool>().bloodAmount = maxHealth/2;
        //Finally kill the enemy
        Destroy(gameObject);
    }
    //Checks if the enemy is able to follow the player
    public bool CanFollow()
    {      
        float distance = Vector3.Distance(target.position, transform.position);
        Debug.Log(distance);
        if(distance <= miniumRange)
        {
            gameObject.GetComponent<Pathfinding.AIPath>().canMove = false;
            gameObject.GetComponent<Pathfinding.AIPath>().canSearch = false;
            return false;
        }
        else
        {
            return true;
        }
    }
    //updates the A* scripts with information if the player is within agro range
    public void FollowPlayer()
    {
        //Movement
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= agroRadius)
        {
            //Grabs and updates the aipath script 
            gameObject.GetComponent<Pathfinding.AIPath>().canMove = true;
            gameObject.GetComponent<Pathfinding.AIPath>().canSearch = true;
            gameObject.GetComponent<Pathfinding.AIPath>().maxSpeed = speed;
            anim.SetBool("Moving", true);
        }
        else
        {
            gameObject.GetComponent<Pathfinding.AIPath>().canMove = false;
            gameObject.GetComponent<Pathfinding.AIPath>().canSearch = false;
            anim.SetBool("Moving", false);
        }
    }
    //Heals the enemy by a given amount if they are missing health
    public void Heal(float healAmount)
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

    //public void ParticleStopper()
    //{
       // if (healingEffect != null && healingEffect.GetComponent<ParticleSystem>().isStopped)
        //{
            //Destroy(healingEffect);
       // }
    //}
    //Flips the enemy spirite so they always face the player
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

    public void GetStunned()
    {
        stunned = true;
        StartCoroutine(StunTime());
    }

    public IEnumerator DestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(particle);
    }

    public IEnumerator StunTime()
    {
        yield return new WaitForSeconds(2.0F);
        stunned = false;

    }
}
