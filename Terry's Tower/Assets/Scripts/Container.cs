using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] items;
    public GameObject[] doors;
    public int songNumber;

    void Start()
    {
        Globals.soundHandler.SetMusic(songNumber);
        //Debug.Log("Load Enemies");
        Globals.serializer.LoadEnemies();
        Globals.serializer.LoadItems();
        Globals.serializer.LoadDoors();
        Globals.serializer.SavePlayerData();
        Globals.transition.endTrans = true;       
    }

    public void CreateEnemy(string e, int x, int y, bool a, string i)
    {
        //Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(x + .5f, y - .5f), .7f, 1 << LayerMask.NameToLayer("Enemy"));

        //if (hitColliders.Length == 0)
        //{
            Enemy enemy = (Enemy)System.Enum.Parse(typeof(Enemy), e);
            Debug.Log((int)enemy);
            GameObject newEnemy = Instantiate(enemies[(int)enemy], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
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

    public void CreateItem(string i, int v, int x, int y, bool a)
    {
        //Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(x + .5f, y - .5f), .7f, 1 << LayerMask.NameToLayer("Item"));

        //if (hitColliders.Length == 0)
        //{
            Item item = (Item)System.Enum.Parse(typeof(Item), i);
            Debug.Log((int)item);
            GameObject newItem = Instantiate(items[(int)item], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            newItem.GetComponent<ItemBase>().active = a;

            newItem.GetComponentInChildren<SpriteRenderer>().enabled = a;
        //    Debug.Log(a);
        //}
        //foreach (Collider2D c in hitColliders)
        //{
        //    Debug.Log(c);
        //}
    }

    public void CreateDoor(string i, int x, int y)
    {                    
                DoorType item = (DoorType)System.Enum.Parse(typeof(DoorType), i);
                GameObject newItem = Instantiate(doors[(int)item], new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));                                           
    }
}
