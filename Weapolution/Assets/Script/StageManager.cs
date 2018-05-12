using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour {


    public static bool timeUp;
    public static  int currentStage = 0;


    bool inMenuState, inTransState, stageOver;
    float slowTime;
    string whichPlayerControl;
    Dialog dialog;
    TeamHp teamHP;
    //GameObject dialog;
    SceneTransRender transRender;

    

    private void Awake()
    {
        //if (stageManager == null) stageManager = this;
        transRender = Camera.main.GetComponent<SceneTransRender>();
        dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
        teamHP = GameObject.Find("TeamHp").GetComponent<TeamHp>();
        dialog.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        if (Input.GetKeyDown(KeyCode.Space)) SetCurStageOver(true);
	}


    void ShowMenu() {
        
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMenuState)
            {
                timeUp = false;
                inMenuState = false;
            }
            else
            {
                inMenuState = true;
                timeUp = true;
            }

        }
        if (timeUp) {
            if (stageOver) {
               
            }
           
        }
    }

    void OnControlMenu() {

    }

    public void SetCurStageOver(bool _isWin) {
        if (stageOver) return;
        stageOver = true;
        timeUp = true;
        dialog.gameObject.SetActive(true);
        dialog.SetDialogOn(_isWin, SetToTransState);
        //transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
        
    }

    public void SetToTransState() {
        //inTransState = true;
        dialog.gameObject.SetActive(false);
        teamHP.CloseHpUi();
        transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
    }

    public IEnumerator SlowDown(float slowTime, bool _isWin) {
        Time.timeScale = 0.2f;
        GameObject.Find("map").GetComponent<AudioSource>().pitch = 0.35f;

        yield return new WaitForSecondsRealtime(slowTime);
        Time.timeScale = 1.0f;
        GameObject.Find("map").GetComponent<AudioSource>().pitch = 1.0f;
        yield return null;
        SetCurStageOver(_isWin);
    }



}
