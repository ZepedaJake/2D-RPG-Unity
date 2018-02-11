using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public string statToBoost;
    public string boostAmount;
    public bool talking = false;
    public float questAvailableAt;
    //these arrays will help determine what the npc will say depending on quest progress and or story progress
    //ex. story is at 1.3, npc will mention killing the boss, 
    // story is at 1.5 (after boss 1) npc will ask for assistance and start quest
    // story is at 2 and quest is at 1, npc will remind you about the task.
    public float[] storyProgress;
    public float[] questProgress;

    //dialogue base will be able to be set manually, but the full set will have to be called depending on story and quest progress
    public string dialogueBase;
    public string dialogueSet;
    public string questName;
    public string questDescription;
	// Use this for initialization
	void Start () {
        //CheckProgress();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckProgress()
    {
        try
        {
            foreach (Quest q in Globals.playerScript.quests)
            {
                Debug.Log(q.questName);
            }
        }
        catch
        {
          
        }
        //if you have their quest use its associated dialogue, else use story
        //this is gonna load up each time the player enters the map the NPC is on
        if (Globals.playerScript.quests.Count > 0)
        {
            for (int y = 0; y < Globals.playerScript.quests.Count; y++)
            {
                
                if (Globals.playerScript.quests[y].questName == questName)
                {
                    //if they have the quest this will trigger
                    if (Globals.playerScript.quests[y].complete == true)
                    {
                        //if the quest is complete this will trigger
                    }
                    else
                    {
                        //quest is not complete
                        for (int x = 0; x < questProgress.Length; x++)
                        {
                            if (Globals.playerScript.quests[y].progress >= questProgress[x])
                            {
                                dialogueSet = dialogueBase + "Quest" + questProgress[x].ToString();
                                break;
                            }
                        }
                    }
                    //else if (Globals.playerScript.storyProgress == 10)
                    //{
                    //    //10 is currently placeholder for beating the final boss
                    //}
                    break;
                }
                else
                {
                    if (Globals.playerScript.storyProgress >= questAvailableAt)
                    {
                        //give quest
                        dialogueSet = dialogueBase + questAvailableAt.ToString();
                    }
                    else
                    {
                        for (int x = storyProgress.Length - 1; x > -1; x--)
                        {
                            if (Globals.playerScript.storyProgress >= storyProgress[x])
                            {
                                dialogueSet = dialogueBase + storyProgress[x].ToString();
                                
                            }

                        }
                    }
                }
            }
            Globals.playerScript.quests.Add(new Quest(questName, questDescription, false, 1));
        }
        else
        {
            //if you do not have the quest, and the final boss has not been beaten, load up basic story progress based dialogue;
            //updates to the most relevent dialogue
            //ex. your story is at 2.3 but NPC triggers are 1, 1.5 and 10.
            // it will call the dialogue for 1.5
            if(Globals.playerScript.storyProgress >= questAvailableAt)
            {
                //give quest
                dialogueSet = dialogueBase + questAvailableAt.ToString();
                Globals.playerScript.quests.Add(new Quest(questName, questDescription,false, 1));
            }
            else
            {
                for (int x = storyProgress.Length - 1; x > -1; x--)
                {
                    if (Globals.playerScript.storyProgress >= storyProgress[x])
                    {
                        dialogueSet = dialogueBase + storyProgress[x].ToString();
                        break;
                    }

                }
            }
            
        }

        Debug.Log(Globals.playerScript.storyProgress + "\n" + dialogueSet);
        
        
    }
}
