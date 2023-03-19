using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoCT : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject sceneCT;
    // Start is called before the first frame update
    void Start()
    {
        vid = gameObject.GetComponent<VideoPlayer>();
        sceneCT = GameObject.FindGameObjectWithTag("SceneCT").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneCT.GetComponent<SceneCt>().roundTime >=22)
        {
            sceneCT.GetComponent<SceneCt>().NextLevel();
        }
    }
}
