using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Button[] mainMenuButtons;
    int selectedButton = 0;    
    public GameObject OptionsMenu;

    public string loadTestMap;
	// Use this for initialization
	void Start () {             
        Globals.currentMap = SceneManager.GetActiveScene().name;
        AdjustButtonSize();

        StartCoroutine(MusicDelay());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.W) && Globals.theLevelMaster.menuOpen == 0)
        {
            if (selectedButton > 0)
            {
                selectedButton--;
            }
            else
            {
                selectedButton = mainMenuButtons.Length - 1;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.S) && Globals.theLevelMaster.menuOpen == 0)
        {
            if (selectedButton < mainMenuButtons.Length - 1)
            {
                selectedButton++;
            }
            else
            {
                selectedButton = 0;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Globals.theLevelMaster.menuOpen == 0)
        {
            switch (selectedButton)
            {
                case 0:
                    StartCoroutine(Continue());
                    break;
                case 1:
                    NewGamePreCheck();
                    break;
                case 2:
                    Options();
                    break;
                case 3:
                    Credits();
                    break;
                case 4:
                    //Quit();
                    StartCoroutine(LoadTest());
                    break;
                default:
                    break;
            }
        }

        //always stop player movement
        if(Globals.playerScript.canMove)
        {
            Globals.playerScript.canMove = false;
        }
    }

    void AdjustButtonSize()
    {
        for (int x = 0; x < mainMenuButtons.Length; x++)
        {
            if (x == selectedButton)
            {
                mainMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                mainMenuButtons[x].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    IEnumerator Continue()
    {
        if (File.Exists(Path.Combine(Globals.serializer.saveData, "player.json")))
        {            
            Globals.transition.startTrans = true;
            yield return new WaitForSeconds(1.5f);
                
            SceneManager.LoadScene(Globals.serializer.SavedFloor());           
            Globals.currentMap = Globals.serializer.SavedFloor();
            Globals.soundHandler.SetMusic();
            Globals.theLevelMaster.UpdateUI();
            Globals.serializer.LoadPlayerData();
            Globals.theLevelMaster.ui.SetActive(true);
            SceneManager.UnloadSceneAsync("00-00-00");

        }
        else
        {

            Debug.Log("there is no save data");
        }
        yield return null;
    }

    void NewGamePreCheck()
    {
        if (File.Exists(Path.Combine(Globals.serializer.saveData, "player.json")))
        {
            Globals.theLevelMaster.boolBox.SetActive(true);
            Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().question.text = "There is already save data\nWould you like to delete it?";
            Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().function = "DeleteOldGame";
            Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().objectInQuestion = gameObject;
        }
        else
        {
            StartCoroutine(NewGame());
        }
        
    }

    void DeleteOldGame(bool choice)
    {
        DirectoryInfo oldSave = new DirectoryInfo(Globals.serializer.saveData);
        if (choice == true)
        {
            foreach(FileInfo f in oldSave.GetFiles())
            {
                f.Delete();
            }
            //File.Delete(Path.Combine(Globals.serializer.saveData, "player.json"));
            StartCoroutine(NewGame());
        }
        else
        {
            //do nothing
        }
    }
    IEnumerator NewGame()
    {        
        Globals.transition.startTrans = true;
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("01-01-01");
        Globals.theLevelMaster.StartDialouge("Newgame", "NewGame");
        //set player at floor spawn
        int spawnX = 0;
        int spawnY = 0;
        Globals.playerScript.gameObject.transform.position = new Vector2(spawnX, spawnY);
        Globals.theLevelMaster.ui.SetActive(true);

        Globals.currentMap = "01-01-01";
        Globals.soundHandler.SetMusic();
        Globals.theLevelMaster.UpdateUI();
        Globals.playerScript.storyProgress = 1;
        SceneManager.UnloadSceneAsync("00-00-00");
        
        yield return null;
    }

    IEnumerator LoadTest()
    {
        Globals.transition.startTrans = true;
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(loadTestMap);

        //set player at floor spawn
        int spawnX = 0;
        int spawnY = 0;
        Globals.playerScript.gameObject.transform.position = new Vector2(spawnX, spawnY);

        Globals.currentMap = (loadTestMap);
        Globals.soundHandler.SetMusic();
        Globals.theLevelMaster.UpdateUI();
        SceneManager.UnloadSceneAsync("00-00-00");
        yield return null;
    }

    void Options()
    {
        Globals.theLevelMaster.menuOpen = 3;
        OptionsMenu.SetActive(true);
    }

    void Credits()
    {

    }

    void Quit()
    {

    }   

    IEnumerator MusicDelay()
    {
        yield return new WaitForSeconds(.1f);
        Globals.soundHandler.SetMusic();
        yield return null;
    }
}
