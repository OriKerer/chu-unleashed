using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MarksAssets.FullscreenWebGL;

public class StartButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Platform.IsMobileBrowser())
            FullscreenWebGL.EnterFullscreen("hide");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
