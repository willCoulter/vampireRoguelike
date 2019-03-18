﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : ScriptableObject
{
    public string name = "New Skill";
    public string desc = "New Desc";
    public Sprite skillSprite;
    public AudioClip skillSound;

    public float baseCD = 1f;
    public float baseCost = 0f;

    public abstract void Initialize(GameObject obj);

    public abstract void TriggerSkill();

}
