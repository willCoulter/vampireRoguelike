using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followTarget != null)
        {
            Vector3 p = transform.position;

            p.x = Mathf.Lerp(transform.position.x, followTarget.transform.position.x, Time.deltaTime * moveSpeed);
            p.y = Mathf.Lerp(transform.position.y, followTarget.transform.position.y, Time.deltaTime * moveSpeed);

            transform.position = p;
        }
    }
}
