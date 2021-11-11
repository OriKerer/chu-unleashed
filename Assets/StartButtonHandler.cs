using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MarksAssets.FullscreenWebGL;

public class StartButtonHandler : MonoBehaviour
{

    private bool nextSceneTrigger = false;
    private NextScene nextScene;
    private float nextSceneTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextScene = GameObject.FindObjectOfType<NextScene>();
        if(Platform.IsMobileBrowser())
            FullscreenWebGL.EnterFullscreen("hide");
    }

    // Update is called once per frame
    void Update()
    {
        if(nextSceneTrigger && nextSceneTime <= Time.time)
        {
            SceneManager.LoadScene("Level1");

        }
    }

    public void StartGame()
    {
        nextSceneTime = Time.time + nextScene.Delay;
        nextScene.nextScene();
        nextSceneTrigger = true;
    }
}
