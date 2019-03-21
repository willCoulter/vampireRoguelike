using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/PercentBuffItem")]
public class PercentBuffItem : Item
{
    public float buffPercent;

    public PercentBuffType buff;

    public enum PercentBuffType{
        Health,
        Blood,
        AttackDamage,
        MagicDamage,
        Speed
    }

    public override void Initialize()
    {
        PlayerController player = PlayerController.instance;
        float buffValue = ConvertToPercent(buffPercent);

        switch(buff){
            case PercentBuffType.Health:
                player.maxHealth *= buffValue;
                Debug.Log(player.maxHealth);
                break;
            case PercentBuffType.Blood:
                player.maxBlood *= buffValue;
                break;
            case PercentBuffType.AttackDamage:
                player.attackDamage *= buffValue;
                break;
            case PercentBuffType.MagicDamage:
                player.magicDamage *= buffValue;
                break;
            case PercentBuffType.Speed:
                player.speed *= buffValue;
                break;
            default:
                break;
        }
    }

    private float ConvertToPercent(float percentValue){
        float convertedValue;
        convertedValue = 1 + (percentValue / 100);
        return convertedValue;
    }
}
