using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // Start is called before the first frame update
    //어떤 대사가 들어갈지
    Dictionary<int, string[]> talkData;//id, 대사
    Dictionary<int, Sprite> portraitData;//초상화 데이터
    Dictionary<int, string> nameData;
    public Sprite[] portraitArr;
    

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        nameData = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData(){
        talkData.Add(1000, new string[] { "안녕?:0", "보고싶었어.:3" });//npc A :number -> index
        talkData.Add(2000, new string[] { "최재영은 정말 바보라고:3", "ㄹㅇㅋㅋ:2" });//npc A :number -> index
        talkData.Add(100, new string[] { "나는 책상이야" });//desk
        talkData.Add(200, new string[] { "나는 박스라구!" });//box
        //quest talk
        talkData.Add(10 + 1000, new string[] { "재영이 정말 화가나.:1", "집에오면 안치워.:0" });
        talkData.Add(11 + 2000, new string[] { "재영이를 처리할 계획을 짜고있어:2", "혼쭐을 내줄건데 너도 참여할래?:3" });

        talkData.Add(20 + 2000, new string[] { "옆에 돌덩이를 들고 내리쳐줘:3", "아주 재밌을거야:3" });
        talkData.Add(20 + 5000, new string[] { "상당히 무거운 돌이다." });
        talkData.Add(21 + 2000, new string[] { "진행시켜:1" });




        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);

        nameData.Add(1000, "진수");
        nameData.Add(2000, "은정");
    }

    public string GetTalk(int id,int talkIndex)//id, 대화내용 선택을 위한 index
    {
        if (!talkData.ContainsKey(id))//
        {
            //퀘스트 진행순서 중 대사가 없을 때.
            //퀘스트 맨 처음 대사를 가지고 온다. 
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
                
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }

                //퀘스트 맨 처음 대사마저 없을 때
                //기본대사를 가지고 오면 됨
        }//안에 내용이 있니없니
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

    public string GetName(int id)
    {
        return nameData[id];
    }
}
