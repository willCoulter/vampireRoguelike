using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodBoltHit : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject arrowObject;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(PlayerController.instance.magicDamage);
            Destroy(arrowObject);

        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sword"))
        {

        }
        else
        {
            Destroy(arrowObject);
        }
    }
}
