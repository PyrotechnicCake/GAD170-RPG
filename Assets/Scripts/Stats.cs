﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Initialize stats for everyone!! YAY!!!
    public int maxHP;
    public int HP;
    public int str;
    public int def;
    public int spd;
    public int luck;

    public bool isDefeated;

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        burnt
    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;
}