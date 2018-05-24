using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public static bool timeUp;
    GameObject PauseMenuSystem;
	GameObject BlackScene;
    public List<Image> MenuButton;
    public List<Sprite> Stage01ButtonState;
    public List<Sprite> Stage02ButtonState;

    int SelectNum = 0;
    int MaxNum = 4, MinNum = 0;
    int InFuntionTime = 0;
	bool showMenu;
    float clickTime;
    public bool MouseHover = false;
    StageManager StageManagerscript;
    public Image PauseMenuImage;
    // Use this for initialization
    void Awake () {
        PauseMenuSystem = GameObject.Find("PauseMenu");
        PauseMenuSystem.SetActive(false);
		StageManagerscript = GameObject.Find ("StageManager").GetComponent<StageManager>();
        BlackScene = GameObject.Find ("PauseBlackScene");
		BlackScene.SetActive(false);
    }
    private void Start()
    {
        SetMenuPic();
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

    void SetMenuPic()
    {
        if (StageManager.currentStage < 5)
        {
            PauseMenuImage.sprite = Stage01ButtonState[2];
            for (int i = 0; i >5 ; i++)
            {
                MenuButton[i].sprite = Stage01ButtonState[0];
            }
            
        }
        else
        {

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
				MenuButton[i].sprite = Stage01ButtonState[0];
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
                MenuButton[SelectNum].sprite = Stage01ButtonState[0]; //idleButton               
                if (SelectNum == MinNum) SelectNum = MaxNum;
                else SelectNum--;
                MenuButton[SelectNum].sprite = Stage01ButtonState[1];
                clickTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MenuButton[SelectNum].sprite = Stage01ButtonState[0];        
                if (SelectNum == MaxNum) SelectNum = MinNum;
                else SelectNum++;
                MenuButton[SelectNum].sprite = Stage01ButtonState[1];
                clickTime = Time.time;
            }
        }
    }

	public void ClickOption(int whichButton){ //MouseClick
        Debug.Log("dfdsfsfsfsf" + whichButton);
		switch(whichButton){
		case 0:
			StageManagerscript.inMenuState = false;
			break;
		case 1:
                StartCoroutine(StageManagerscript.OnChangingScene(1f));
                break;
		case 2:
                StageManager.nextStage = StageManager.currentStage+1;
			    //StageManager.currentStage++;
                StartCoroutine(StageManagerscript.OnChangingScene(1f));
                break;
		case 3:
                //StageManager.currentStage = 2;
                StageManager.nextStage = 2;
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
                    StageManager.nextStage = StageManager.currentStage;
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));                
                    break;
			case 2:
                    StageManager.nextStage = StageManager.currentStage+1;
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));
                    break;
			case 3:
				    StageManager.nextStage = 2;
                    StartCoroutine(StageManagerscript.OnChangingScene(1f));
                    break;
			}
		}
	}

}
