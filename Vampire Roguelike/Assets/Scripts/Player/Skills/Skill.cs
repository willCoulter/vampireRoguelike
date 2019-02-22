using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName = "New Skill";
    public Sprite skillSprite;
    public AudioClip skillSound;

    public float skillBaseCD = 1f;
    public float skillBaseCost = 0f;

    public abstract void Initialize(GameObject obj);

    public abstract void TriggerAbility();

}
