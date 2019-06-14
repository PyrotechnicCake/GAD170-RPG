using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //call my stats
    Stats myStats;

    //enemy enum
    public enum Enemytypes
    {
        small,
        medium,
        large
    }

    public Enemytypes myType;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>(); 
        switch(myType)
        {
            case Enemytypes.small:
                //do setup
                break;
            case Enemytypes.medium:
                //setup
                break;
            case Enemytypes.large:
                //setup
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myStats.maxHP = 4;
    }

    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.HP -= incDmg - myStats.def;
        myStats.myStatus = incEffect;
    }

    public void AttackTarget()
    {
        Attacked(myStats.str, Stats.StatusEffect.none);
    }
}
