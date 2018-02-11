using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBasedExistence : MonoBehaviour {
    public float destroyPastThisStoryPoint;
	// Use this for initialization
	void Start () {
		if(Globals.playerScript.storyProgress >= destroyPastThisStoryPoint)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
