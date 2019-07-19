using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //call my stats
    public Stats myStats;

    private GameObject BattleManager;


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
        //find our gamemanager
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager");

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

    /*
    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.HP -= incDmg - myStats.def;
        myStats.myStatus = incEffect;
        if (myStats.HP <= 0)
            myStats.isDefeated = true;
    }

    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Player>().Attacked(myStats.str, Stats.StatusEffect.none);
    }
    */
    public void defeated()
    {
        BattleManager.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }

}
