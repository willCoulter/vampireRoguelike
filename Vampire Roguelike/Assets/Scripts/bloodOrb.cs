using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodOrb : MonoBehaviour
{

    public GameObject target;
    public float bloodAmount;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the player");
            collision.gameObject.GetComponent<PlayerController>().gainBlood(bloodAmount); ;
            Destroy(gameObject);
        }


    }
}
