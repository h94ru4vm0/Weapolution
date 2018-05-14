using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage : MonoBehaviour {
    public bool isChoosed = false;
    GameObject Camera;
    bool MoveOnlyOnce;
    float p1_L_JoyX = 0.0f;
    float p1_L_JoyY = 0.0f;
    float p2_L_JoyX = 0.0f;
    float p2_L_JoyY = 0.0f;
    public List<GameObject> StageImage;
    int stageNum = 0;
    // Use this for initialization
    void Awake () {
        Camera = GameObject.Find("Main Camera");
        StageImage[0] = GameObject.Find("tutorial");
        StageImage[1] = GameObject.Find("stage1");
        StageImage[2] = GameObject.Find("stage2");
        StageImage[1].SetActive(false);
        StageImage[2].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        LeftListener();
        if (isChoosed) MoveCamera();
        ChoosingStage();
    }
    void MoveCamera()
    {
        if(Camera.transform.position.x < 25)
        {
            Camera.transform.position += new Vector3(0.5f, 0, 0);

        }
        else if (Camera.transform.position.x >25)
        {
            Camera.transform.position = new Vector3(25f, 0, 0);
        }
        else if (Camera.transform.position.x == 25)
        {
            return;
        }
    }

    void LeftListener()
    {
        p1_L_JoyX = Input.GetAxis("p1LHorizontal");
        p1_L_JoyY = Input.GetAxis("p1LVertical");
        p2_L_JoyX = Input.GetAxis("p2LHorizontal");
        p2_L_JoyY = Input.GetAxis("p2LVertical");
    }

    void ChoosingStage() //target左右動
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) ) 
        {
            StageImage[stageNum].SetActive(false);
            if (stageNum == 2) stageNum = 0;
            else stageNum += 1;
            StageImage[stageNum].SetActive(true);
            Debug.Log(stageNum);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StageImage[stageNum].SetActive(false);
            if (stageNum == 0) stageNum = 2;
            else stageNum -= 1;
            StageImage[stageNum].SetActive(true);
        }
        if (0.5f <= Mathf.Abs(p1_L_JoyX) && !MoveOnlyOnce)
        {

        }
        if (0.5f >= Mathf.Abs(p1_L_JoyX) && MoveOnlyOnce)
        {
            MoveOnlyOnce = false;
        }


    }
}
