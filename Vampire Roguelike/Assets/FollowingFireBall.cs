using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingFireBall : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject fireBall;
    public Transform playerTarget;
    public Transform wizardShield;
    private bool canHitWizard = false;
        public float damage;

    void Start()
    {
    
        GameObject player = GameObject.Find("Player");
        playerTarget = player.transform;

        GameObject wizard = GameObject.Find("Wizard Boss");
        wizardShield = wizard.transform;

    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
        attackFacing();
    }

    public void ChangeTarget()
    {
        if(playerTarget != wizardShield)
        {
            canHitWizard = true;
            playerTarget = wizardShield;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canHitWizard == false) {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
                Destroy(fireBall);
            }
        }
        else if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Sword"))
        {

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canHitWizard == true)
            {
                collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
                Destroy(fireBall);
            }
        }
        else
        {
            Destroy(fireBall);
        }

    }


    private void attackFacing()
    {
        //grabs the differnce in the swords positions and the mouses
        Vector2 direction = new Vector2(
         playerTarget.transform.position.x - transform.position.x,
         playerTarget.transform.position.y - transform.position.y
         );
        //updates the swords transfrom from the right
        fireBall.transform.right = direction;

    }

}
