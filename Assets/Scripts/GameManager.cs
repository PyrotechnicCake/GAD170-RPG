using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Worlds
    {
        Overworld,
        BattleScene
    }
    //void awake is called before void start on ANY OBJECT LMAO
    void Awake()
    {
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
}
