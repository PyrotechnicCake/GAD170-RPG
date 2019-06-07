using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Default the player stats

    public int playerlvl;
    int maxHP;
    int HP;
    int str;
    int def;
    int spd;
    int luck;
    public int exp;
    int grantexp;
    int growth1;

    // Default the enemy stats

    public int enelvl;

    // Make winchance public (for debugging)
    public int winchance;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the stats
        playerlvl = 1;
        maxHP = 7;
        HP = maxHP;
        str = 3;
        def = 1;
        spd = 2;
        luck = 1;
        enelvl = 1;
        growth1 = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //simulate battle with the push of a button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            winorlose();
        }
        //level up the player based off chance
        if(exp >= 100)
        {
            growth1 = 20 + playerlvl * 10;
            if (growth1 > 80)
            {
                growth1 = 80;
            }
            LevelUp();
            exp = 0;
        }
    }

    void LevelUp()
    {
        //check if maxHP increases
        if (Random.Range(0, 100) < growth1)
        {
            maxHP += 1;
        }
        //check if str increases
        if (Random.Range(0, 100) < growth1)
        {
            str += 1;
        }
        //check if def increases
        if (Random.Range(0, 100) < growth1)
        {
            def += 1;
        }
        //check if spd increases
        if (Random.Range(0, 100) < growth1)
        {
            spd += 1;
        }
        //check if luck increases
        if (Random.Range(0, 100) < growth1)
        {
            luck += 1;
        }
        //increase level
        playerlvl += 1;
    }

    void winorlose()
    {
        int pst = playerlvl + HP + str + def + spd + luck;
        winchance = (pst / 5 - enelvl * 10) + 50;
        if(Random.Range(0,100) < winchance)
        {
            print("You won the battle!");
            giveEXP();
        }
        else
        {
            print("you lose the battle...");
        }

    }

    void giveEXP()
    {
        grantexp = enelvl / playerlvl * 50;
        print("you got " + grantexp + " exp points.");
        exp += grantexp;
    }
}
