﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class CharacterControllerScript : MonoBehaviour {

    public string direction = "LookUp";
    public Vector2 looking = Vector2.up;
    float speed = 3;
    
    public bool canMove = false;
    public bool battle = false;   
    
    public SkillBase[] skills;
    public List<InventoryItemBase> inventory = new List<InventoryItemBase>();
    public List<Quest> quests = new List<Quest>();

    public int level = 1;
    public int statPoints = 5;
    public int skillPoints = 1;
    public int spentStatPoints = 0;
    public int spentSkillPoints = 0;

    public int maxHealth = 100;
    public int currentHealth = 100;   

    public int currentXP = 0;
    public int xpToLevel = 10;

    public int atk;
    public int atkItemBonus;
    public int statAtk;

    public int def;
    public int defItemBonus;
    public int statDef;

    public int bronzeKeys = 0;
    public int silverKeys = 0;
    public int goldKeys = 0;

    public int redOrbState = 0; //0 = not placed, 1 = 1st pedestal, 2 = end pedestal  
    public int blueOrbState = 0;
   

    public float storyProgress;



    // Use this for initialization
    void Start () {
        skills = new SkillBase[5];
        
        Globals.Player = gameObject;
        //Globals.musicSource = gameObject.GetComponent<AudioSource>();
        //Globals.musicVolume = (int)(gameObject.GetComponent<AudioSource>().volume  * 10);
        
        skills[0] = new SkillBase(1,1, "Basic Attack", 0,.12);
        skills[1] = new SkillBase(1,4, "Powerful Strike", 2.00, .10);
        skills[2] = new SkillBase(1,1, "Defend", 1.50, .15);
        skills[3] = new SkillBase(1,4, "Heal", 1.5, .20);
        skills[4] = new SkillBase(1,1, "Escape", 0, .0);

        //SetMusic();
        UpdateStats();
    }
	
	// Update is called once per frame
	void Update () {
        if(canMove && !Globals.paused)
        {
            Move();
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Animator>().Play(direction);
        }

        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, looking, .5f);
        
        if (hit.collider != null)
        {

            if (Input.GetKey(KeyCode.Space) && hit.collider.gameObject.tag == "Door")
            {
                Debug.Log(hit.collider.gameObject.name);
                switch (hit.collider.gameObject.GetComponent<DoorBase>().doorType)
                {
                    case DoorType.BronzeDoor:
                        if (bronzeKeys > 0)
                        {
                            bronzeKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;
                    /*case DoorType.BronzeDoorSide:
                        if (bronzeKeys > 0)
                        {
                            bronzeKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;*/
                    case DoorType.GoldDoor:
                        if (goldKeys > 0)
                        {
                            goldKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;
                    /*case DoorType.GoldDoorSide:
                        if (goldKeys > 0)
                        {
                            goldKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;*/
                    case DoorType.SilverDoor:
                        if (silverKeys > 0)
                        {
                            silverKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;
                   /* case DoorType.SilverDoorSide:
                        if (silverKeys > 0)
                        {
                            silverKeys--;
                            Destroy(hit.collider.gameObject);
                            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[3]);
                        }
                        break;*/
                    default:
                        break;
                }
                Globals.theLevelMaster.UpdateUI();
            }
            else if (Input.GetKey(KeyCode.Space) && hit.collider.gameObject.tag == "Pedestal" && canMove && Globals.theLevelMaster.boolBox.activeSelf == false)
            {                
                    Globals.theLevelMaster.boolBox.SetActive(true);
                if(!hit.collider.gameObject.GetComponent<Pedestal>().placed)
                {
                    Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().question.text = "Place Orb?";
                    Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().function = "PlaceOrb";
                }
                else
                {
                    Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().question.text = "Take Orb?";
                    Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().function = "TakeOrb";
                }
                    
                    canMove = false;
                    Globals.theLevelMaster.boolBox.GetComponent<BoolBox>().objectInQuestion = hit.collider.gameObject;                                 
            }
            else if(Input.GetKey(KeyCode.Space) && hit.collider.gameObject.tag == "Fountain" && canMove && storyProgress >= 5)
            {
                //show fountain menu to fast travel.
               
                Globals.theLevelMaster.fountainMenu.SetActive(true);
                canMove = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && hit.collider.gameObject.tag == "NPC" && canMove && hit.collider.gameObject.GetComponent<NPC>().talking == false)
            {
                hit.collider.gameObject.GetComponent<NPC>().CheckProgress();
                StartCoroutine(Globals.theLevelMaster.NPCDialouge(hit.collider.gameObject));
                canMove = false;
                Debug.Log("talk to npc");
            }            
        }
        //Debug.Log(hit.collider.gameObject.tag);
        //Debug.DrawRay(gameObject.transform.position, looking);
        

    }
     
    
    public void Move()
    {       
        if(Input.GetKey(KeyCode.A) && canMove)
        {
            //gameObject.transform.Translate(Vector2.left * speed);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            direction = "LookLeft";
            looking = Vector2.left;
            gameObject.GetComponent<Animator>().Play("WalkLeft");
        }
        else if(Input.GetKey(KeyCode.S) && canMove)
        {
            //gameObject.transform.Translate(Vector2.down * speed);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            gameObject.GetComponent<Animator>().Play("WalkDown");
            direction = "LookDown";
            looking = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D) && canMove)
        {
            //gameObject.transform.Translate(Vector2.right * speed);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                gameObject.GetComponent<Animator>().Play("WalkRight");
            direction = "LookRight";
            looking = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.W) && canMove)
        {
            //gameObject.transform.Translate(Vector2.up * speed);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            gameObject.GetComponent<Animator>().Play("WalkUp");
            direction = "LookUp";
            looking = Vector2.up;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Animator>().Play(direction);            
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Enemy" && c.gameObject.GetComponent<EnemyBase>().alive)
        {
            Globals.theLevelMaster.StartBattle(c.gameObject);
            try
            {
                Globals.soundHandler.PlayBossSong(c.gameObject.GetComponent<BossBase>().songNumber);
            }
            catch
            {

            }
        }  
        else if(c.tag == "DialougeTrigger")
        {
            if (storyProgress == c.GetComponent<DialougeTrigger>().storyPoint)
            {
                Globals.theLevelMaster.StartDialouge(c.GetComponent<DialougeTrigger>().dialougeSet, c.GetComponent<DialougeTrigger>().dialougeSet);
                //might not need this. check later.
                Destroy(c.gameObject);
                //ensures that the dialouge triggers wont be hit more than once if they shouldnt be
                //probably want both == and > later
                storyProgress = c.GetComponent<DialougeTrigger>().setStoryTo;
            }
            else if( c.GetComponent<DialougeTrigger>().alternateDialougeSet != null)
            {
                Globals.theLevelMaster.StartDialouge(c.GetComponent<DialougeTrigger>().alternateDialougeSet, c.GetComponent<DialougeTrigger>().alternateDialougeSet);
            }
            
        }  
        //else if(c.tag == "Boss")
        //{
            
            
        //    Debug.Log("boss ready");
        //    Globals.theLevelMaster.StartBattle(c.gameObject);
        //}
        else if(c.tag == "Item")
        {
            switch(c.gameObject.GetComponent<ItemBase>().itemType)
            {
                case Item.BronzeKey:
                    bronzeKeys++;
                    break;
                case Item.SilverKey:
                    silverKeys++;
                    break;
                case Item.GoldKey:
                    goldKeys++;
                    break;
                case Item.Sword:
                    atkItemBonus += c.GetComponent<ItemBase>().value;                    
                    break;
                case Item.Shield:
                    defItemBonus += c.GetComponent<ItemBase>().value;
                    break;
                default:
                    break;
                    
            }
            //c.GetComponent<ItemBase>().active = false;
            //c.gameObject.SetActive(false);
            Destroy(c.gameObject);
            UpdateStats();
            Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[0]);
            Globals.theLevelMaster.UpdateUI();
        }
        else if(c.tag == "Portal")
        {
            if(c.gameObject.name == "GreenPortal")
            {
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[4]);
            }
            else if(c.gameObject.name == "RedPortal")
            {
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[5]);
            }
            StartCoroutine(ChangeMap(c.gameObject));
            
        }
        else if(c.tag == "Button")
        {
            c.GetComponentInParent<BinaryObject>().SendMessage("Toggle");
        }
        else
        {
           
        }

    }

    IEnumerator ChangeMap(GameObject c)
    {
        

        //Globals.serializer.SaveMap(c.GetComponent<Portal>().returnPointX, c.GetComponent<Portal>().returnPointY);
        Globals.serializer.SavePlayerData();
        //Globals.serializer.SaveEnemies();
        //Globals.serializer.SaveItems();
        //Globals.serializer.SaveDoors();
        Globals.serializer.SaveMapData(false);
        string nextScene = c.GetComponent<Portal>().goToScene;

        Globals.transition.startTrans = true;
        yield return new WaitForSeconds(1.5f);

        try
        {
            //Globals.serializer.LoadMap(nextScene);
            gameObject.transform.position = (c.GetComponent<Portal>().goToPoint);
        }
        catch
        {
            
            Debug.Log(nextScene + " position missing");
            gameObject.transform.position = new Vector2(0,0);
        }

        SceneManager.LoadScene(nextScene);
        //SceneManager.UnloadSceneAsync(Globals.currentMap);
        Globals.currentMap = nextScene;
        yield return null;
    }

    public void UpdateSkills()
    {
        skills[0].calculatedStrength = (int)(skills[0].baseStrength + atk);
        skills[1].calculatedStrength = (int)(skills[1].baseStrength * atk);
        skills[2].calculatedStrength = (int)(skills[2].baseStrength * def);      
        skills[3].calculatedStrength = (int)(skills[3].baseStrength * (int)(def/2));
         
        for(int x = 0; x<5;x++)
        {
            double skillTempMult = ((skills[x].level-1) * skills[x].multiplier);
            if(skillTempMult  > 0)
            {
                skills[x].calculatedStrength =(int)((skills[x].calculatedStrength) * (1 + skillTempMult));
            }           
        }
    }

    public void UpdateStats()
    {
        atk = statAtk + atkItemBonus;
        def = statDef + defItemBonus;
        maxHealth = ((level - 1) * 25) + 100;
        
    }

}
