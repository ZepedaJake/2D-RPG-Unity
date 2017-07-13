using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour {

    public bool startTrans;
    public bool endTrans;

    Image thisImage;


    void Start()
    {
        thisImage = gameObject.GetComponent<Image>();
        Globals.transition = gameObject.GetComponent<TransitionScript>();      
    }
	void FixedUpdate()
    {
        if(startTrans)
        {
            if(thisImage.color.a < 1)
            {
                Globals.playerScript.canMove = false;
                thisImage.color += new Color(0, 0, 0, .02f);               
            }
            else
            {
                
                startTrans = false;
            }
        }

        if(endTrans)
        {
            if (thisImage.color.a > .05f)
            {
                thisImage.color -= new Color(0, 0, 0, .02f);
            }
            else
            {
                Globals.playerScript.canMove = true;
                endTrans = false;
            }
        }
            
    }

    
}
