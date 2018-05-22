using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {

    public bool isfillingPowder = false;
    public bool startFilled = false;
    public bool canFiiled = false;
    string whichPlayer = "p1";
    bool readyToShoot = false;

    float p1_L_JoyX;
    float p1_L_JoyY;
    float p2_L_JoyX;
    float p2_L_JoyY;

    GameObject RightCanon, LeftCanon;
    public GameObject RightAim,LeftAim;
    bool ShowRightAim = false, ShowLeftAim = false;
    List<Vector3> DefaultAimPos;
    public int CanonNum;
    float speed = 10f;
    Crafter CrafterScript;
    public CraftSystem CraftSystemScript;


    private void Awake()
    {
        CrafterScript = GameObject.Find("character2").GetComponent<Crafter>();
        RightCanon = GameObject.Find("Canon");
        LeftCanon = GameObject.Find("Canon (1)");
        //CraftSystemScript = GameObject.Find("CraftSystem").GetComponent<CraftSystem>();
        Debug.Log(GameObject.Find("CraftSystem").GetComponent<CraftSystem>());
        Debug.Log("CraftSystemScript：  " + CraftSystemScript);
    }
    private void Start()
    {
        if (Player.p2charaType) whichPlayer = Player.p2joystick;
        else whichPlayer = Player.p1joystick;
        DefaultAimPos[0] = RightCanon.transform.position + new Vector3(-3f, 2.5f, 0);
        DefaultAimPos[1] = LeftCanon.transform.position + new Vector3(3f, 2.5f, 0);
        RightAim.SetActive(false);
        LeftAim.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        LeftListener();
        if (!isfillingPowder) //notFillingInPowder
        {
            if (canFiiled)
            {
                FillingInPowder();
                if (startFilled) CrafterScript.Gathering();
            }
        }
        else
        {
            if (!readyToShoot) AimControl();
            else ShootBullet();

        }

    }

    void LeftListener()
    {
        p1_L_JoyX = Input.GetAxis("p1LHorizontal");
        p1_L_JoyY = Input.GetAxis("p1LVertical");
        p2_L_JoyX = Input.GetAxis("p2LHorizontal");
        p2_L_JoyY = Input.GetAxis("p2LVertical");
    }

    void ShootBullet()
    {

    }
    void ShowAim()
    {
        if (CanonNum == 0)
        {
            RightAim.SetActive(true);
            RightAim.transform.position = DefaultAimPos[0];
            ShowRightAim = true;
        }
        else
        {
            LeftAim.SetActive(true);
            LeftAim.transform.position = DefaultAimPos[1];
            ShowLeftAim = true;

        }
    }
    void AimControl()
    {
        
        if (Player.p2charaType)
        {
            if (Player.p2controller)
            {
                if (whichPlayer == "p1") RightAim.transform.position += new Vector3(p1_L_JoyX / speed, p1_L_JoyY / speed, 0);
                else RightAim.transform.position += new Vector3(p2_L_JoyX / speed, p2_L_JoyY / speed, 0);
            }
            else //用鍵盤
            {

            }

        }
        else
        {
            RightAim.transform.position += new Vector3(p1_L_JoyX, p1_L_JoyY, 0);
            Debug.Log("Aim.transform.positios  1");

        }
    }
    void FillingInPowder()
    {
        
        if (Player.p2charaType)
        {
            if (Player.p2controller && Input.GetButtonDown(whichPlayer + "LB"))
            {
                Player.p2moveAble = false;
                startFilled = true;
            }
            else if (!Player.p2controller && Input.GetKeyDown(KeyCode.E))
            {
                Player.p2moveAble = false;
                startFilled = true;
            }
        }
        else
        {
            if (Player.p1controller && Input.GetButtonDown(whichPlayer + "LB"))
            {
                Player.p1moveAble = false;
                startFilled = true;
            }
            else if (!Player.p1controller && Input.GetKeyDown(KeyCode.E))
            {
                Player.p1moveAble = false;
                startFilled = true;

            }
        }


    }

    public void CallCraftSystemFucion()
    {
        CraftSystemScript.ThrowOut();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CraftSystemScript.craftA" + CraftSystemScript);
        if (collision.tag == "Player")
        {
            if (CraftSystemScript.CheckHandle().id == 3)
            {
                canFiiled = true;
            }
                
            else return;

        }
    }
}
