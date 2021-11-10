using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string> nameData;
    public Sprite[] portraitArr;
    

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        nameData = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData(){
        talkData.Add(1000, new string[] { "First 1", "second 1" });//npc A :number -> index
        talkData.Add(2000, new string[] { "I wonder how many camels I am worth?..."});//npc A :number -> index
        talkData.Add(3000, new string[] { "That Does not look good"});
        talkData.Add(100, new string[] { "first 3" });//desk
        talkData.Add(200, new string[] { "first 4" });//box
        talkData.Add(400, new string[] { "Barak Trainer" });//box
        talkData.Add(500, new string[] { "Barak Traner servers", "Seems to work fine" });//box
        //quest talk
        talkData.Add(10 + 1000, new string[] { 
            "Agh, the trainer is broken agian.", 
            "Try talking to harush,", "See if elbit can fix it." 
        });

        talkData.Add(20 + 2000, new string[] { 
            "The trainer is broken?", 
            "Must be the software!",
            "It is always the software,",
            "never the hardware."});
        talkData.Add(20 + 1000, new string[] { "Did you talk with Harush?"});

        talkData.Add(30 + 1000, new string[] { "Of course, he blame us."});
        talkData.Add(30 + 3000, new string[] { "That may be the cause of the problem,", "Better plug this in."});

        nameData.Add(3000, "Unpluged Cable");
        nameData.Add(1000, "Racheli");
        nameData.Add(2000, "Harush");
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (!talkData.ContainsKey(id))//
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
                
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public string GetName(int id)
    {
        return nameData[id];
    }
}
