using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public Text[] optionTitles;
    public Button[] optionButtons;
    public Text[] optionLevels;

    public int selectedButton = 0;
	// Use this for initialization
	void Start () {        
        try
        {
            UpdateOptions();
            AdjustButtonSize();
        }

        catch
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectedButton > 0)
            {
                selectedButton--;
            }
            else
            {
                selectedButton = optionButtons.Length - 3;
            }
            AdjustButtonSize();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedButton < optionButtons.Length - 3)
            {
                selectedButton++;
            }
            else
            {
                selectedButton = 0;
            }
            AdjustButtonSize();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {           
            if(selectedButton == 0 && Globals.soundEffectVolume < 10)
            {
                Globals.soundEffectVolume++;               
               StartCoroutine(ButtonGrow(optionButtons[1]));
                UpdateOptions();
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);
            }
            else if(selectedButton == 1 && Globals.musicVolume < 10)
            {
                Globals.musicVolume++;
                StartCoroutine(ButtonGrow(optionButtons[3]));
                UpdateOptions();
            }           
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (selectedButton == 0 && Globals.soundEffectVolume > 0)
            {
                Globals.soundEffectVolume--;
                StartCoroutine(ButtonGrow(optionButtons[0]));
                UpdateOptions();
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);
            }
            else if (selectedButton == 1 && Globals.musicVolume > 0)
            {
                Globals.musicVolume--;
                StartCoroutine(ButtonGrow(optionButtons[2]));
                UpdateOptions();
            }
           
        }

        if(Input.GetKeyDown(KeyCode.Space) && selectedButton == optionButtons.Length -3)
        {
            Close();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    void AdjustButtonSize()
    {
        
        if (selectedButton  < optionTitles.Length)
        {
            for(int x = 0; x<optionTitles.Length; x++)
            {
                if(x == selectedButton)
                {
                    optionTitles[x].fontSize = 35;
                }
                else
                {
                    optionTitles[x].fontSize = 30;
                }
            }
            optionButtons[optionButtons.Length-1].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            optionButtons[optionButtons.Length - 1].GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            foreach(Text t in optionTitles)
            {
                t.fontSize = 30;
            }
        }
    }


    void UpdateOptions()
    {
        optionLevels[0].text = Globals.soundEffectVolume.ToString();
        optionLevels[1].text = Globals.musicVolume.ToString();        
        Globals.soundEffectSource.volume = (float)(Globals.soundEffectVolume * .1);
        Globals.musicSource.volume = (float)(Globals.musicVolume * .1);
    }

   void Close()
    {
        gameObject.SetActive(false);
        Globals.theLevelMaster.menuOpen = 0;
    }

   

    IEnumerator ButtonGrow(Button b)
    {
        b.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(.05f);
        b.transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }
}
