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
        //Globals.serializer.SavePlayerData();
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
            
            newEnemy.GetComponent<EnemyBase>().alive = a;

            newEnemy.GetComponent<SpriteRenderer>().enabled = a;
       
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

    }

    public void CreateDoor(string n, int x, int y)
    {                         
        GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/" + n), new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
                                              
    }

    public void CreateBinaryObject(string n, bool bs, bool os, int bx, int by, int ox, int oy, bool t)
    {
        GameObject newItem = Instantiate(Resources.Load<GameObject>("Sprites/Doors/" + n), new Vector3(bx, by, 0), new Quaternion(0, 0, 0, 0));
        BinaryObject newObject = newItem.GetComponent<BinaryObject>();

        newObject.buttonState = bs;
        newObject.objectState = os;
        newObject.objectLocationX = ox;
        newObject.objectLocationY = oy;
        newObject.canToggle = t;

    }
}
