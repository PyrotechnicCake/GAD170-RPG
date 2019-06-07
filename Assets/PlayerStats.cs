using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   
    //create lives
    
    public int playerLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        ///HelloWorld();
        ///IncreaseLives(2);
        ///DisplayName("Jake", "Thomas");

        //creat functions
        ///mix("baked", 45, "cake");

        ///if(Random.Range(successCalc,100) > 75)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //coin toss
            if (Random.Range(1, 7) > 4)
            {
                
                    IncreaseLives(2);
                print("you win the coin toss.");
            }
        else
        {
                print("you loose the coin toss.");
                IncreaseLives(-2);

            }
        }
    }

    void HelloWorld()
    {
        print("Hello world!");
    }

    void IncreaseLives(int incLives)
    {
        playerLives += incLives;
    }

    void DisplayName(string FirstName, string Lastname)
    {
        print(FirstName + " " + Lastname);
    }

    void ingredients(int amA, string ingA, int amB, string ingB, int amC, string ingC)
    {
        print("There are " + amA + " " + ingA + ", " + amB + " " + ingB + " and " + amC + " " + ingC + ".");
    }

    void mix (string method, int time, string result)
    {
        ingredients(4, "eggs", 3, "tablespoons of sugar", 1, "cup of milk");
        print("They where mixed together and " + method + " for " + time + " minutes to make a " + result + ".");
    }
}
