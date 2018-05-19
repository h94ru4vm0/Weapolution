using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public static bool timeUp;
    GameObject PauseMenuSystem;
    public List<Image> MenuButton;
    public List<Sprite> ButtonState;
    int SelectNum = 0;
    int MaxNum = 4, MinNum = 0;
    int InFuntionTime = 0;
    float clickTime;
    public bool MouseHover = false;
    bool inMenuState;
    // Use this for initialization
    void Awake () {
        PauseMenuSystem = GameObject.Find("PauseMenu");
        PauseMenuSystem.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShowMenu()
    {

        if (inMenuState)
        {
            PauseMenuSystem.SetActive(true);
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 100);
        }
        else
        {
            PauseMenuSystem.SetActive(false);
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }

    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //StartCoroutine(SlowDown(0.5f,true));
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
            ShowMenu();
        }
        OnControlMenu();
      
    }

    void OnControlMenu()
    {
        if (Time.time - clickTime > 0.2f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MenuButton[SelectNum].sprite = ButtonState[0];
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
}
