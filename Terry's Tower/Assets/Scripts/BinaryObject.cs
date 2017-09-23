using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryObject : MonoBehaviour {
    // for doors / objects activated / deactivated by a trigger
    public bool buttonState = false; //save
    public bool objectState = false; //save
    public bool canToggle; //save
    public int objectLocationX; //save
    public int objectLocationY; //save
    public GameObject objectState0; //in prefab
    public GameObject objectState1; //in prefab
    public GameObject buttonState0; //in prefab
    public GameObject buttonState1; //in prefab
    public bool buffer = false;
    public float timeBuffer = 1;


    // Use this for initialization
	void Start () {
        //tells game where to place the door. it will be offset from the button
        objectState0.transform.localPosition = new Vector2(objectLocationX, objectLocationY);
        objectState1.transform.localPosition = new Vector2(objectLocationX, objectLocationY);

        UpdateSprites();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle()
    {
        if(canToggle)
        {
            buttonState = !buttonState;
            objectState = !objectState;
        }
        else if(buttonState  == false)
        {
            buttonState = true;
            objectState = true;
        }

        UpdateSprites();
    }

    void UpdateSprites()
    {
        if(buttonState)
        {
            buttonState0.SetActive(false);
            buttonState1.SetActive(true);
        }
        else
        {
            buttonState0.SetActive(true);
            buttonState1.SetActive(false);
        }

        if (objectState)
        {
            objectState0.SetActive(false);
            objectState1.SetActive(true);
        }
        else
        {
            objectState0.SetActive(true);
            objectState1.SetActive(false);
        }
    }
}
