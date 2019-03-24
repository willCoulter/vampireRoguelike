using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ExplosionSkill")]
public class ExplosionSkill : Skill
{
    public int damage = 10;
    public LayerMask enemiesMask;
    public float radius = 1f;
    private Transform spawnLocation;
    public GameObject player;
    public Transform playerPos;
    Vector3 positionOfCircle;

    public override void Initialize(GameObject obj)
    {
        if (obj != null)
        {
            spawnLocation = obj.transform;
        }
    }

    void Update()
    {
        positionOfCircle = player.transform.position;
        Debug.Log(playerPos.position);
    }

    public override void TriggerSkill()
    {
        Debug.Log(name + " triggered");
        Debug.Log(playerPos.position);
        //Vector3 positionOfCircle;
        positionOfCircle = player.transform.position;
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(positionOfCircle, radius, enemiesMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("Enemy hit");
            enemies[i].GetComponent<Enemy>().takeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, radius);

    }
}
