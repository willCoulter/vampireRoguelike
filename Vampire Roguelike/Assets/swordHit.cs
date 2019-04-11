using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordHit : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("oof");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
        }
    }
}
