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
        talkData.Add(1000, new string[] { "Where is Saar?"});//npc A :number -> index
        talkData.Add(2000, new string[] { "I wonder how many camels I am worth?..."});//npc A :number -> index
        talkData.Add(3000, new string[] { "That Does not look good"});
        talkData.Add(5000, new string[] { "I need to finish my work before I get on the VR"});
        talkData.Add(400, new string[] { "Barak Trainer" });//box
        talkData.Add(800, new string[] { "You found the secret Pretzel!!!", "Sadly,", " You don't have a mouth to eat it." });//box
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

        talkData.Add(30 + 1000, new string[] { "Of course, he blame us.", "Try to check the server"});
        talkData.Add(50 + 1000, new string[] { "Of course the server was fine.", "Try looking around.", "See if something look wrong"});
        talkData.Add(70 + 1000, new string[] { "The cable was unpluged?!", "Lucky you fugure that out", "We finnished here", "You can work on the VR", " It's on the table"});
        talkData.Add(80 + 1000, new string[] { "Good Job!" });

        talkData.Add(60 + 3000, new string[] { "That must be the the problem!",
            "Better plug this in."});
        talkData.Add(80 + 5000, new string[] { "Yes finally!", "Lets put on the VR headset" });

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
