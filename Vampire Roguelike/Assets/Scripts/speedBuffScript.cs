using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedBuffScript : MonoBehaviour
{
    private float speed;
    private float buffedSpeed;

    public static speedBuffScript Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void BuffSpeed(float multipler, float dur)
    {
        StartCoroutine(Buff(multipler, dur));


    }

    public IEnumerator Buff(float multipler, float dur)
    {
        speed = PlayerController.instance.speed;
        buffedSpeed = speed * multipler;
        PlayerController.instance.speed = buffedSpeed;
        yield return new WaitForSeconds(dur);
        PlayerController.instance.speed = speed;
    }
}
