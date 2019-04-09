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
        shootFireBall();
    }

    public void shootFireBall()
    {
        if (attackLag <= 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= agroRadius) {
                GameObject fireBall = Instantiate(followingFireBall, launchPosition.position, launchPosition.rotation);
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
        //grabs the differnce in the swords positions and the mouses
        Vector2 direction = new Vector2(
         target.transform.position.x - transform.position.x,
         target.transform.position.y - transform.position.y
         );
        //updates the swords transfrom from the right
        launchPosition.transform.right = direction;

    }
}
