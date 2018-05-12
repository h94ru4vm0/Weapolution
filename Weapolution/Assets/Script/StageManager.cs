using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour {

    public static bool timeUp;
    public static int currentStage = 0;


    bool inMenuState,inTransState, stageOver, slowDownOver;
    float slowTime;
    string whichPlayerControl;
    Dialog dialog;
    //GameObject dialog;
    SceneTransRender transRender;

    

    private void Awake()
    {
        transRender = Camera.main.GetComponent<SceneTransRender>();
        dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
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

    void OnSlowMode() {
        slowTime += Time.unscaledDeltaTime;
        if (slowTime > 0.5f) slowDownOver = true;
    }



    public void SetCurStageOver(bool _isWin) {
        stageOver = true;
        timeUp = true;
        dialog.gameObject.SetActive(true);
        dialog.SetDialogOn(_isWin, SetToTransState);
        //transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
        
    }

    public void SetToTransState() {
        //inTransState = true;
        transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
    }

    public static IEnumerator SlowDown(float slowTime) {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(slowTime);
        Time.timeScale = 1.0f;
    }

}
