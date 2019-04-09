using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodPool : MonoBehaviour
{

    public float speed;
    private bool trigger = false;
    public float bloodAmount;
    public GameObject target;
    public GameObject bloodBall;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger == true)
        {
            
            

        }
    }

    public void sendBlood(GameObject followTarget)
    {
        GameObject bloodOrb = Instantiate(bloodBall, transform.position, transform.rotation);
        bloodOrb.GetComponent<bloodOrb>().target = followTarget;
        bloodOrb.GetComponent<bloodOrb>().bloodAmount = bloodAmount;
        bloodOrb.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Vanish");
    }
}
