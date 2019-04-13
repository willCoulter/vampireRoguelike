using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealSkill : MonoBehaviour
{
    public static void HealPlayer(float healAmount)
    {
        PlayerController.instance.health += healAmount;
    }
}
