using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class LevelMaster : MonoBehaviour {
    public Text bronzeKeysText;
    public Text silverKeysText;
    public Text goldKeysText;
    public Text playerXpText;
    public Text playerHpText;
    public Text atkText;
    public Text defText;
    //public CharacterControllerScript player;

    public Slider xpBar;
    public Slider hpBar;

    public GameObject battleScreen;
    public GameObject enemyImage;
    public GameObject enemyEffect;
    public GameObject enemyEffectDef;
    public GameObject playerEffect;
    public GameObject playerEffectDef;

    public Sprite hit;
    public Sprite powerHit;
    public Sprite heal;
    public Sprite defend;
    public Sprite blank;

    public Text enemyDamage;
    public Text playerDamage;
    public EnemyBase enemy;
    public int battleMenuSelection = 0;
    public GameObject[] skillImages;
    public Text enemyHealthText;
    public Text enemyAtkText;
    public Text enemyDefText;
    public Text playerBattleHealthText;
    public Text playerBattleAtkText;
    public Text playerBattleDefText;

    float enemyTempDef;
    float enemyTempAtk;
    int playerTempDef;
    int battlePhase = 0;
    bool enemyDefending = false;

    float time = 0;

    
    //stuff for pause
    public GameObject pauseMenu;
    public GameObject debugMenu;
    public int menuOpen = 0; //0 = none used for pause and main menu: 1 = stats  2 = skills  3 = inventory  4 = options

    public GameObject deathMenu;
    public GameObject ui;

    //BoolBox
    public GameObject boolBox;

    //notifications
    public GameObject notifyingText;
    public List<string> notices = new List<string>();

    //fountain travel menu
    public GameObject fountainMenu;
    
    //sounds / music
    //public AudioClip[] soundEffects;
    //public AudioClip[] music;
    //public AudioSource audioSrc;

    // Use this for initialization
    void Start () {
        //audioSrc = gameObject.GetComponent<AudioSource>();
        Globals.MainCamera = GameObject.FindWithTag("MainCamera");
        Globals.playerScript = Globals.Player.GetComponent<CharacterControllerScript>();
        Globals.theLevelMaster = gameObject.GetComponent<LevelMaster>();
        Globals.serializer = gameObject.GetComponent<Serializer>();
        //Globals.soundEffectSource = audioSrc;
        //Globals.soundEffectVolume = (int)(audioSrc.volume * 10);
        UpdateUI();       
	}
	
	// Update is called once per frame
	void Update () {
        Globals.MainCamera.transform.position = new Vector3(Globals.Player.transform.position.x, Globals.Player.transform.position.y, Globals.MainCamera.transform.position.z);

        if (Globals.playerScript.battle && battlePhase == 0)
        {
            StartCoroutine(BattleMenu());
        }
        
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && Globals.paused == false && Globals.playerScript.battle == false)
        {
            Globals.paused = true;

            pauseMenu.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {            
            Globals.debug = !Globals.debug;
            debugMenu.SetActive(Globals.debug);
            
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            //Globals.serializer.SaveEnemiesDefault();
            //Globals.serializer.SaveItemsDefault();
            //Globals.serializer.SaveDoorsDefault();
            Globals.serializer.SaveMapData(true);
            
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //StartCoroutine(Globals.serializer.LoadMapData());

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //instantiate babyslime at 0,0
            //GameObject testLoadEnemy = Resources.Load<GameObject>("Sprites/Enemies/BabySlime");
            //Instantiate(testLoadEnemy, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            //Globals.playerScript.quests.Add(new Quest("n", "d", false, 1));
            Debug.Log(Globals.playerScript.quests[0].questName);
            Debug.Log(Globals.playerScript.quests[0].description);
            Debug.Log(Globals.playerScript.quests[0].complete);
            Debug.Log(Globals.playerScript.quests[0].progress);
           
        }              
    }
    
    //used for little popups to show experience gained, items gained and level up
    IEnumerator FeedNotices()
    {

        foreach(string s in notices)
        {
            Notice(s);
            yield return new WaitForSeconds(1);
        }
        notices.Clear();
        yield return null;
    }
    public void Notice(string s)
    {
        GameObject notice = Instantiate(notifyingText, GameObject.Find("Canvas").transform);
        notice.GetComponent<Text>().text = s;
    }
    public void StartDialouge(string s, string set)
    {
        StartCoroutine(Dialouge(s, set));
    }
    public IEnumerator Dialouge(string s, string set)
    {
        
        //s is what set of messages to pull from ex. messages for a specific character or the intro messages (dialouge)
        //set is for particular sets of dialouge from one character. if you come back to them later they use diff messages
        string path = "Assets/Resources/Dialouge/" + s;
        int counter = 1;

        //for each txt file in s dialouge's directory
        
        foreach (string f in System.IO.Directory.GetFiles(path + "/","*.txt"))
        {
            //load a message box
            GameObject message = Instantiate(Resources.Load("MessageBox"), GameObject.Find("Canvas").transform) as GameObject;
            message.GetComponentInChildren<Text>().text = "";
    

           
                //read each line into the text box
                //resoucres/assets/test/test1.txt
                string[] lines = System.IO.File.ReadAllLines(path + "/" + set + counter + ".txt");
                foreach (string line in lines)
                {// Display the file contents by using a foreach loop
                 //read out lines
                 //change this later
                 
                message.GetComponentInChildren<Text>().text += line + "\n";
                                

            }

            do
            {
                //nothing
                Globals.playerScript.canMove = false;
                yield return null;
            }
            while (!Input.GetKeyDown(KeyCode.Space));
                //wait for input ^^

                //up counter and read next file.
                counter++;
            Destroy(message);
            //Debug.Log(counter);
        }
        //Debug.Log("Close Text");
        Globals.playerScript.canMove = true;
        yield return null;
    }

    public IEnumerator NPCDialouge(GameObject target)
    {
        NPC npc = target.GetComponent<NPC>();
        npc.talking = true;
        //base is held in NPC folder
        //in the base folder is seperate options that will be chosen by dialouge set
        //each file should be named "set"1, "set"2 etc

        string path = "Assets/Resources/Dialouge/NPC/" + npc.dialogueBase + "/" + npc.dialogueSet;
        int counter = 1;

        //for each txt file in s dialouge's directory

        foreach (string f in System.IO.Directory.GetFiles(path + "/", "*.txt"))
        {
            //load a message box
            GameObject message = Instantiate(Resources.Load("MessageBox"), GameObject.Find("Canvas").transform) as GameObject;
            message.GetComponentInChildren<Text>().text = "";

            //read each line into the text box
            //use _ to seperate set from the files
            //Assets/Resources/Dialouge/NPC/Robes/Robes1/Robes1_1
            string[] lines = System.IO.File.ReadAllLines(path + "/" + npc.dialogueSet + "_" + counter + ".txt");
            foreach (string line in lines)
            {// Display the file contents by using a foreach loop
             //read out lines
             //change this later

                message.GetComponentInChildren<Text>().text += line + "\n";
            }
            do
            {
                //nothing
                Globals.playerScript.canMove = false;
                yield return null;
            }
            while (!Input.GetKeyDown(KeyCode.Space));
            //wait for input ^^

            //up counter and read next file.
            counter++;
            Destroy(message);
            //Debug.Log(counter);
        }
        //Debug.Log("Close Text");
        Globals.playerScript.canMove = true;
        npc.talking = false;
        yield return null;
    }

    public void UpdateUI()
    {
        bronzeKeysText.text = Globals.playerScript.bronzeKeys.ToString();
        silverKeysText.text = Globals.playerScript.silverKeys.ToString();
        goldKeysText.text = Globals.playerScript.goldKeys.ToString();

        playerXpText.text = Globals.playerScript.currentXP.ToString() + " / " + Globals.playerScript.xpToLevel.ToString();
        playerHpText.text = Globals.playerScript.currentHealth.ToString() + " / " + Globals.playerScript.maxHealth.ToString();

        xpBar.maxValue = Globals.playerScript.xpToLevel;
        xpBar.value = Globals.playerScript.currentXP;
        hpBar.value = Globals.playerScript.currentHealth;
        hpBar.maxValue = Globals.playerScript.maxHealth;

        atkText.text = "Atk: " + Globals.playerScript.atk;
        defText.text = "Def: " + Globals.playerScript.def;

        StartCoroutine(FeedNotices());
            

    }

   
    // EVERYTHING BELOW THIS IS FOR BATTLE************************************************************************************************


    public void StartBattle(GameObject e)
    {
        battleScreen.SetActive(true);
        Globals.playerScript.canMove = false;
        Globals.playerScript.battle = true;

        enemyDamage.text = "";
        playerDamage.text = "";
        enemy = e.GetComponent<EnemyBase>();
        enemyImage.GetComponent<Image>().sprite = e.GetComponent<SpriteRenderer>().sprite;
        if (e.GetComponent<BossBase>()!= null) 
        {
            enemyImage.transform.localScale = new Vector2(2, 2);
            enemyEffectDef.transform.localScale = new Vector2(3f, 3f);
        }
        else
        {
            enemyImage.transform.localScale = new Vector2(1.2f, 1.2f);
            enemyEffectDef.transform.localScale = new Vector2(2f, 2f);
        }
        battleMenuSelection = 0;
        battlePhase = 0;
        Globals.playerScript.skills[battleMenuSelection].selected = true;
        skillImages[battleMenuSelection].transform.localScale = new Vector2(1.8f, 1.8f);
        playerEffectDef.SetActive(false);
        enemyEffectDef.SetActive(false);
        enemy.currentHealCooldown = enemy.healCooldown;
        enemy.currentPowerAtkCooldown = enemy.powerAtkCooldown;
        UpdateBattleMenu();

        //increment all skill cooldowns at start of battle to avoid being locked out of one.
        foreach (SkillBase s in Globals.playerScript.skills)
        {
            if (s.coolDownTemp == 0)
            {
                s.coolDownTemp = 1;
                s.selected = false;
            }
        }
    }

    public void UpdateBattleMenu()
    {
        enemyHealthText.text = "Health: " + enemy.GetComponent<EnemyBase>().currentHealth.ToString();
        enemyAtkText.text = "Atk: " + enemy.GetComponent<EnemyBase>().atk.ToString();
        enemyDefText.text = "Def: " + enemy.GetComponent<EnemyBase>().def.ToString();

        playerBattleHealthText.text = "Health: " + Globals.playerScript.currentHealth.ToString() + " / " + Globals.playerScript.maxHealth.ToString();
        playerBattleAtkText.text = "Atk: " + Globals.playerScript.atk.ToString();
        playerBattleDefText.text = "Def: " + Globals.playerScript.def.ToString();

        Globals.playerScript.UpdateSkills();

        //change color for skills and size       
        for (int x = 0; x < 5; x++)
        {
            if (Globals.playerScript.skills[x].selected)
                skillImages[x].transform.localScale = new Vector2(1.8f, 1.8f);
            else
                skillImages[x].transform.localScale = new Vector2(1, 1);

            //skill not available, grey it out
            if (Globals.playerScript.skills[x].coolDownTemp != Globals.playerScript.skills[x].coolDown)
            {
                skillImages[x].GetComponent<Image>().color = Color.grey;
            }
            else
            {
                skillImages[x].GetComponent<Image>().color = Color.white;
            }
        }
    }

    void CycleDown()
    {
        Globals.playerScript.skills[battleMenuSelection].selected = false;
        battleMenuSelection += 1;
        if (battleMenuSelection > 4)
        {
            battleMenuSelection = 0;
        }
        if (Globals.playerScript.skills[battleMenuSelection].coolDownTemp >= Globals.playerScript.skills[battleMenuSelection].coolDown)
        {
            Globals.playerScript.skills[battleMenuSelection].selected = true;
        }
        else
        {
            CycleDown();
        }

        UpdateBattleMenu();
    }

    void CycleUp()
    {
        Globals.playerScript.skills[battleMenuSelection].selected = false;
        battleMenuSelection -= 1;
        if (battleMenuSelection < 0)
        {
            battleMenuSelection = 4;
        }
        if (Globals.playerScript.skills[battleMenuSelection].coolDownTemp >= Globals.playerScript.skills[battleMenuSelection].coolDown)
        {
            Globals.playerScript.skills[battleMenuSelection].selected = true;
        }
        else
        {
            CycleUp();
        }

        UpdateBattleMenu();
    }

    IEnumerator BattleMenu()
    {
        
        //cycle through menu
        //Debug.Log(Globals.playerScript.skills[battleMenuSelection].skillName);
        if (Input.GetKeyDown(KeyCode.S))
        {
            CycleDown();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            CycleUp();
        }


        //select action
        if (Input.GetKeyDown(KeyCode.Space) && battlePhase == 0)
        {
            //select enemy action just before player action

            //ex power 15, def 15, heal 20
            // power is 0-15, def is 16-30, heal is 31-50, normal is anything else
            int x = Random.Range(0, 100);
            if(Globals.debug)
            {
                Globals.theDebugMenu.enemyMoveParameters.text = "0-" + enemy.powerAtkChance + "\t" + (enemy.powerAtkChance + 1) + "-" + (enemy.powerAtkChance + enemy.defChance) + "\t" + (enemy.powerAtkChance + enemy.defChance + 1) + "-" + (enemy.powerAtkChance + enemy.defChance + enemy.healChance) + "\t" + (enemy.powerAtkChance + enemy.defChance + enemy.healChance + 1) + "-100";
            }         
            //power
            if (x <=  enemy.powerAtkChance && enemy.currentPowerAtkCooldown <= 0)
            {
                enemyTempAtk = enemy.atk * enemy.powerAtkMult;
                enemyTempDef = enemy.def;
                enemy.currentPowerAtkCooldown = enemy.powerAtkCooldown;
                //Debug.Log("power attack, " + enemyTempAtk);

                if(Globals.debug)
                Globals.theDebugMenu.enemyNextMove.text = x + " Power Attack: " + enemyTempAtk;
            }
            //defend
            else if (x > enemy.powerAtkChance && x <= (enemy.powerAtkChance + enemy.defChance))
            {
                enemyTempDef = enemy.def * enemy.defMult;
                enemyEffectDef.SetActive(true);
                enemyDefending = true;
                //Debug.Log("Defend, " + enemyTempDef);

                if(Globals.debug)
                Globals.theDebugMenu.enemyNextMove.text = x + " Defend: " + enemyTempDef;
            }
            else if (x > (enemy.powerAtkChance + enemy.defChance) && x <= (enemy.powerAtkChance + enemy.defChance + enemy.healChance) && enemy.currentHealCooldown <= 0)
            {
                enemyTempAtk = -10;
                enemyTempDef = enemy.def;
                enemy.currentHealCooldown = enemy.healCooldown;
                //Debug.Log("heal, " + (int)(enemy.health * enemy.healPerc));

                if(Globals.debug)
                Globals.theDebugMenu.enemyNextMove.text = x + " Heal: " + (enemy.health * enemy.healPerc);
            }
            else //regular atk
            {
                //these only used for when power attack or defend was chosen
                enemyTempDef = enemy.def;
                enemyTempAtk = -1;
                //Debug.Log("normal attack, " + enemy.atk);
                if(Globals.debug)
                Globals.theDebugMenu.enemyNextMove.text = x + " Normal Attack: " + enemy.atk;
            }


            battlePhase = 1;


            //set TempDef real quick for later. it was getting set to 0 =_=
            playerTempDef = Globals.playerScript.def;
            //which action did player choose?
            switch (battleMenuSelection)
            {
                case 0:
                    //basic attack
                    if(enemyTempDef > Globals.playerScript.skills[battleMenuSelection].calculatedStrength)
                    {
                        enemyDamage.text = "-0";
                    }
                    else
                    {
                        enemy.currentHealth -= Globals.playerScript.skills[battleMenuSelection].calculatedStrength - (int)enemyTempDef;
                        enemyDamage.text = "-" + (Globals.playerScript.skills[battleMenuSelection].calculatedStrength - (int)enemyTempDef).ToString();
                    }
                    
                    enemyEffect.GetComponent<Image>().sprite = hit;
                    Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);                   
                    break;
                case 1:
                    //power attack
                    if (enemyTempDef > Globals.playerScript.skills[battleMenuSelection].calculatedStrength)
                    {
                        enemyDamage.text = "-0";
                    }
                    else
                    {
                        enemy.currentHealth -= Globals.playerScript.skills[battleMenuSelection].calculatedStrength - (int)enemyTempDef;
                        enemyDamage.text = "-" + (Globals.playerScript.skills[battleMenuSelection].calculatedStrength - (int)enemyTempDef).ToString();
                    }             
                    enemyEffect.GetComponent<Image>().sprite = powerHit;
                    Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);
                    break;
                case 2:
                    //defend
                    
                    playerTempDef = Globals.playerScript.def;
                    Globals.playerScript.def = Globals.playerScript.skills[battleMenuSelection].calculatedStrength;
                    playerEffectDef.SetActive(true);
                    break;
                case 3:
                    //heal            
                    if(Globals.playerScript.maxHealth - Globals.playerScript.currentHealth < Globals.playerScript.skills[battleMenuSelection].calculatedStrength)
                    {
                        playerDamage.text = "+" + (Globals.playerScript.maxHealth - Globals.playerScript.currentHealth).ToString();
                        Globals.playerScript.currentHealth = Globals.playerScript.maxHealth;                
                    }
                    else
                    {
                        Globals.playerScript.currentHealth += Globals.playerScript.skills[battleMenuSelection].calculatedStrength;
                        playerDamage.text = "+" + Globals.playerScript.skills[battleMenuSelection].calculatedStrength.ToString();
                    }
                                           
                    playerEffect.GetComponent<Image>().sprite = heal;
                    
                    playerDamage.color = Color.green;
                    Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[2]);
                    break;
                case 4:
                    
                    //run
                    switch (Globals.playerScript.direction)
                    {
                        case "LookUp":
                            Globals.playerScript.transform.position += (Vector3.down * .5f);
                            Globals.playerScript.direction = "LookDown";
                            break;
                        case "LookLeft":
                            Globals.playerScript.transform.position += (Vector3.right * .5f);
                            Globals.playerScript.direction = "LookRight";
                            break;
                        case "LookDown":
                            Globals.playerScript.transform.position += (Vector3.up * .5f);
                            Globals.playerScript.direction = "LookUp";
                            break;
                        case "LookRight":
                            Globals.playerScript.transform.position += (Vector3.left * .5f);
                            Globals.playerScript.direction = "LookLeft";
                            break;
                        default:
                            break;
                    }


                    Debug.Log(Globals.playerScript.skills[4].coolDownTemp);
                    BattleEnd(false, 0);
                    break;
                default:
                    break;
            }
            Globals.playerScript.skills[battleMenuSelection].coolDownTemp = 0;
            UpdateBattleMenu();

            //wait then clear damage and effect
            yield return new WaitForSeconds(1);
            enemyDamage.text = "";
            playerDamage.text = "";
            playerDamage.color = Color.red;            
            playerEffect.GetComponent<Image>().sprite = blank;
            enemyEffect.GetComponent<Image>().sprite = blank;

        }

        //now enemy attcks or heals on phase 2
        if (battlePhase == 1 && enemy.currentHealth > 0)
        {
            //power atk
            if (!enemyDefending && enemyTempAtk > 0)
            {
                if (Globals.playerScript.def < (int)enemyTempAtk)
                {
                    Globals.playerScript.currentHealth -= (int)enemyTempAtk - Globals.playerScript.def;
                    playerEffect.GetComponent<Image>().sprite = powerHit;
                    playerDamage.text = "-" + ((int)(enemyTempAtk - Globals.playerScript.def)).ToString();
                }
                else
                {
                    playerDamage.text = "-0";
                }
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);
            }
            //normal atk
            else if (!enemyDefending && enemyTempAtk == -1)
            {
                if(Globals.playerScript.def<enemy.atk)
                {
                    Globals.playerScript.currentHealth -= enemy.atk - Globals.playerScript.def;
                    playerEffect.GetComponent<Image>().sprite = hit;
                    playerDamage.text = "-" + (enemy.atk - Globals.playerScript.def).ToString();
                }
                else
                {
                    playerDamage.text = "-0";
                }
                Globals.soundEffectSource.PlayOneShot(Globals.soundHandler.soundEffect[1]);
            }
            //heal
            else if (!enemyDefending && enemyTempAtk == -10)
            {
                if (enemy.currentHealth < (enemy.health - (int)(enemy.health * enemy.healPerc)))
                    enemy.currentHealth += (int)(enemy.health * enemy.healPerc);
                else
                    enemy.currentHealth = enemy.health;

                enemyEffect.GetComponent<Image>().sprite = heal;
                Debug.Log("Enemy Healed" + (int)(enemy.health * enemy.healPerc));
            }
            else
            {
                //enemyEffect.GetComponent<Image>().sprite = defend;
            }

            battlePhase = 2;
            UpdateBattleMenu();

            yield return new WaitForSeconds(1);
            enemyDamage.text = "";
            playerDamage.text = "";
            enemyDefending = false;
            Globals.playerScript.def = playerTempDef;
            playerEffect.GetComponent<Image>().sprite = blank;
            enemyEffect.GetComponent<Image>().sprite = blank;
            playerEffectDef.SetActive(false);
            enemyEffectDef.SetActive(false);
            battlePhase = 0;

            //increment all skill cooldowns now.
            enemy.currentHealCooldown--;
            enemy.currentPowerAtkCooldown--;
            foreach (SkillBase s in Globals.playerScript.skills)
            {
                if (s.coolDownTemp < s.coolDown)
                {
                    s.coolDownTemp++;
                    s.selected = false;
                }
            }


            //default back to attack            
            battleMenuSelection = 0;
            Globals.playerScript.skills[battleMenuSelection].selected = true;
            UpdateBattleMenu();
            
        }
        else if(enemy.currentHealth <= 0)
        {
            try
            {
                Globals.playerScript.storyProgress = enemy.gameObject.GetComponent<ChangeStoryProgressOnDeath>().setStoryProgress;
            }
            catch
            {

            }
            BattleEnd(true, enemy.xp);         
        }
        
        if(Globals.playerScript.currentHealth <= 0)
        {
            Death();
        }

        yield return null;
    }

    public void BattleEnd(bool win, int xp)
    {
        if(win == true)
        {
            try
            {
                if (enemy.heldItem != "")
                {
                    InventoryItemBase item = new InventoryItemBase(enemy.heldItem);
                    Globals.playerScript.inventory.Add(item);
                    notices.Add(item.inventoryItemName + "Obtained");
                }
                
            }
            catch
            {

            }
            
           notices.Add(enemy.xp.ToString() + "Xp Gained");
            Destroy(enemy.gameObject);
           //enemy.alive = false;          
           //enemy.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }
        battlePhase = 0;
        Globals.playerScript.battle = false;
        Globals.playerScript.canMove = true;
        battleScreen.SetActive(false);
        Globals.playerScript.currentXP += xp;
        while(Globals.playerScript.currentXP > Globals.playerScript.xpToLevel)
        {
            Globals.playerScript.level += 1;
            Globals.playerScript.skillPoints += 1;
            Globals.playerScript.statPoints += 5;
            
            Globals.playerScript.currentXP -= Globals.playerScript.xpToLevel;
            Globals.playerScript.xpToLevel += (int)Mathf.Pow(Globals.playerScript.level+1,2);
            Globals.playerScript.UpdateStats();
            Globals.playerScript.currentHealth = Globals.playerScript.maxHealth;
            notices.Add("Level Up!");
        }
       
        Globals.playerScript.UpdateStats();
        UpdateUI();
    }

    public void Death()
    {
        battlePhase = 0;
        Globals.playerScript.battle = false;
        battleScreen.SetActive(false);
        Globals.playerScript.canMove = false;
        Globals.paused = true;
        deathMenu.SetActive(true);

    }

    }



