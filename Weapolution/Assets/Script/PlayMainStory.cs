using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainStory : MonoBehaviour {

    public MovieTexture movTexture; //電影材質
    private AudioSource movAudio; //影片音軌
    GameObject loadScece;

    bool videoStart;
    int beCalledTime;

    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        loadScece = GameObject.Find("LoadingScreen");
        movTexture.loop = false;
        videoStart = false;
        movAudio = transform.GetComponent<AudioSource>();
        beCalledTime = 0;
    }
    void Update()
    {

        if ( !videoStart && movTexture.isPlaying == false)
        {
            //GetComponent<AudioSource>().Play();
            movTexture.Play();
            movAudio.Play();
            videoStart = true;
        }
        if( !movTexture.isPlaying && Input.GetKeyDown(KeyCode.P))
        {
            movTexture.Play();
            movAudio.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<AudioSource>().Pause();
            movTexture.Pause();
            movAudio.Pause();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movTexture.Stop();
            movAudio.Stop();
        }

        if (!movTexture.isPlaying && beCalledTime ==0)
        {
            loadScece.SendMessage("ChangeScene");
            beCalledTime++;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            loadScece.SendMessage("ChangeScene");
        }
    }
}
    
    


