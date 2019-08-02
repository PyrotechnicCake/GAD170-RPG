using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;

    List<int> storedPlayerStats;
    Transform storedPlayerTransform;
    public string tracker;

    public enum Worlds
    {
        Overworld,
        BattleScene
    }
    //void awake is called before void start on ANY OBJECT LMAO
    void Awake()
    {
        foreach (GameObject gameMan in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            if(gameMan != this)
            {
                Destroy(gameMan);
            }
        }
        //this will make the object constant between all scenes and will never be destroyed
        DontDestroyOnLoad(this.gameObject);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.Overworld:
                //load overworld
                SceneManager.LoadScene("Overworld");
                break;
            case Worlds.BattleScene:
                //load battlescene
                SceneManager.LoadScene("BattleScene");
                break;
        }
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            //add randome enemies from the list, run script each time the player enters wild grass
            EnemiesToFight.Add(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)]);
        }
    }

    void SavePlayerStuff(bool isFromOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //only save position in overworld
        if(isFromOverworld)
        {

        }
        //save stats that we need
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        Stats playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<stats>();
        playerStats
        //load position in overworld
        if(goingToOverworld)
            playerObj.transform.position = storedPlayerTransform.position;
    }
}
