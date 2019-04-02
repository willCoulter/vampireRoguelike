using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/FlatBuffItem")]
public class FlatBuffItem : Item
{
    public float buffAmount;

    public FlatBuffType buff;

    public enum FlatBuffType{
        Health,
        Blood,
        AttackDamage,
        MagicDamage,
        Speed
    }

    public override void Initialize()
    {
        PlayerController player = PlayerController.instance;

        switch(buff){
            case FlatBuffType.Health:
                player.maxHealth += buffAmount;
                break;
            case FlatBuffType.Blood:
                player.maxBlood += buffAmount;
                break;
            case FlatBuffType.AttackDamage:
                player.attackDamage += buffAmount;
                break;
            case FlatBuffType.MagicDamage:
                player.magicDamage += buffAmount;
                break;
            case FlatBuffType.Speed:
                player.speed += buffAmount;
                break;
            default:
                break;
        }
    }
}
