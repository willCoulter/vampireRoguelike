using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit");
        if(other.gameObject.name == "Mimic") {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<Enemy>().takeDamage(2);
        }
    }
}
