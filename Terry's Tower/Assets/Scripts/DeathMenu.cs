using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().question.text = "Return To Menu?";
        Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().function = "DeathChoice";
        Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().objectInQuestion = gameObject;
        Globals.soundHandler.ForceSong(5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DeathChoice(bool choice)
    {
        if(choice == true)
        {
            ToMainMenu();
        }
        else
        {
            //reload last save
            StartCoroutine(Continue());
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
            SceneManager.UnloadSceneAsync("00-00-00");

        }
        else
        {

            Debug.Log("there is no save data");
        }
        yield return null;
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene("00-00-00");
        Globals.theLevelMaster.ui.SetActive(false);
        Globals.soundHandler.SetMusic();
    }
}
