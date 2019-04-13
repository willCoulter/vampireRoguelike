using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBoss : Enemy
{
    public Transform launchPosition;
    private float attackLag;
    public float startLag;
    private bool attacking;
    public float cooldownCounter;
    public float fireBallDamage;
    public float fireBeamDamage;
    public float fireBeamCooldown;
    public GameObject smallFireBall;
    public float dur;
    private bool fireBeam = false;

    private void FixedUpdate()
    {
        AttackFacing();
        FireBeamAttack();
        NormalAttack();
    }

    //Updates the enemy's player tracking
    private void AttackFacing()
    {
        if (attacking != true)
        {
            //grabs the differnce in the positions and the mouses
            Vector2 direction = new Vector2(
             target.transform.position.x - transform.position.x,
             target.transform.position.y - transform.position.y
             );
            //updates the transfrom from the right
            launchPosition.transform.right = direction;
        }

    }
    //This method is the bosses basic attack which  spawns and shoots a fire ball.
    private void NormalAttack()
    {
        //Only attack if the beam isn't firing and the lag is 0
        if (attackLag <= 0 && fireBeam == false)
        {
            //Grab the distance between the monster and the player
            float distance = Vector3.Distance(target.position, transform.position);
            //Check to make sure the player is in range
            if (distance <= agroRadius)
            {
                //Spawn a fireball object
                GameObject fireBall = Instantiate(smallFireBall, launchPosition.position, launchPosition.rotation);
                //Set the objects damage
                fireBall.GetComponent<arrow>().damage = damage;
                //Reset the attack cooldown
                attackLag = startLag;
            }           
        }
        else
        {
            //Countdown by seconds
            attackLag -= Time.deltaTime;
        }
    }

    private void FireBeamAttack()
    {
        if (attackLag <= 0 && cooldownCounter <= 0)
        {
            //Beam is firing
            fireBeam = true; 
            //Checks if the duriton is 0 if it is it ends the beam and resets all the counters
            if (dur <= 0) {
                attackLag = startLag;
                cooldownCounter = fireBeamCooldown;
                dur = 10;
                fireBeam = false;
            }
            //Spawn constant fire balls and lower the duration
            else
            {
                GameObject fireBall = Instantiate(smallFireBall, launchPosition.position, launchPosition.rotation);
                fireBall.GetComponent<arrow>().damage = fireBeamDamage;
                dur -= Time.deltaTime;
            }
            
        }
        else
        {
            
            cooldownCounter -= Time.deltaTime;
        }
    }
}