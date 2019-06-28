using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Initialize stats for everyone!! YAY!!!
    public int maxHP;
    public int HP;
    public int str;
    public int skill;
    public int def;
    public int spd;
    public int luck;
    public int hurt;

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

    public void Attacked(int incDmg, StatusEffect incEffect)
    {
        hurt = incDmg - def;
        if (hurt < 0)
            hurt = 0;
        HP -= hurt;
        myStatus = incEffect;
        if (HP <= 0)
            isDefeated = true;
    }
}
