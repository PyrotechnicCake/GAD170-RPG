using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Default the player stats

    int playerlvl;
    int maxHP;
    int HP;
    int str;
    int def;
    int spd;
    int luck;
    int exp;
    int growth1;

    // Default the enemy stats

    int enelvl;

    // Start is called before the first frame update
    void Start()
    {
        growth1 = 20 + playerlvl * 10;
        if (growth1 > 80)
        {
            growth1 = 80;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //level up the player based off chance
        if(exp >= 100)
        {
            LevelUp();
            exp = 0;
        }
    }

    void LevelUp()
    {
        //check if maxHP increases
        if (Random.Range(0, 100) > growth1)
        {
            maxHP += 1;
        }
        //check if str increases
        if (Random.Range(0, 100) > growth1)
        {
            str += 1;
        }
        //check if def increases
        if (Random.Range(0, 100) > growth1)
        {
            def += 1;
        }
        //check if spd increases
        if (Random.Range(0, 100) > growth1)
        {
            spd += 1;
        }
        //check if luck increases
        if (Random.Range(0, 100) > growth1)
        {
            luck += 1;
        }
    }
}
