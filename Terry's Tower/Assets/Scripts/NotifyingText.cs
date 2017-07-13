using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyingText : MonoBehaviour {
    public string text;
    float timer;
    float lifetime = 2;
	
    void Start()
    {
        //transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localPosition = new Vector3(0, 0, 0);
    }
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(0,Mathf.Pow(((lifetime-timer)* .8f),2) ,0);
       float alpha = (lifetime - timer)/2;
        gameObject.GetComponent<Text>().color = new Color(255, 255, 0, alpha);

      
	}
}
