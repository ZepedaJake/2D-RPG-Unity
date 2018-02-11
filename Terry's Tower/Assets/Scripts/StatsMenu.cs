using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    public Button[] statMenuButtons;
    int selectedButton = 2;
    public Text attackStat;
    public Text defenseStat;
    public Text statPoints;

    LevelMaster theLevelMaster;

    // Use this for initialization
    void Start()
    {
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<LevelMaster>();

        try
        {
            UpdateStats();
        }
        
        catch
        {

        }
    }

    private void OnEnable()
    {
        UpdateStats();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectedButton > 0 )
            {
                selectedButton--;
            }
            else
            {
                selectedButton = statMenuButtons.Length - 1;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedButton < statMenuButtons.Length - 1)
            {
                selectedButton++;
            }
            else
            {
                selectedButton = 0;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (selectedButton)
            {
                case 0:
                    AttackUp();
                    break;
                case 1:
                    DefenseUp();
                    break;
                case 2:
                    Close();
                    break;               
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        { Close(); }        
    }

    void AdjustButtonSize()
    {
        for (int x = 0; x < statMenuButtons.Length; x++)
        {
            if (x == selectedButton)
            {
                statMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                statMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
        }
    } 

    // Update is called once per frame
    public void UpdateStats()
    {
        Globals.playerScript.UpdateStats();
        attackStat.text =  "" +Globals.playerScript.atk.ToString();
        defenseStat.text =  Globals.playerScript.def.ToString();
        statPoints.text = Globals.playerScript.statPoints.ToString() + " Points";              
    }

    public void AttackUp()
    {
        if (Globals.playerScript.statPoints > 0)
        {
            Globals.playerScript.statAtk += 1;
            Globals.playerScript.statPoints -= 1;
            Globals.playerScript.spentStatPoints++;
            //Debug.Log("attack up .. " + (Globals.playerScript.statAtk + Globals.playerScript.atkItemBonus + 3));          
        }

        UpdateStats();
    }
    public void DefenseUp()
    {
        if (Globals.playerScript.statPoints > 0)
        {
            Globals.playerScript.statDef += 1;
            Globals.playerScript.statPoints -= 1;
            Globals.playerScript.spentStatPoints++;
        }

        UpdateStats();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        theLevelMaster.menuOpen = 0;
    }
}
