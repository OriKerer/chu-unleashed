using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class webglVideoFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
//#if !UNITY_EDITOR
        GameObject go = GameObject.Find("Square");
        var videoPlayer = go.GetComponent<UnityEngine.Video.VideoPlayer>();

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "background-vid.mp4");
//#endif
    }
}
