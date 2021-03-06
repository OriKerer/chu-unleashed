using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId;
    public int questActionIndex;
    private GameObject[] brokenCable;
    private GameObject[] fixedCable;
    Dictionary<int, QuestData> questList;
    NextScene nextScene;
    private bool nextSceneTrigger = false;
    private float nextSceneTime = 0;
    private void Start()
    {
        nextScene = GameObject.FindObjectOfType<NextScene>();
    }

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
        questList.Add(80, new QuestData("Go To VR station", new int[] { 5000 }));
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
        } else if (questId == 80)
        {
            nextSceneTrigger = true;
            nextScene.nextScene();
            nextSceneTime = Time.time + nextScene.Delay;
        }

        questId += 10;
        questActionIndex = 0;
    }

    private void Update()
    {
        if(nextSceneTrigger && nextSceneTime <= Time.time)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}

