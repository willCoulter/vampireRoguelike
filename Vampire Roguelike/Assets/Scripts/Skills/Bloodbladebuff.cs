using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodbladebuff : MonoBehaviour
{
    private float damage;
    private float buffedDamage;

    public static Bloodbladebuff Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void BuffDamage(float multipler, float dur)
    {
        StartCoroutine(Buff(multipler,dur));


    }

    public IEnumerator Buff(float multipler, float dur)
    {
        PlayerController.instance.sword.GetComponent<SpriteRenderer>().color = Color.red;
        damage = PlayerController.instance.attackDamage;
        buffedDamage = damage * multipler;
        PlayerController.instance.attackDamage = buffedDamage;
        yield return new WaitForSeconds(dur);
        PlayerController.instance.attackDamage = damage;
        PlayerController.instance.sword.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);



    }


}


