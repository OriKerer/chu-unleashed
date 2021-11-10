using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public int CharPerSeconds;//CPS
    Text msgText;
    public GameObject endCursor;
    int index;
    float interval;
    AudioSource audioSource;
    public bool isAnim;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
        
    }

    void EffectStart()
    {
        msgText.text = "";
        isAnim = true;
        index = 0;
        endCursor.SetActive(false);
        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];

        //Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;
        
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        endCursor.SetActive(true);
        isAnim = false;
    }

}
