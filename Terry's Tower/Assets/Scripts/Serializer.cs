using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class Serializer : MonoBehaviour {
    //save defaults are only used to set the data used when initializing that map the next time.

    string saveFile;
    string loadFile;
    string fileName;
    public string saveData;
    public string defaults;
    
    CharacterControllerScript player;
   
	// Use this for initialization
	void Start () {
        player = Globals.playerScript;
        //save on disc
        saveData = Path.Combine(Application.persistentDataPath, "SaveData");
        // defaults included in project
        defaults = "Assets/Resources/Defaults/";
        CreateDirectories();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void CreateDirectories()
    {
        if(!Directory.Exists(saveData))
        {
            Directory.CreateDirectory(saveData);
        }

        /*if(!Directory.Exists(defaults))
        {
            Directory.CreateDirectory(defaults);
        }*/
    }

    public void SavePlayerData()
    {
        PlayerSaveData playerData = new PlayerSaveData()
        {
            level = player.level,
            statPoints = player.statPoints,
            skillPoints = player.skillPoints,
            spentStatPoints = player.spentStatPoints,
            spentSkillPoints = player.spentSkillPoints,

            currentHealth = player.currentHealth,
            maxHealth = player.maxHealth,

            currentXP = player.currentXP,
            xpToLevel = player.xpToLevel,

            atk = player.atk,
            atkItemBonus = player.atkItemBonus,
            statAtk = player.statAtk,

            def = player.def,
            defItemBonus = player.defItemBonus,
            statDef = player.statDef,

            skill1Lvl = player.skills[0].level,
            skill2Lvl = player.skills[1].level,
            skill3Lvl = player.skills[2].level,
            skill4Lvl = player.skills[3].level,

            lookingX = (int)player.looking.x,
            lookingY = (int)player.looking.y,
            direction = player.direction,

            bronzeKeys = player.bronzeKeys,
            silverKeys = player.silverKeys,
            goldKeys = player.goldKeys,

            currentMap = Globals.currentMap,

            redOrbState = player.redOrbState,
            blueOrbState = player.blueOrbState,
            storyProgress = player.storyProgress,

            sideQuests = new List<PlayerSaveData.PlayerQuests>()
        };

        foreach(Quest q in player.quests)
        {
            PlayerSaveData.PlayerQuests quest = new PlayerSaveData.PlayerQuests();
            quest.questName = q.questName;
            quest.description = q.description;
            quest.complete = q.complete;
            quest.progress = q.progress;

            playerData.sideQuests.Add(quest);
        }

        string json = JsonUtility.ToJson(playerData);             
        
        string fileName = Path.Combine(saveData, "player.json");

        if(File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        File.WriteAllText(fileName, json);

        SavePlayerInventory();

        //Debug.Log(json);
        Debug.Log(fileName);
    }

    public void SavePlayerInventory()
    {
        PlayerInventory playerInventory = new PlayerInventory();

        try
        {
            foreach (InventoryItemBase i in player.inventory)
            {
                playerInventory.inventoryItems.Add(i);
            }
        }
        catch
        {
            Debug.Log("Empty Inventory");
        }

        string json = JsonUtility.ToJson(playerInventory);

        string fileName = Path.Combine(saveData, "inventory.json");

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        File.WriteAllText(fileName, json);

    }

    public void LoadPlayerInventory()
    {
        try
        {
            fileName = Path.Combine(saveData, "inventory.json");
            loadFile = File.ReadAllText(fileName);

            PlayerInventory load = JsonUtility.FromJson<PlayerInventory>(loadFile);
            if (load.inventoryItems.Count > 0)
            {
                player.inventory.Clear();
                foreach (InventoryItemBase i in load.inventoryItems)
                {
                    player.inventory.Add(i);
                }
            }
        }
        catch
        {
            Debug.Log("Problem loading inventory");
        }
        
        
        
    }

    public void LoadPlayerData()
    {        
        fileName = Path.Combine(saveData, "player.json");
        loadFile = File.ReadAllText(fileName);

        PlayerSaveData load = JsonUtility.FromJson<PlayerSaveData>(loadFile);
        //Debug.Log(load.level+ "" + load.statPoints+""+ load.skillPoints+ "" +load.currentMap);

        player.level = load.level;
        player.statPoints = load.statPoints;
        player.skillPoints = load.skillPoints;
        player.spentStatPoints = load.spentStatPoints;
        player.spentSkillPoints = load.spentSkillPoints;

        player.maxHealth = load.maxHealth;
        player.currentHealth = load.currentHealth;

        player.xpToLevel = load.xpToLevel;
        player.currentXP = load.currentXP;

        player.atk = load.atk;
        player.statAtk = load.statAtk;
        player.atkItemBonus = load.atkItemBonus;

        player.def = load.def;
        player.statDef = load.statDef;
        player.defItemBonus = load.defItemBonus;

        player.skills[0].level = load.skill1Lvl;
        player.skills[1].level = load.skill2Lvl;
        player.skills[2].level = load.skill3Lvl;
        player.skills[3].level = load.skill4Lvl;

        player.looking = new Vector2(load.lookingX, load.lookingY);
        player.direction = load.direction;

        player.bronzeKeys = load.bronzeKeys;
        player.silverKeys = load.silverKeys;
        player.goldKeys = load.goldKeys;

        player.redOrbState = load.redOrbState;
        player.blueOrbState = load.blueOrbState;

        player.storyProgress = load.storyProgress;

        //for (int x = 0; x < load.sideQuests.Count; x++)
        //    {
        //        Debug.Log(load.sideQuests[x].questName);
        //        Debug.Log(load.sideQuests[x].description);
        //        Debug.Log(load.sideQuests[x].complete);
        //        Debug.Log(load.sideQuests[x].progress);
        //    player.quests.Add(new Quest(load.sideQuests[x].questName, load.sideQuests[x].description, load.sideQuests[x].complete, load.sideQuests[0].progress));

        //}
        foreach (PlayerSaveData.PlayerQuests q in load.sideQuests)
        {
            //Debug.Log(q.questName);
            //Debug.Log(q.description);
            //Debug.Log(q.complete);
            //Debug.Log(q.progress);
            player.quests.Add(new Quest(q.questName, q.description, q.complete, q.progress));

            //Debug.Log(player.quests[0].questName);

        }
        LoadPlayerInventory();
    }

    public string SavedFloor()
    {
        //used to load in saved floor
        fileName = Path.Combine(saveData, "player.json");
        loadFile = File.ReadAllText(fileName);

        PlayerSaveData load = JsonUtility.FromJson<PlayerSaveData>(loadFile);
        return load.currentMap;
    }

    //public void SaveMap(int x, int y)
    //{
    //    MapSaveData mapData = new MapSaveData()
    //    {
    //        returnPointX = x,
    //        returnPointY = y
    //    };

    //    string json = JsonUtility.ToJson(mapData);
    //    string fileName = Path.Combine(saveData, SceneManager.GetActiveScene().name + ".json");

    //    if (File.Exists(fileName))
    //    {
    //        File.Delete(fileName);
    //    }

    //    File.WriteAllText(fileName, json);
    //}

    //public void LoadMap(string nextMap)
    //{
    //    fileName = Path.Combine(saveData, nextMap + ".json");
    //    loadFile = File.ReadAllText(fileName);

    //    MapSaveData load = JsonUtility.FromJson<MapSaveData>(loadFile);
    //    player.gameObject.transform.position = new Vector2(load.returnPointX + .5f, load.returnPointY-.5f);
    //}

    /*public void SaveEnemies()
    {
        try
        {


            EnemiesOnMap mapEnemies = new EnemiesOnMap
            {
                mapEnemyInfo = new List<EnemiesOnMap.EnemyInfo>()
            };
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyBase currentEnemy = enemy.GetComponent<EnemyBase>();

                EnemiesOnMap.EnemyInfo newInfo = new EnemiesOnMap.EnemyInfo();
                {
                    newInfo.enemyName = currentEnemy.enemyName.ToString();
                    newInfo.posX = (int)currentEnemy.transform.position.x;
                    newInfo.posY = (int)currentEnemy.transform.position.y;
                    newInfo.alive = currentEnemy.alive;
                    newInfo.heldItem = currentEnemy.heldItem;
                }
                mapEnemies.mapEnemyInfo.Add(newInfo);
            }

            string json = JsonUtility.ToJson(mapEnemies);



            string fileName = Path.Combine(saveData, Globals.currentMap + "enemies.json");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Enemies here");
        }
    }   

    public void SaveEnemiesDefault()
    {
        try
        {


            EnemiesOnMap mapEnemies = new EnemiesOnMap
            {
                mapEnemyInfo = new List<EnemiesOnMap.EnemyInfo>()
            };
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyBase currentEnemy = enemy.GetComponent<EnemyBase>();

                EnemiesOnMap.EnemyInfo newInfo = new EnemiesOnMap.EnemyInfo();
                {
                    newInfo.enemyName = currentEnemy.enemyName.ToString();
                    newInfo.posX = (int)currentEnemy.transform.position.x;
                    newInfo.posY = (int)currentEnemy.transform.position.y;
                    newInfo.alive = currentEnemy.alive;
                    newInfo.heldItem = currentEnemy.heldItem;
                }
                mapEnemies.mapEnemyInfo.Add(newInfo);
                Destroy(enemy);
            }

            string json = JsonUtility.ToJson(mapEnemies);



            string fileName = Path.Combine(defaults, Globals.currentMap + "defaultenemies.json");


            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Enemies here");
        }
    }

    public void LoadEnemies()
    {try
        {
            StartCoroutine(LoadEnemiesDelay());
        }
        catch
        {
            Debug.Log("No Enemies Loaded");
                }
       
    }

    public IEnumerator LoadEnemiesDelay()
    {
        yield return new WaitForSeconds(.5f);

        if (File.Exists(Path.Combine(saveData, Globals.currentMap + "enemies.json")))
        {
            fileName = Path.Combine(saveData, Globals.currentMap + "enemies.json");
        }
        else
        {
            fileName = Path.Combine(defaults, Globals.currentMap + "defaultenemies.json");
            
        }

        Debug.Log("loading enemies from " + fileName);
        loadFile = File.ReadAllText(fileName);

        EnemiesOnMap load = JsonUtility.FromJson<EnemiesOnMap>(loadFile);
        Container enemyContainer = GameObject.FindWithTag("Container").GetComponent<Container>();

        //foreach (EnemiesOnMap.EnemyInfo info in load.mapEnemyInfo)
        //{
            
        //    //load into a list.
        //    //enemyContainer.CreateEnemy(info.enemyName, info.posX, info.posY, info.alive);
        //}

        for(int x = load.mapEnemyInfo.Count -1; x>-1; x--)
        {
            //start new enemy
            //set its info to load[x]
            //create from container
            //clear entry from list
            EnemiesOnMap.EnemyInfo e = load.mapEnemyInfo[x];
            enemyContainer.CreateEnemy(e.enemyName, e.posX, e.posY, e.alive,e.heldItem);
            load.mapEnemyInfo.RemoveAt(x);
        }
        
        yield return null;
    }

    public void SaveItems()
    {
        try
        {


            ItemsOnMap mapItems = new ItemsOnMap
            {
                mapItemInfo = new List<ItemsOnMap.ItemInfo>()
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            {
                ItemBase currentItem = item.GetComponent<ItemBase>();

                ItemsOnMap.ItemInfo newInfo = new ItemsOnMap.ItemInfo();
                {
                    newInfo.itemName = currentItem.itemName.ToString();
                    newInfo.value = currentItem.value;
                    newInfo.posX = (int)currentItem.transform.position.x;
                    newInfo.posY = (int)currentItem.transform.position.y;
                    //newInfo.active = currentItem.active;
                }
                mapItems.mapItemInfo.Add(newInfo);
            }

            string json = JsonUtility.ToJson(mapItems);

            string fileName = Path.Combine(saveData, Globals.currentMap + "items.json");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Items Here");
        }
    }

    public void SaveItemsDefault()
    {
        try
        {


            ItemsOnMap mapItems = new ItemsOnMap
            {
                mapItemInfo = new List<ItemsOnMap.ItemInfo>()
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            {
                ItemBase currentItem = item.GetComponent<ItemBase>();

                ItemsOnMap.ItemInfo newInfo = new ItemsOnMap.ItemInfo();
                {
                    newInfo.itemName = currentItem.itemName.ToString();
                    newInfo.value = currentItem.value;
                    newInfo.posX = (int)currentItem.transform.position.x;
                    newInfo.posY = (int)currentItem.transform.position.y;
                    //newInfo.active = currentItem.active;
                }
                mapItems.mapItemInfo.Add(newInfo);
                Destroy(item);
            }

            string json = JsonUtility.ToJson(mapItems);

            string fileName = Path.Combine(defaults, Globals.currentMap + "defaultitems.json");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Items Here");
        }
    }

    public void LoadItems()
    {
        try
        {
            StartCoroutine(LoadItemsDelay());
        }
        catch
        {
            Debug.Log("No items loaded");
        }
    }

    public IEnumerator LoadItemsDelay()
    {
        yield return new WaitForSeconds(.5f);

        if (File.Exists(Path.Combine(saveData, Globals.currentMap + "items.json")))
        {
            fileName = Path.Combine(saveData, Globals.currentMap + "items.json");
        }
        else
        {
            fileName = Path.Combine(defaults, Globals.currentMap + "defaultitems.json");
        }

        Debug.Log("loading items from " + fileName);
        loadFile = File.ReadAllText(fileName);

        ItemsOnMap load = JsonUtility.FromJson<ItemsOnMap>(loadFile);
        Container itemContainer = GameObject.FindWithTag("Container").GetComponent<Container>();

        //foreach (ItemsOnMap.ItemInfo info in load.mapItemInfo)
        //{
        //    Debug.Log(info.itemName);
        //    itemContainer.CreateItem(info.itemName, info.value, info.posX, info.posY, info.active);
        //}

        for (int x = load.mapItemInfo.Count - 1; x > -1; x--)
        {
            ItemsOnMap.ItemInfo i = load.mapItemInfo[x];
            itemContainer.CreateItem(i.itemName,i.value, i.posX, i.posY);
            load.mapItemInfo.RemoveAt(x);
        }

        yield return null;
    }

    public void SaveDoors()
    {
        try
        {
            DoorsOnMap mapDoors = new DoorsOnMap
            {
                mapDoorInfo = new List<DoorsOnMap.DoorInfo>()
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Door"))
            {
                DoorBase currentItem = item.GetComponent<DoorBase>();

                DoorsOnMap.DoorInfo newInfo = new DoorsOnMap.DoorInfo();
                {
                    newInfo.doorType = currentItem.doorType.ToString();
                    newInfo.doorName = currentItem.doorName;
                    newInfo.posX = (int)currentItem.transform.position.x;
                    newInfo.posY = (int)currentItem.transform.position.y;
                }
                mapDoors.mapDoorInfo.Add(newInfo);
            }

            string json = JsonUtility.ToJson(mapDoors);

            string fileName = Path.Combine(saveData, Globals.currentMap + "doors.json");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Doors Here");
        }
    }

    public void SaveDoorsDefault()
    {
        try
        {
            DoorsOnMap mapDoors = new DoorsOnMap
            {
                mapDoorInfo = new List<DoorsOnMap.DoorInfo>()
            };
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Door"))
            {
                DoorBase currentItem = item.GetComponent<DoorBase>();

                DoorsOnMap.DoorInfo newInfo = new DoorsOnMap.DoorInfo();
                {
                    newInfo.doorType = currentItem.doorType.ToString();
                    newInfo.doorName = currentItem.doorName;
                    newInfo.posX = (int)currentItem.transform.position.x;
                    newInfo.posY = (int)currentItem.transform.position.y;
                }

                mapDoors.mapDoorInfo.Add(newInfo);
                Destroy(item);
            }

            string json = JsonUtility.ToJson(mapDoors);

            string fileName = Path.Combine(defaults, Globals.currentMap + "defaultdoors.json");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, json);
        }
        catch
        {
            Debug.Log("No Doors Here");
        }
    }

    public void LoadDoors()
    {
            StartCoroutine(LoadDoorsDelay());
        
    }

    public IEnumerator LoadDoorsDelay()
    {
        yield return new WaitForSeconds(.5f);
        try
        {           
            if (File.Exists(Path.Combine(saveData, Globals.currentMap + "doors.json")))
            {
                fileName = Path.Combine(saveData, Globals.currentMap + "doors.json");
            }
            else
            {
                fileName = Path.Combine(defaults, Globals.currentMap + "defaultdoors.json");
            }

            Debug.Log("loading doors from " + fileName);
            loadFile = File.ReadAllText(fileName);

            DoorsOnMap load = JsonUtility.FromJson<DoorsOnMap>(loadFile);
            Container itemContainer = GameObject.FindWithTag("Container").GetComponent<Container>();

            //foreach (DoorsOnMap.DoorInfo info in load.mapDoorInfo)
            //{
            //    itemContainer.CreateDoor(info.doorType, info.posX, info.posY);
            //}
            try
            {
                for (int x = load.mapDoorInfo.Count - 1; x > -1; x--)
                {
                    DoorsOnMap.DoorInfo d = load.mapDoorInfo[x];
                    itemContainer.CreateDoor(d.doorType, d.doorName, d.posX, d.posY);
                    load.mapDoorInfo.RemoveAt(x);
                }
            }
            catch
            {
                for (int x = load.mapDoorInfo.Count - 1; x > -1; x--)
                {
                    DoorsOnMap.DoorInfo d = load.mapDoorInfo[x];
                    itemContainer.CreateDoor(d.doorType, d.posX, d.posY);
                    load.mapDoorInfo.RemoveAt(x);
                }
            }
            

        }
        catch { }
        yield return null;       
    }*/

    public void SaveMapData(bool d)
    {
        //true = default; false = other
        fileName = "";
        MapData mapData = new MapData
        {
            mapDoorInfo = new List<MapData.Door>(),
            mapEnemyInfo = new List<MapData.Enemy>(),
            mapItemInfo = new List<MapData.Item>(),
            mapBinaryObjectInfo = new List<MapData.BinaryObject>()
            
        };

        //Save Doors
        try
        {
            foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
            {
                DoorBase currentItem = door.GetComponent<DoorBase>();

                MapData.Door newInfo = new MapData.Door();
                {
                    //newInfo.doorType = currentItem.doorType.ToString();
                    newInfo.doorName = currentItem.doorName;
                    newInfo.posX = (int)currentItem.transform.position.x;
                    newInfo.posY = (int)currentItem.transform.position.y;
                }

                mapData.mapDoorInfo.Add(newInfo);
                Destroy(door);
            }
        }
        catch
        {
            Debug.Log("problem saving doors");
        }
        //Save Items
         try
            {             
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
                {
                    ItemBase currentItem = item.GetComponent<ItemBase>();

                    MapData.Item newInfo = new MapData.Item();
                    {
                        newInfo.itemName = currentItem.itemName.ToString();
                        newInfo.value = currentItem.value;
                        newInfo.posX = (int)currentItem.transform.position.x;
                        newInfo.posY = (int)currentItem.transform.position.y;
                        //newInfo.active = currentItem.active;
                    }
                    mapData.mapItemInfo.Add(newInfo);
                    Destroy(item);
                }
            }
        catch
        {
            Debug.Log("problem saving items");
        }

        //Save Enemies
        try
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyBase currentEnemy = enemy.GetComponent<EnemyBase>();

                MapData.Enemy newInfo = new MapData.Enemy();
                {
                    newInfo.enemyName = currentEnemy.enemyName.ToString();
                    newInfo.posX = (int)currentEnemy.transform.position.x;
                    newInfo.posY = (int)currentEnemy.transform.position.y;
                    newInfo.alive = currentEnemy.alive;
                    newInfo.heldItem = currentEnemy.heldItem;
                }
                mapData.mapEnemyInfo.Add(newInfo);
                Destroy(enemy);
            }
        }
        catch
        {
            Debug.Log("problem saving enemies");
        }

        //Save BinaryItems
        try
        {
            foreach(GameObject binary in GameObject.FindGameObjectsWithTag("Button"))
            {
                BinaryObject currentObject = binary.GetComponent<BinaryObject>();

                MapData.BinaryObject newObject = new MapData.BinaryObject();
                {
                    newObject.objectName = currentObject.objectName.ToString();
                    newObject.buttonState = currentObject.buttonState;
                    newObject.objectState = currentObject.objectState;
                    newObject.buttonX = (int)currentObject.transform.position.x;
                    newObject.buttonY = (int)currentObject.transform.position.y;
                    newObject.objectX = currentObject.objectLocationX;
                    newObject.objectY = currentObject.objectLocationY;
                    newObject.canToggle = currentObject.canToggle;

                }
            }
        }
        catch
        {
            //nothing to save
        }
           
        

        string json = JsonUtility.ToJson(mapData);
      
        if(d == true)
        {
            fileName = Path.Combine(defaults, Globals.currentMap + ".json");
            Debug.Log(d);
        }
        else
        {
            fileName = Path.Combine(saveData, Globals.currentMap + ".json");
            Debug.Log(d);
        }
            
        
            
        
        if (File.Exists(fileName))
        {
        File.Delete(fileName);
        }

        File.WriteAllText(fileName, json);
           
        Debug.Log("save complete" + fileName);
    }

    public IEnumerator LoadMapData()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("loading map data");
        try
        {
            if (File.Exists(Path.Combine(saveData, Globals.currentMap + ".json")))
            {
                fileName = Path.Combine(saveData, Globals.currentMap + ".json");
            }
            else
            {
                fileName = Path.Combine(defaults, Globals.currentMap + ".json");
            }

            Debug.Log("loading from " + fileName);
            loadFile = File.ReadAllText(fileName);

            MapData load = JsonUtility.FromJson<MapData>(loadFile);
            Container itemContainer = GameObject.FindWithTag("Container").GetComponent<Container>();

            try
            {
                for (int x = load.mapDoorInfo.Count - 1; x > -1; x--)
                {
                    MapData.Door d = load.mapDoorInfo[x];
                    itemContainer.CreateDoor(d.doorName, d.posX, d.posY);
                    load.mapDoorInfo.RemoveAt(x);
                }
            }
            catch
            {
                Debug.Log("problem loading doors");
            }

            try
            {
                for (int x = load.mapItemInfo.Count - 1; x > -1; x--)
                {
                    MapData.Item i = load.mapItemInfo[x];
                    itemContainer.CreateItem(i.itemName, i.value, i.posX, i.posY);
                    load.mapItemInfo.RemoveAt(x);
                }
            }
            catch
            {

            }

            try
            {
                for (int x = load.mapEnemyInfo.Count - 1; x > -1; x--)
                {
                    MapData.Enemy e = load.mapEnemyInfo[x];
                    itemContainer.CreateEnemy(e.enemyName, e.posX, e.posY,e.alive,e.heldItem);
                    load.mapEnemyInfo.RemoveAt(x);
                }
            }
            catch
            {

            }

            try
            {
                for (int x = load.mapBinaryObjectInfo.Count - 1; x > -1; x--)
                {
                    MapData.BinaryObject o = load.mapBinaryObjectInfo[x];
                    itemContainer.CreateBinaryObject(o.objectName, o.buttonState, o.objectState, o.buttonX, o.buttonY, o.objectX, o.objectY, o.canToggle);
                    load.mapBinaryObjectInfo.RemoveAt(x);
                }
            }
            catch
            {

            }


        }
        catch { }
        yield return null;
    }
}
