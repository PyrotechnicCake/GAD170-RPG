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
            StartCoroutine(doBattle());
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
                //victory
                //tell the player they won the battle
                combatState = CombatState.EnemyTurn;
                break;

            //enemy turn
            case CombatState.EnemyTurn:
            //decision attack
            //attac player
            BattleRound(enemyObj, playerObj);
                //is player dead?
                if (playerObj.GetComponent<Stats>().isDefeated)
                {
                    print("you died");
                    combatState = CombatState.Loss;
                    //loss
                    //the player lost
                    case CombatState.Loss:
                    SceneManager.LoadScene("SampleScene");
                    
                }
            combatState = CombatState.PlayerTurn;
            break;

        }
    }
    public void BattleRound(GameObject attacker, GameObject defender)
    IEnumerator battleGo()
    {
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().str, Stats.StatusEffect.none);
        print(attacker.name + "dealt" + (attacker.GetComponent<Stats>().attack - defender.GetComponent + defender.name )
    }
