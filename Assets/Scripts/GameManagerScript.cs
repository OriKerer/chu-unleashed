using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public QuestManager questManager;
    public Image portraitImg;
    public Text questText;
    public TalkManager talkManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public TypeEffect talk;
    public GameObject scanObject;// 플레이어가 조사했던 오브젝트 변수
    public bool isAction; // 판넬이 띄워져있는가
    public int talkIndex;
    public Sprite prevPortrait;
    public GameObject menuSet;
    public GameObject player;
    public Text nameText;
    private Text objective;
    
     void Start()
    {
        objective = GameObject.Find("ObjectiveText").GetComponent<Text>();
        GameLoad();
        isAction = false;
        talkPanel.SetBool("isShow",isAction);
        //Debug.Log(questManager.checkQuest());
        objective.text = questManager.checkQuest();

        questText.text = questManager.checkQuest();
        
    }

     void Update()
    {
        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }
    // Update is called once per frame
    public void Action(GameObject scanObj)
    {

            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNpc);
            talkPanel.SetBool("isShow",isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            //Debug.Log(questManager.checkQuest(id));
            objective.text = questManager.checkQuest(id);
            questText.text = questManager.checkQuest();
            return;
        }

        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            //portraitImg.sprite = talkManager.GetPortrait(id , int.Parse(talkData.Split(':')[1]));
            //portraitImg.color = new Color(1, 1, 1, 1);
            //if(prevPortrait != portraitImg.sprite)
            //{
            //    portraitAnim.SetTrigger("doEffect");
            //    prevPortrait = portraitImg.sprite;
            //}

            nameText.text = talkManager.GetName(id);
            
        }
        else
        {
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
            nameText.text = "";
        }
        isAction = true;
        talkIndex++;
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
        //player.x
        //player.y
        //quest id
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX"); ;
        float y = PlayerPrefs.GetFloat("PlayerY"); ;
        int questId = PlayerPrefs.GetInt("QuestId"); ;
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex"); ;

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        //questManager.controlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
