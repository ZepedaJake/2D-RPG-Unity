﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {

	
    public string requiredItem;
    public bool placed;

    public GameObject glow;
    public GameObject item;
    public GameObject color;
	// Update is called once per frame
	void Update () {
		
	}
    void Start()
    {
        ChangeOrbState();
    }
    public void PlaceOrb(bool choice)
    {
        if (choice)
        {
            //PlaceOrb
            int index = Globals.playerScript.inventory.FindIndex(a => a.inventoryItemName == requiredItem);
            try
            {
                Globals.playerScript.inventory.RemoveAt(index);
            }
            catch { }
            Globals.playerScript.redOrbState = 1;


            if (!Globals.playerScript.redOrbActivated)
            {
                Globals.playerScript.redOrbActivated = true;
            }          
        }
        ChangeOrbState();
    }

    public void TakeOrb(bool choice)
    {
        if (choice)
        {
            Globals.playerScript.inventory.Add(new InventoryItemBase(requiredItem));
            Globals.playerScript.redOrbState = 0;
        }
        ChangeOrbState();
    }

    void ChangeOrbState()
    {
        if (Globals.playerScript.redOrbState == 1)
        {
            glow.SetActive(true);
            item.SetActive(true);
            placed = true;
        }
        else
        {
            placed = false;
            glow.SetActive(false);
            item.SetActive(false);
        }
    }
}
