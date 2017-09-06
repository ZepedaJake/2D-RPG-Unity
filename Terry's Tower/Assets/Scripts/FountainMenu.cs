using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FountainMenu : MonoBehaviour {
    public GameObject[] buttons;
    int selection = 0;
	// Use this for initialization
	void Start () {
		if(Globals.playerScript.storyProgress < 8)
        {
            buttons[2].SetActive(false);
        }
        if(Globals.playerScript.storyProgress < 12)
        {
            buttons[3].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            SelectionDown();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            SelectionUp();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(selection)
            {
                case 0:
                    {
                        StartCoroutine(Teleport("02-02", -13, -10));
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(Teleport("01-10", 0, 2));
                        break;
                    }
                case 4:
                    {
                        gameObject.SetActive(false);
                        Globals.playerScript.canMove = true;
                        break;
                    }
                default:
                    break;
            }
        }
	}

    void SelectionDown()
    {
        if (selection > 0)
        {
            selection--;
            if(buttons[selection].activeSelf == false)
            {
                SelectionDown();
            }
        }
        else
        {
            selection = 4;
        }
        AdjustButtonSize();
    }

    void SelectionUp()
    {
        if (selection < 4)
        {
            selection++;
            if (buttons[selection].activeSelf == false)
            {
                SelectionUp();
            }
        }
        else
        {
            selection = 0;
        }
        AdjustButtonSize();
    }

    void AdjustButtonSize()
    {
        for (int x = 0; x < buttons.Length; x++)
        {
            if (x == selection)
            {
                buttons[x].GetComponent<Transform>().localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            else
            {
                buttons[x].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    //teleports between fountains. m = map. x and y are position to set to.
    IEnumerator Teleport(string m, int x, int y)
    {
        Globals.serializer.SavePlayerData();
        Globals.serializer.SaveEnemies();
        Globals.serializer.SaveItems();
        Globals.serializer.SaveDoors();
        Globals.transition.startTrans = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(m);
        Globals.playerScript.gameObject.transform.position = new Vector2(x, y);
        Globals.currentMap = m;
        Globals.theLevelMaster.UpdateUI();
        gameObject.SetActive(false);
        yield return null;
    }
}
