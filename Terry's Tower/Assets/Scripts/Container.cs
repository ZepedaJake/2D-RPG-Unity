using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    //one of these is needed on each scene. it contains song#, transition trigger, and load scripts.
    public int songNumber;

    void Start()
    {


        //Globals.serializer.LoadEnemies();
        //Globals.serializer.LoadItems();
        //Globals.serializer.LoadDoors();
        StartCoroutine(Globals.serializer.LoadMapData());
        Globals.serializer.SavePlayerData();
        Globals.transition.endTrans = true;
    }

    private void Awake()
    {
        StartCoroutine(SetSong());
    }

    IEnumerator SetSong()
    {
        yield return new WaitForSeconds(.1f);
        Globals.soundHandler.SetMusic(songNumber);
        yield return null;
    }
    public void CreateEnemy(string e, int x, int y, bool a, string i)
    {               
            GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Sprites/Enemies/" + e), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            
               // newEnemy = Instantiate(enemies[(int)enemy], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            
            
            //if(i != null)
            //{
            //    newEnemy.GetComponent<EnemyBase>().heldItem = i;
            //}
            
            newEnemy.GetComponent<EnemyBase>().alive = a;

            newEnemy.GetComponent<SpriteRenderer>().enabled = a;
            //Debug.Log(a);
        //}
       // foreach (Collider2D c in hitColliders)
       // {
           // Debug.Log(c);
        //}
    }

    public void CreateItem(string i, int v, int x, int y)
    {
        try
        {
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Items/" + i), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
        }
        catch
        {
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Items/Tower" + i), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
        }
       
        //GameObject newItem = Instantiate(items[(int)item], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));

        /*newItem.GetComponent<ItemBase>().active = a;
        newItem.GetComponentInChildren<SpriteRenderer>().enabled = a;*/
    }

    public void CreateDoor(string n, int x, int y)
    {                         
        GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/" + n), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));

        
       
        //GameObject newItem = Instantiate(doors[(int)item], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));                                           
    }
    /*public void CreateDoor(string i, int x, int y)
    {       
        try
        {
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/Tower" + i + "Front"), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));

        }
        catch
        {
            if (i == "SilverDoorSide")
            {
                GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/TowerSilverDoorSide"), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (i == "BronzeDoorSide")
            {
                GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/TowerBronzeDoorSide"), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (i == "GoldDoorSide")
            {
                GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/TowerGoldDoorSide"), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/" + i), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            }
        }

        //GameObject newItem = Instantiate(doors[(int)item], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));                                           
    }*/
}
