using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId;
    public int questActionIndex;//quest 순방향 진행을 위함
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;

     void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("재영이 험담하기", new int[] {1000, 2000 }));//questId, questData
        questList.Add(20, new QuestData("재영이 처리하기", new int[] { 5000, 2000 }));//questId, questData
        questList.Add(30, new QuestData("퀘스트 올 클리어", new int[] { 0 }));//questId, questData
    }

    public int GetQuestTalkIndex(int id)
    {

        return questId + questActionIndex;
    }

    public string checkQuest(int id)
    {
        

        if(id == questList[questId].npcId[questActionIndex])//quest 순서대로 하기위한 
            questActionIndex++;

        //control quest object
        controlObject();
        if (questActionIndex == questList[questId].npcId.Length)//퀘스트 순서가 끝에 도달했을 때 퀘스트 번호 증가
            nextQuest();

        return questList[questId].questName;
    }
    public string checkQuest()
    {
        return questList[questId].questName;
    }
    void nextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void controlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                
                break;
        }
    }
}
