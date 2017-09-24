﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour {
    
    public DoorType doorType;
    public string doorName;
}

public enum DoorType
{
    BronzeDoor,
    //BronzeDoorSide,
    GoldDoor,
    //GoldDoorSide,
    //SecretDoor,
    //SecretDoorSide,
    SilverDoor,
    //SilverDoorSide
}
