using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    //keep track of UI
    private Button attackButton;
    private Button defendButton;
    private Button magicButton;

    public Image pHealthBarFill;
    public Image eHealthBarFill;

    public BattleManager bManager;

    public event System.Action CallAttack;
    public event System.Action CallDefend;
    public event System.Action CallMagic;

    public Text[] combatLogLines;
    public List<string> combatlog;

    private void Awake()
    {
        //UpdateHealthBar(true, amount as a float);
        bManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        bManager.UpdateHealth += UpdateHealthBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DebugLogTest());
    }

    public void UpdateHealthBar(bool isPlayer, float HP)
    {
        //fill amount is a number between 0 and 1
        //will handle fill amount in a script that calls the update healthbar function
        if(isPlayer)
        {
            pHealthBarFill.fillAmount = HP;
        }
        else
        {
            pHealthBarFill.fillAmount = HP;
        }
    }

    public void CallAttackEvent()
    {
        print("Attack");
        CallAttack();
    }
    public void CallDefendEvent()
    {
        print("Defend");
        CallDefend();
    }
    public void CallMagicEvent()
    {
        print("Magic");
        CallMagic();
    }

    public void UpdateCombatLog(string incLog)
    {
        //add a string to the list
        combatlog.Insert(0, incLog);
        //if list length > array length, delete top entry
        if (combatlog.Count > combatLogLines.Length)
        {
            combatlog.RemoveAt(combatlog.Count - 1);
        }
        //loop through arrray and set the text to the strings
        for(int i = 0; i < combatlog.Count; i++)
        {
            combatLogLines[i].text = combatlog[i];
        }

        StartCoroutine(DebugLogTest());
    }
    IEnumerator DebugLogTest()
    {
        int randomNumber = Random.Range(1, 1000);
        yield return new WaitForSeconds(3f);
        UpdateCombatLog(randomNumber.ToString());
    }
}
