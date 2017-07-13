using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    public int value;
    public bool active = true;
    public Item itemName;
}

public enum Item
{
    BronzeKey,
    BronzeShield,
    BronzeSword,

    CorruptedShield,
    CorruptedSword,
   
    GoldKey,
    GoldShield,
    GoldSword,

    SilverKey,
    SilverShield,
    SilverSword


}
