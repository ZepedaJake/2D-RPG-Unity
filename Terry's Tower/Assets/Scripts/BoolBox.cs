using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolBox:MonoBehaviour{
    public Text question;
    public string function;
    public bool choice = true;   
    public GameObject yes;
    public GameObject no;
    public GameObject objectInQuestion;
    public bool interact;
    
    void OnEnable()
    {
        
        interact = true;
        SetSize();
    }
   void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.A) && interact)
        {
            choice = true;
            SetSize();
        }
        else if (Input.GetKeyDown(KeyCode.D) && interact)
        {
            choice = false;
            SetSize();
        }

        if (Input.GetKeyDown(KeyCode.Space) && interact)
        {
            interact = false;
            objectInQuestion.SendMessage(function, choice, SendMessageOptions.DontRequireReceiver);
            StartCoroutine(DeactivateSelf());

        }
    }

    void SetSize()
    {
        if (choice)
        {
            yes.transform.localScale = new Vector2(1.5f, 1.5f);
            no.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            no.transform.localScale = new Vector2(1.5f, 1.5f);
            yes.transform.localScale = new Vector2(1, 1);
        }
    }

    IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        Globals.playerScript.canMove = true;
        yield return null;
    }
}
