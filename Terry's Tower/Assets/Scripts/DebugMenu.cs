using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {

    public Text currentMap;
    public Text inputText;
    public Text messageText;
    public Text enemyMoveParameters;
    public Text enemyNextMove;

    private void Start()
    {
        Globals.theDebugMenu = gameObject.GetComponent<DebugMenu>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        currentMap.text = " Current Map " + Globals.currentMap;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
           if(inputText.text == "toggleNoClip")
            {
                Globals.Player.GetComponentInChildren<CapsuleCollider2D>().enabled = !Globals.Player.GetComponentInChildren<CapsuleCollider2D>().enabled;
            }
        }
    }
}
