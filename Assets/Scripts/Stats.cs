using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Initialize stats for everyone!! YAY!!!
    public int playerlvl;
    public float maxHP;
    public float HP;
    public int str;
    public int skill;
    public int def;
    public int spd;
    public int luck;
    public int hurt;
    public int growth;
    public int growthB;
    public int growthC;
    int exp;
    int grantexp;

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

    public void LevelUp()
    {
        growth = (20 + (playerlvl * 10));
        if (growth > 80)
            growth = 80;

        growthB = (20 + ((playerlvl - 5) * 10));
        if (growthB > 70)
            growthB = 70;

        growthB = (10 + ((playerlvl - 10) * 10));
        if (growthC > 60)
            growthC = 60;

        //check if maxHP increases
        if (Random.Range(0, 100) < growth)
        {
            maxHP += 1;
            HP += 1;
            //check if maxHP increases again
            if (Random.Range(0, 100) < growthB)
            {
                maxHP += 1;
                HP += 1;
                //check if maxHP increases again and again
                if (Random.Range(0, 100) < growthC)
                {
                    maxHP += 1;
                    HP += 1;
                }
            }
        }
        //check if str increases
        if (Random.Range(0, 100) < growth)
        {
            str += 1;
            //check if str increases again
            if (Random.Range(0, 100) < growthB)
            {
                str += 1;
                //check if str increases again and again
                if (Random.Range(0, 100) < growthC)
                {
                    str += 1;
                }
            }
        }
        //check if skill increases
        if (Random.Range(0, 100) < growth)
        {
            skill += 1;
            //check if skill increases again
            if (Random.Range(0, 100) < growthB)
            {
                skill += 1;
                //check if skill increases again and again
                if (Random.Range(0, 100) < growthC)
                {
                    skill += 1;
                }
            }
        }
        //check if def increases
        if (Random.Range(0, 100) < growth)
        {
            def += 1;
            //check if def increases again
            if (Random.Range(0, 100) < growthB)
            {
                def += 1;
                //check if def increases again
                if (Random.Range(0, 100) < growthC)
                {
                    def += 1;
                }
            }
        }
        //check if spd increases
        if (Random.Range(0, 100) < growth)
        {
            spd += 1;
            //check if spd increases again
            if (Random.Range(0, 100) < growthB)
            {
                spd += 1;
                //check if str increases again
                if (Random.Range(0, 100) < growthC)
                {
                    spd += 1;
                }
            }
        }
        //check if luck increases
        if (Random.Range(0, 100) < growth)
        {
            luck += 1;
            //check if luck increases
            if (Random.Range(0, 100) < growthB)
            {
                luck += 1;
                //check if str increases again
                if (Random.Range(0, 100) < growthC)
                {
                    luck += 1;
                }
            }
        }

        //increase level
        playerlvl += 1;
    }

    public void giveEXP(GameObject player, GameObject enemy)
    {
        float enelvl = (enemy.GetComponent<Stats>().playerlvl);
        grantexp = Mathf.RoundToInt(enelvl / ((float)playerlvl * 50));
        print("you got " + grantexp + " exp points.");
        exp += grantexp;
    }
}
