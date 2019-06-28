using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //create list
    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;

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
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(doBattle)
        {
            //start the battle, the object with higher spd goes first
            //if spd is the same the fist turn is random
            StartCoroutine(battleGo());
            doBattle = false;
        }
        /*foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().HP -= 10;
        }*/
        
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
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
                //decision (attac)
                //attac the enemy
                BattleRound(playerObj, enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                    SpawnEnemy();
                //switch to enemy turn
                combatState = CombatState.EnemyTurn;
                break;
            //enemy turn
            case CombatState.EnemyTurn:
                //decision attack
                //attac player
                BattleRound(enemyObj, playerObj);
                //is player dead?
                if(playerObj.GetComponent<Stats>().isDefeated)
                {
                    //the player died so set the state to loss
                    combatState = CombatState.Loss;
                    print("you died");
                    break;
                }
                //Change to player turn
                combatState = CombatState.PlayerTurn;
                break;
            //victory
            case CombatState.Win:
                print("a winner is you");
                
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

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //this function takes an attacker and a defender and compares their stats and makes them fight
        //A hit/miss chance will need to be added bassed off stats, a skill stat has been added for this
        //skill will be compared to spd and mayb luck too?
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().str, Stats.StatusEffect.none);
        Debug.Log(attacker.name +
            " attacked " +
            defender.name +
            " and dealt " +
            (defender.GetComponent<Stats>().hurt) +
            " damage");
    }

    IEnumerator battleGo()
    {
        checkCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }
}
