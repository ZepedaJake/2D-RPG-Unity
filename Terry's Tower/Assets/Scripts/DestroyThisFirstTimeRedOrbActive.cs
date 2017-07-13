using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisFirstTimeRedOrbActive : MonoBehaviour {

	// Use this for initialization
	void Start() {
		if(Globals.playerScript.redOrbActivated)
        {
            Destroy(gameObject);
        }
	}
	
	
}
