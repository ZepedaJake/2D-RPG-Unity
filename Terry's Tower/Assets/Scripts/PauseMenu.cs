using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public Button[] buttons;
    public int selectedButton;
    LevelMaster theLevelMaster;
    public GameObject StatsMenu;
    public GameObject SkillsMenu;
    public GameObject OptionsMenu;
    public GameObject InventoryMenu;


	// Use this for initialization
	void Start () {
        Globals.paused = true;
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<LevelMaster>();
        selectedButton = -1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //cycle through buttons with keys.

        //handles selecting buttons and whatever
        if (theLevelMaster.menuOpen == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selectedButton > 0)
                {
                    selectedButton--;
                }
                else
                {
                    selectedButton = buttons.Length - 1;
                }
                AdjustButtonSize();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (selectedButton < buttons.Length - 1)
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
                        Stats();
                        break;
                    case 1:
                        Skills();
                        break;
                    case 2:
                        Inventory();
                        break;
                    case 3:
                        Options();
                        break;
                    case 4:
                        Close();
                        break;
                    case 5:
                        Quit();
                        break;
                    default:
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            { Close(); }

        }
    }

    void AdjustButtonSize()
    {
        for (int x = 0; x < buttons.Length; x++)
        {
            if (x == selectedButton)
            {
                buttons[x].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                buttons[x].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
    public void Stats()
    {
        theLevelMaster.menuOpen = 1;
        StatsMenu.SetActive(true);
    }

    public void Skills()
    {
        theLevelMaster.menuOpen = 2;
        SkillsMenu.SetActive(true);
    }

    public void Inventory()
    {
        theLevelMaster.menuOpen = 3;
        InventoryMenu.SetActive(true);
    }

    public void Options()
    {
        Globals.theLevelMaster.menuOpen = 4;
        OptionsMenu.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Close");
        gameObject.SetActive(false);
        Globals.paused = false;

    }

    public void Quit()
    {

    }
}
