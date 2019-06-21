using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().HP -= 10;
        }
        
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
                playerObj.GetComponent<Player>().AttackTarget(enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Enemy>().myStats.isDefeated)
                    SpawnEnemy();
                break;

            //enemy turn
            case CombatState.EnemyTurn:
            //decision attack
            //attac player
            enemyObj.GetComponent<Enemy>().AttackTarget(playerObj);
                //is player dead?
            if (playerObj.GetComponent<Player>().myStats.isDefeated)
                    print("you died");
            break;

            //victory
            //tell the player they won the battle

            //loss
            //the player lost
        }
    }
}
