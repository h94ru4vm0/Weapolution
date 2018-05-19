using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {


    public static bool timeUp;
    public static  int currentStage = -2;


    bool inMenuState, inTransState, stageBegin, stageOver;
    bool inChanging;
    float slowTime;
    string whichPlayerControl;
    Dialog dialog;
    TeamHp teamHP;
    //GameObject dialog;
    SceneTransRender transRender;
    AudioSource BGM, CharacterSound,MonsterSound;
    Animator animator;

    GameObject PauseMenu;
    GameObject BlackScene;
    public List<Image> MenuButton;
    public List<Sprite> ButtonState;
    int SelectNum = 0;
    int MaxNum = 4, MinNum = 0;
    int InFuntionTime = 0;
    float clickTime;
    public bool MouseHover = false;

    private void Awake()
    {
        //if (stageManager == null) stageManager = this;
        animator = GetComponent<Animator>();
        if (currentStage > -1)
        {
            transRender = Camera.main.GetComponent<SceneTransRender>();
            transRender.stageManager = this;
            BGM = GameObject.Find("map").GetComponent<AudioSource>();
            MonsterSound = GameObject.Find("MonsterAudio").GetComponent<AudioSource>();
            CharacterSound = GameObject.Find("CharacterAudio").GetComponent<AudioSource>();
            if (currentStage > 0) {
                dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
                teamHP = GameObject.Find("TeamHp").GetComponent<TeamHp>();
                dialog.gameObject.SetActive(false);
            }

        }

        PauseMenu = GameObject.Find("PauseMenu");
        BlackScene = GameObject.Find("BlackScene");
        PauseMenu.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (currentStage < 0) return;
        if (!stageBegin)
        {
            if (!timeUp) {
                timeUp = true;
            }
            if (!inChanging)
            {
                inChanging = true;
                if (currentStage < 1) animator.Play("BlackIn");
                else transRender.SetIsGoIn(true);
            }
        }
        else {
            GetInput();
            //if (Input.GetKeyDown(KeyCode.Space)) SetCurStageOver(true);
        }

	}

    public void ToStageBegin() {
        stageBegin = true;
        timeUp = false;
    }

    void ShowMenu() {
        
       if (inMenuState)
       {
            PauseMenu.SetActive(true);
            animator.Play("BlackIn");
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 100);
       }
        else
        {
            PauseMenu.SetActive(false);
            animator.Play("BlackOut");
            //BlackScene.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }
        
        

    }

    void GetInput() {
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
        if (timeUp) {
            if (stageOver) {
               
            }
           
        }
    }

    void OnControlMenu() {
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

    

    public void ChangeSceneBlackOut()
    {
        animator.Play("BlackOut");
    }

    public void SetCurStageOver(bool _isWin) {
        if (stageOver) return;
        stageOver = true;
        timeUp = true;
        dialog.gameObject.SetActive(true);
        dialog.SetDialogOn(_isWin, SetToTransState, StartToTrans);
        //transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
    }


    public void SetToTransState() {
        //inTransState = true;
        //dialog.gameObject.SetActive(false);
        //teamHP.CloseHpUi();
        transRender.SetIsGoIn(false);
        transRender.SetTransRenderOn(SceneTransRender.shaderType.glitch);
    }


    public void StartToTrans() {
        dialog.gameObject.SetActive(false);
        teamHP.CloseHpUi();
        transRender.SetStartTrans();
        StartCoroutine(OnChangingScene(1.0f));  //白屏後切場景
    }

    public IEnumerator SlowDown(float slowTime, bool _isWin) {
        Time.timeScale = 0.2f;
        BGM.pitch = 0.35f;
        MonsterSound.pitch = 0.35f;
        CharacterSound.pitch = 0.35f;
        yield return new WaitForSecondsRealtime(slowTime);
        Time.timeScale = 1.0f;
        BGM.pitch = 1.0f;
        MonsterSound.pitch = 1.0f;
        CharacterSound.pitch = 0.35f;
        yield return null;
        SetCurStageOver(_isWin);
    }

    public IEnumerator OnChangingScene(float time) {
        yield return new WaitForSeconds(time);
        Debug.Log("currentsdadasdasda" + currentStage);
        SceneManager.LoadScene(currentStage);
    }

}
