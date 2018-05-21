using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public static bool timeUp;
    GameObject PauseMenuSystem;
	GameObject BlackScene;
    public List<Image> MenuButton;
    public List<Sprite> ButtonState;
    int SelectNum = 0;
    int MaxNum = 4, MinNum = 0;
    int InFuntionTime = 0;
	bool showMenu;
    float clickTime;
    public bool MouseHover = false;
	StageManager  StageManagerscript;
    // Use this for initialization
    void Awake () {
        PauseMenuSystem = GameObject.Find("PauseMenu");
        PauseMenuSystem.SetActive(false);
		StageManagerscript = GameObject.Find ("StageManager").GetComponent<StageManager>();
		BlackScene = GameObject.Find ("PauseBlackScene");
		BlackScene.SetActive(false);
    }
	
	// Update is called once per frame=
	void Update () {

		ShowMenu ();
		if(showMenu){
			OnControlMenu ();
			MouseControl ();
			LockOption ();
		}
	}

    void ShowMenu()
    {

		if (StageManagerscript.inMenuState)
        {
            PauseMenuSystem.SetActive(true);
			BlackScene.SetActive(true);
			showMenu = true;
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 100);
        }
        else
        {
            PauseMenuSystem.SetActive(false);
			BlackScene.SetActive(false);
			showMenu = false;
            SelectNum = 0;
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }

    }

	void MouseControl(){
		if(MouseHover && InFuntionTime == 0){
			for(int i = 0; i < 5; i++){
				MenuButton[i].sprite = ButtonState[0];
			}
			SelectNum = 0;
			InFuntionTime++;
		}
		else if(!MouseHover){
			InFuntionTime = 0;
		}
	}

    void OnControlMenu()
    {
        if (Time.time - clickTime > 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MenuButton[SelectNum].sprite = ButtonState[0]; //idleButton               
                if (SelectNum == MinNum) SelectNum = MaxNum;
                else SelectNum--;
                MenuButton[SelectNum].sprite = ButtonState[1];
                clickTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MenuButton[SelectNum].sprite = ButtonState[0];        
                if (SelectNum == MaxNum) SelectNum = MinNum;
                else SelectNum++;
                MenuButton[SelectNum].sprite = ButtonState[1];
                clickTime = Time.time;
            }
        }
    }

	public void ClickOption(int whichButton){ //MouseClick
		switch(whichButton){
		case 0:
			StageManagerscript.inMenuState = false;
			break;
		case 1:
                StartCoroutine(StageManagerscript.OnChangingScene(1f));
                break;
		case 2:
			    StageManager.currentStage++;
                StartCoroutine(StageManagerscript.OnChangingScene(1f));
                break;
		case 3:
			    StageManager.currentStage = 2;
                StartCoroutine(StageManagerscript.OnChangingScene(1f));
                break;
		}
	}

	void LockOption(){ //keyboard
		if(Input.GetKeyDown(KeyCode.Return)){
			switch(SelectNum){
			case 0:
				    StageManagerscript.inMenuState = false;
				    break;
			case 1:
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));                
                    break;
			case 2:
				    StageManager.currentStage++;
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));
                    break;
			case 3:
				    StageManager.currentStage = 2;
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));
                    break;
			}
		}
	}

}
