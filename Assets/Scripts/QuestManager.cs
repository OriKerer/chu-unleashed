using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId;
    public int questActionIndex;
    private GameObject[] brokenCable;
    private GameObject[] fixedCable;
    Dictionary<int, QuestData> questList;

     void Awake()
    {
        brokenCable = GameObject.FindGameObjectsWithTag("BrokenCable");
        fixedCable = GameObject.FindGameObjectsWithTag("FixedCable");
        foreach (var o in fixedCable)
        {
            o.SetActive(false);
        }

        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("Talk With Racheli", new int[] {1000}));//questId, questData
        questList.Add(20, new QuestData("Talk With Harush", new int[] { 2000 }));
        questList.Add(30, new QuestData("Talk to Racheli", new int[] { 1000 }));
        questList.Add(40, new QuestData("Check Barak server", new int[] { 500 }));
        questList.Add(50, new QuestData("Talk to Racheli", new int[] { 1000 }));
        questList.Add(60, new QuestData("Try To find the problem", new int[] { 3000 }));
        questList.Add(70, new QuestData("Talk to Racheli", new int[] { 1000 }));
        questList.Add(80, new QuestData("Go To VR station", new int[] { 4000 }));
    }

    public int GetQuestTalkIndex(int id)
    {

        return questId + questActionIndex;
    }

    public string checkQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //control quest object
        //controlObject();
        if (questActionIndex == questList[questId].npcId.Length)
            nextQuest();

        return questList[questId].questName;
    }
    public string checkQuest()
    {
        return questList[questId].questName;
    }
    void nextQuest()
    {
        if(questId == 60)
        {
            foreach(var o in brokenCable)
            {
                o.SetActive(false);
            }
            foreach (var o in fixedCable)
            {
                o.SetActive(true);
            }
        }

        questId += 10;
        questActionIndex = 0;
    }
}
