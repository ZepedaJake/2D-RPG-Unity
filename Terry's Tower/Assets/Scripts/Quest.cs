using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Quest{
    public float progress;
    public string questName;
    public string description;
    public bool complete;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Quest(string n, string d, bool c, float p)
    {
        questName = n;
        description = d;
        complete = c;
        progress = p;
    }

    public Quest(string n, string d)
    {
        progress = 1;
        questName = n;
        description = d;
        complete = false;
    }
}
