using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {

    public Text currentMap;
    public Text inputText;
    public Text messageText;
	// Update is called once per frame
	void FixedUpdate () {
        currentMap.text = Globals.currentMap;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {

        }
    }
}
