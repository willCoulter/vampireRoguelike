using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardBoss : Enemy
{
    public GameObject followingFireBall;
    public GameObject shield;
    public Transform launchPosition;
    private float attackLag;
    public float startLag;

    private void FixedUpdate()
    {
        attackFacing();
        ShootFireBall();
    }


    //Fires a fire ball based on the attack cooldown
    public void ShootFireBall()
    {
        if (attackLag <= 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= agroRadius) {
                GameObject fireBall = Instantiate(followingFireBall, launchPosition.position, launchPosition.rotation);
                fireBall.GetComponent<FollowingFireBall>().wizardShield = transform;
                attackLag = startLag;
            }
        }
        else
        {
            attackLag -= Time.deltaTime;
        }
    }

    private void attackFacing()
    {
        //grabs the differnce in the objects positions and the mouses
        Vector2 direction = new Vector2(
         target.transform.position.x - transform.position.x,
         target.transform.position.y - transform.position.y
         );
        //updates the objects transfrom from the right
        launchPosition.transform.right = direction;

    }
}
