using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour {
    float timerx = 0;
    float timery = 0;
    float timera = 0;
    public GameObject glow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
        timerx += Time.deltaTime;
        timery += (Time.deltaTime / 4);
        timera += (Time.deltaTime / 2);
        transform.localPosition = new Vector3( Mathf.Sin(timerx)/2,  .1f + (Mathf.Sin(timery))/10, 0);
        glow.GetComponent<SpriteRenderer>().color = new Color(255,255,255,Mathf.Sin(timera));

    }
}
