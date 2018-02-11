using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PlayerSaveData
{
    public int level;
    public int statPoints;
    public int skillPoints;
    public int spentStatPoints;
    public int spentSkillPoints;

    public int currentHealth;
    public int maxHealth;

    public int currentXP;
    public int xpToLevel;

    public int atk;
    public int atkItemBonus;
    public int statAtk;

    public int def;
    public int defItemBonus;
    public int statDef;

    public int skill1Lvl;
    public int skill2Lvl;
    public int skill3Lvl;
    public int skill4Lvl;

    public int lookingX;
    public int lookingY;
    public string direction;

    public int bronzeKeys;
    public int silverKeys;
    public int goldKeys;

    public string currentMap;

    public int redOrbState;
    public int blueOrbState;
   

    public float storyProgress;

    [Serializable]
    public struct PlayerQuests
    {    
        public string questName;
        public string description;
        public bool complete;
        public float progress;
    }

    public List<PlayerQuests> sideQuests;
}

//[Serializable]
//public class MapSaveData
//{
//    public int startPointX;
//    public int startPointY;
//    public int returnPointX;
//    public int returnPointY;
//}

[Serializable]
public struct EnemiesOnMap
{
    [Serializable]
    public struct EnemyInfo
    {
        public string enemyName;
        public int posX;
        public int posY;
        public bool alive;
        public string heldItem;
    }

    public List<EnemyInfo> mapEnemyInfo;
}

[Serializable]
public struct ItemsOnMap
{
    [Serializable]
    public struct ItemInfo
    {
        public string itemName;
        public int value;
        public int posX;
        public int posY;
        //public bool active;
    }

    public List<ItemInfo> mapItemInfo;
}

[Serializable]
public struct DoorsOnMap
{

    [Serializable]
    public struct DoorInfo
    {
        public string doorType;
        public string doorName;
        public int posX;
        public int posY;
    }

    public List<DoorInfo> mapDoorInfo;
}

[Serializable]
public class PlayerInventory
{
    public List<InventoryItemBase> inventoryItems;
}



[Serializable]
public struct MapData
{
    [Serializable]
    public struct Enemy
    {
        public string enemyName;
        public int posX;
        public int posY;
        public bool alive;
        public string heldItem;

    }
    //----------
    public List<Enemy> mapEnemyInfo;

    [Serializable]
    public struct Item
    {
        public string itemName;
        public int value;
        public int posX;
        public int posY;
    }

    public List<Item> mapItemInfo;

    //----------
    [Serializable]
    public struct Door
    {
        //public string doorType;
        public string doorName;
        public int posX;
        public int posY;
    }

    public List<Door> mapDoorInfo;

    [Serializable]
    public struct BinaryObject
    {
        public string objectName;
        public bool buttonState;
        public bool objectState;
        public int buttonX;
        public int buttonY;
        public int objectX;
        public int objectY;
        public bool canToggle;
    }

    public List<BinaryObject> mapBinaryObjectInfo;
}

