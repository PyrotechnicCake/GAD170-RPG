﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    //create list
    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;
    public int hit;

    private GameObject gameManager;
    private GameObject battleUIManager;

    public event System.Action<bool, float> UpdateHealth;

    public void Awake()
    {
        //sub to battle ui manager
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallAttack += checkCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallDefend += checkCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallMagic += checkCombatState;
        //in future implementing an enum that control's the player's decisions would be ideal
    }

    public enum GameState
    {
        notInCombat,
        inCombat
    }
    public GameState gameState;

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Loss
    }
    public CombatState combatState;
    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;

    private bool doBattle = true;

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Random.Range(3, 5); i++)
        {
            //add a random food item from the buffet to our plate, remember they are already different types!
            GameObject enemy = Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
            enemyList.Add(enemy);
        }
        setTarget();
        BattleStart();

        //find our gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    //Start a battle
    void BattleStart()
    {
        if (doBattle)
        {
            //start the battle, the object with higher spd goes first
            //if spd is the same the fist turn is random
            if (enemyList.Count > 0)
            {
                if (playerObj.GetComponent<Stats>().spd > enemyObj.GetComponent<Stats>().spd)
                {
                    combatState = CombatState.PlayerTurn;
                }
                else
                {
                    if (playerObj.GetComponent<Stats>().spd < enemyObj.GetComponent<Stats>().spd)
                    {
                        combatState = CombatState.EnemyTurn;
                    }
                    else
                    {
                        int coinFlip = Random.Range(0, 2);
                        combatState = (CombatState)coinFlip;
                    }
                }
            }
            StartCoroutine(battleGo());
            doBattle = false;
        }
        /*foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().HP -= 10;
        }*/
        
    }

    private void Update()
    {
        BattleStart();
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
        Destroy(enemyToRemove);
        setTarget();
    }

    
    public void SpawnEnemy()
    {
        //Spawn an enemy from our list, use a random range
        Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
    }
    

    public void checkCombatState()
    {
        switch (combatState)
        {
            //player turn
            case CombatState.PlayerTurn:
                //check for enemies to fight
                if (enemyList.Count == 0)
                {
                    //if there are no enemies left you win
                    combatState = CombatState.Win;
                    break;
                }
                else
                {
                    //decision (attac)
                    //attac the enemy
                    BattleRound(playerObj, enemyObj);
                    //check if enemy is defeated
                    if (enemyObj.GetComponent<Stats>().isDefeated)
                    {
                        RemoveEnemy(enemyObj);
                        SpawnEnemy();
                    }
                    //switch to enemy turn
                    combatState = CombatState.EnemyTurn;
                }
                break;
            //enemy turn
            case CombatState.EnemyTurn:
                //check if there is a enemy alive to take it's turn
                if (enemyList.Count == 0)
                {
                    //if there are no enemies left you win
                    combatState = CombatState.Win;
                    break;
                }
                else
                {
                    //decision attack
                    //attac player
                    BattleRound(enemyObj, playerObj);
                    //is player dead?
                    if (playerObj.GetComponent<Stats>().isDefeated)
                    {
                        //the player died so set the state to loss
                        combatState = CombatState.Loss;
                        print("you died");
                        break;
                    }
                    //Change to player turn
                    combatState = CombatState.PlayerTurn;
                    break;
                }
            //victory
            case CombatState.Win:
                print("a winner is you");

                //return to the overworld
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                break;
            //tell the player they won
            //end the game
            case CombatState.Loss:
                //we lose reset game
                //loss
                //tell the player they're a loser
                //reset the game
                SceneManager.LoadScene("SampleScene");
                break;
        }

    }

    public void setTarget()
    {
        if (enemyList.Count > 0)
            enemyObj = enemyList[0];
        else
            combatState = CombatState.Win;
    }

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //this function takes an attacker and a defender and compares their stats and makes them fight
        //A hit/miss chance will need to be added bassed off stats, a skill stat has been added for this
        //skill will be compared to spd and mayb luck too?
        hit = 10 * Mathf.RoundToInt((attacker.GetComponent<Stats>().skill * 2) - (defender.GetComponent<Stats>().spd + ((float)defender.GetComponent<Stats>().luck / 2)));
        if (hit < 0)
            hit = 0;
        if (hit > 100)
            hit = 100;
        //does the attacker hit?
        if (hit > Random.Range(1, 101))
        {
            defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().str, Stats.StatusEffect.none);
            Debug.Log(attacker.name +
                " attacked " +
                defender.name +
                " and dealt " +
                (defender.GetComponent<Stats>().hurt) +
                " damage");
        }
        else
        //if missed
        Debug.Log(attacker.name +
            " attacked " +
            defender.name +
            " but they missed!");


        float percentage = defender.GetComponent<Stats>().HP / defender.GetComponent<Stats>().maxHP;
        if (combatState == CombatState.PlayerTurn)
            UpdateHealth(combatState == CombatState.PlayerTurn, percentage);
    }

    IEnumerator battleGo()
    {
        checkCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }
}