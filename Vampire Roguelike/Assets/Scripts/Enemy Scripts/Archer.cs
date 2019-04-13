using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    private float attackLag;
    public float startLag;

    private void FixedUpdate()
    {
        attackFacing();
        shootArrow();
    }

    public void shootArrow()
    {
        if (attackLag <= 0)
        {
            Debug.Log("Arrow Shot");
            GameObject bullet = Instantiate(bulletPrefab, launchPosition.position, launchPosition.rotation);
            bullet.GetComponent<arrow>().damage = damage;
            attackLag = startLag;          
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
