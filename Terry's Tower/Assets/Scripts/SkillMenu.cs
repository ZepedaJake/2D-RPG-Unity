using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour {
    public Text[] skillNames;
    public Text[] skillLevels;
    public Text[] skillMult;
    public Text skillPoints;

    public Button[] skillMenuButtons;

    int selectedButton = 4;

    LevelMaster theLevelMaster;
    // Use this for initialization
    void Start () {
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<LevelMaster>();

        try
        {
            UpdateSkills();
        }

        catch
        {

        }
    }

    private void OnEnable()
    {
        UpdateSkills();   
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectedButton > 0 )
            {
                selectedButton--;
            }
            else
            {
                selectedButton = skillMenuButtons.Length - 1;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedButton < skillMenuButtons.Length - 1)
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
                    BasicAttackUp();
                    break;
                case 1:
                    PowerAttackUp();
                    break;
                case 2:
                    DefendUp();
                    break;
                case 3:
                    HealUp();
                    break;
                case 4:
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
        for (int x = 0; x < skillMenuButtons.Length; x++)
        {
            if (x == selectedButton)
            {
                skillMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
               skillMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void BasicAttackUp()
    {
        if (Globals.playerScript.skillPoints > 0 && Globals.playerScript.skills[selectedButton].level<10)
        {
            Globals.playerScript.skills[selectedButton].level += 1;
            Globals.playerScript.skillPoints -= 1;
            Globals.playerScript.spentSkillPoints++;
        }

        UpdateSkills();
    }

    void PowerAttackUp()
    {
        if (Globals.playerScript.skillPoints > 0 && Globals.playerScript.skills[selectedButton].level < 10)
        {
            Globals.playerScript.skills[selectedButton].level += 1;
            Globals.playerScript.skillPoints -= 1;
            Globals.playerScript.spentSkillPoints++;
        }

        UpdateSkills();
    }

    void DefendUp()
    {
        if (Globals.playerScript.skillPoints > 0 && Globals.playerScript.skills[selectedButton].level < 10)
        {
            Globals.playerScript.skills[selectedButton].level += 1;
            Globals.playerScript.skillPoints -= 1;
        }

        UpdateSkills();
    }

    void HealUp()
    {
        if (Globals.playerScript.skillPoints > 0 && Globals.playerScript.skills[selectedButton].level < 10)
        {
            Globals.playerScript.skills[selectedButton].level += 1;
            Globals.playerScript.skillPoints -= 1;
        }

        UpdateSkills();
    }

    void Close()
    {
        gameObject.SetActive(false);
        theLevelMaster.menuOpen = 0;
    }

    void UpdateSkills()
    {
        for (int x = 0; x<4; x++)
        {
            skillNames[x].text = Globals.playerScript.skills[x].skillName + " :";
            skillLevels[x].text = Globals.playerScript.skills[x].level.ToString();
            skillMult[x].text = (((Globals.playerScript.skills[x].level - 1) * (Globals.playerScript.skills[x].multiplier)) * 100).ToString() + "%";
            Globals.playerScript.UpdateSkills();
            skillPoints.text = Globals.playerScript.skillPoints + " Points";
        }
    }
}
