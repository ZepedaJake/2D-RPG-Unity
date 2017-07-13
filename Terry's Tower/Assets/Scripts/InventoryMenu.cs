using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour {

    public Text inventoryText;
	
    void OnEnable()
    {
        Debug.Log(Globals.playerScript.inventory);
        inventoryText.text = "";
        foreach(InventoryItemBase i in Globals.playerScript.inventory)
        {
            inventoryText.text += i.inventoryItemName + "\n";
        }

        
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            Globals.theLevelMaster.menuOpen = 0;
        }
	}
}
