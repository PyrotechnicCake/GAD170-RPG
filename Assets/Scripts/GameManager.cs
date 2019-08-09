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

    public enum Worlds
    {
        Overworld,
        BattleScene
    }
    private static GameManager gameManRef;

    //void awake is called before void start on ANY OBJECT LMAO
    void Awake()
    {
        if (gameManRef == null)
        {
            gameManRef = this;
            //this will make the object constant between all scenes and will never be destroyed
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

    }

    void Start()
    {

        LoadPlayerStuff(true);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.Overworld:
                //load overworld
                SavePlayerStuff(false);
                SceneManager.LoadScene("Overworld");
                LoadPlayerStuff(true);
                break;
            case Worlds.BattleScene:
                //load battlescene
                SavePlayerStuff(true);
                SceneManager.LoadScene("BattleScene");
                LoadPlayerStuff(false);
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
        Stats playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<Stats>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //only save position in overworld
        if (isFromOverworld)
        {
            PlayerPrefs.SetFloat("PlayerPosx", playerObj.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosy", playerObj.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosz", playerObj.transform.position.z);

            PlayerPrefs.SetFloat("PlayerRotx", playerObj.transform.rotation.x);
            PlayerPrefs.SetFloat("PlayerRoty", playerObj.transform.rotation.y);
            PlayerPrefs.SetFloat("PlayerRotz", playerObj.transform.rotation.z);
        }
        //save stats that we need
        PlayerPrefs.SetFloat("playerHealth", playerStats.HP);
        PlayerPrefs.SetInt("playerCurrentExp", playerStats.playerlvl);
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        Stats playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<Stats>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.GetFloat("playerMaxHealth", playerStats.maxHP);
        PlayerPrefs.GetFloat("playerHealth", playerStats.HP);
        PlayerPrefs.GetInt("playerStrength", playerStats.str);
        PlayerPrefs.GetInt("playerSkill", playerStats.skill);
        PlayerPrefs.GetInt("playerDef", playerStats.def);
        PlayerPrefs.GetInt("playerSpeed", playerStats.spd);
        PlayerPrefs.GetInt("playerLuck", playerStats.luck);


        //load position in overworld
        if (goingToOverworld)
            playerObj.transform.position = new Vector3(PlayerPrefs.GetFloat("playerPosx", 0f),
                                                       PlayerPrefs.GetFloat("playerPosy", 0f),
                                                       PlayerPrefs.GetFloat("playerPosz", 0f));

        playerObj.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("playerRotx", 0f),
                                                        PlayerPrefs.GetFloat("playerRoty", 0f),
                                                        PlayerPrefs.GetFloat("playerRotz", 0f));
    }

    public void DeleteSavedStuff()
    {
        //hard reset
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Overworld");
    }
}
