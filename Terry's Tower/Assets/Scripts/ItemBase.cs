using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    public int value;
    //public bool active = true;
    public string itemName;
    public Item itemType;
}

public enum Item
{
    BronzeKey,
    GoldKey,
    SilverKey,
    Shield,
    Sword

}
