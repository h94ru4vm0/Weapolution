using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MonoBehaviour {
    public GameObject P1character1;
    public GameObject P1character2;
    public GameObject P2character1;
    public GameObject P2character2;
    public GameObject scene;

    public bool type1;
    public bool type2;
    static public bool AttackerSucces;
    static public bool CrafterSucces;
    bool setActiveP1;
    bool setActiveP2;
    bool p1Ready;
    bool p2Ready;
   
    Player playerScript;
    SpriteRenderer MapP1Square;
    SpriteRenderer MapP2Square;
    // Use this for initialization
    void Start () {
        playerScript = transform.GetChild(0).GetComponent<Player>();
        MapP1Square = GameObject.Find("Mapping_square").GetComponent<SpriteRenderer>();
        MapP2Square = GameObject.Find("Mapping_square (1)").GetComponent<SpriteRenderer>();
        scene = GameObject.Find("LoadingScreen");
        setActiveP1 = false;
        setActiveP2 = false;
        P1character1 = GameObject.Find("character1");
        P1character2 = GameObject.Find("character2");
        P2character1 = GameObject.Find("character3");
        P2character2 = GameObject.Find("character4");
        P1character1.SetActive(false);
        P1character2.SetActive(false);
        P2character1.SetActive(false);
        P2character2.SetActive(false);
        type1 = false; //預設藍頭髮
        type2 = true; //預設橘頭髮
        AttackerSucces = false;
        CrafterSucces = false;
        p1Ready = false;

        Player.p1moveAble = false;
        Player.p2moveAble = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!p1Ready && setActiveP1) ChooseP1Type(Player.p1joystick);
        if (!p2Ready && setActiveP2) ChooseP2Type(Player.p2joystick);
    
        if (!setActiveP1) SetPlayer01();
        else if (!setActiveP2 && setActiveP1) SetPlayer02(); //Active 2 character

        if (AttackerSucces && CrafterSucces)
        {
            Player.isMapped = true;
            scene.SendMessage("ChangeScene");
        }
    }
    void SetPlayer01()
    {
        if ( Input.GetKeyDown("space") )
        {
            P1character1.SetActive(true);
            setActiveP1 = true;
            Player.p1controller = false;
            Player.p1joystick = null;
            Debug.Log("SetPlayer01 succes by keyboard");
        }
        else if (Input.GetButtonDown("p1ButtonA") )
        {
            setActiveP1 = true;
            Player.p1joystick = "p1";
            Player.p1controller = true;
            P1character1.SetActive(true);
            Debug.Log("SetPlayer01 succes by joystick1");
        }
        else if (Input.GetButtonDown("p2ButtonA"))
        {
            setActiveP1 = true;
            Player.p1joystick = "p2";
            Player.p1controller = true;
            P1character1.SetActive(true);
            Debug.Log("SetPlayer01 succes by joystick2");
        }
        Player.p1moveAble = false;
    }
    void SetPlayer02()
    {
        if (!Player.p1controller) //p1 用鍵盤玩
        {
            if (Input.GetButtonDown("p2ButtonA"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2joystick = "p2";
                Player.p2controller = true;
            }
            else if (Input.GetButtonDown("p1ButtonA"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2joystick = "p1";
                Player.p2controller = true;
            }
        }
        else if (Player.p1joystick == "p1")//p1用搖桿1
        {
            if (Input.GetButtonDown("p2ButtonA"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2joystick = "p2";
                Player.p2controller = true;
            }
            if (Input.GetKeyDown("space"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2controller = false;
                Player.p2joystick = null;
                Debug.Log("SetPlayer02 succes by keyboard");
            }
        }
         else if (Player.p1joystick == "p2")//p1用搖桿2
        {
            if (Input.GetButtonDown("p1ButtonA"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2joystick = "p1";
                Player.p2controller = true;
            }
            if (Input.GetKeyDown("space"))
            {
                P2character2.SetActive(true);
                setActiveP2 = true;
                Player.p2controller = false;
                Debug.Log("SetPlayer02 succes by keyboard");
            }
        }
              
    }
    void toggleCharacter(int whichPlayer)
    {
        if (whichPlayer == 1)
        {
            if (type1 == false)
            {
                P1character1.SetActive(false);
                P1character2.SetActive(true);
                type1 = true;
            }
            else
            {
                P1character1.SetActive(true);
                P1character2.SetActive(false);
                type1 = false;
            }
        }
        if (whichPlayer ==2)
        {
            if (type2 == false)
            {
                P2character1.SetActive(false);
                P2character2.SetActive(true);
                type2 = true;
            }
            else
            {
                P2character1.SetActive(true);
                P2character2.SetActive(false);
                type2 = false;
            }
        }
    }
    void ChooseP1Type(string WhichContorller)
    {
        if (!Player.p1controller) //p1用鍵盤
        {
            if (Input.GetKeyDown(KeyCode.E)) toggleCharacter(1);
            if (Input.GetKeyDown("space"))
            {
                if (type1 == false && !AttackerSucces)
                {
                    Player.p1charaType = false;
                    Debug.Log("SetPlayer01 as a Attacker");
                    AttackerSucces = true;
                    MapP1Square.color = Color.red;
                    p1Ready = true;
                }
                else if(type1 && !CrafterSucces)
                {
                    Player.p1charaType = true;
                    Debug.Log("SetPlayer01 as a Crafter");
                    CrafterSucces = true;
                    MapP1Square.color = Color.red;
                    p1Ready = true;
                }
                
            }
        }
        else
        {
            if (Input.GetButtonDown(WhichContorller + "ButtonX")) toggleCharacter(1);
            //if (Player.p1joystick == "p2" && Input.GetButtonDown("p2ButtonX")) toggleCharacter();

            if (Input.GetButtonDown(WhichContorller + "ButtonA"))
            {
                if (type1 == false && !AttackerSucces)
                {
                    Player.p1charaType = false;
                    Debug.Log("SetPlayer01 as a Attacker");
                    AttackerSucces = true;
                    MapP1Square.color = Color.red;
                    p1Ready = true;
                }
                else if (type1  && !CrafterSucces)
                {
                    Player.p1charaType = true;
                    Debug.Log("SetPlayer01 as a Crafter");
                    CrafterSucces = true;
                    MapP1Square.color = Color.red;
                    p1Ready = true;
                }
                
            }
        }
        
    }

    void ChooseP2Type(string WhichContorller)
    {
        if (!Player.p2controller) //p2用鍵盤
        {
            if (Input.GetKeyDown(KeyCode.E)) toggleCharacter(2);
            if (Input.GetKeyDown("space"))
            {
                if (type2 == false && !AttackerSucces)
                {
                    Player.p2charaType = false;
                    Debug.Log("SetPlayer02 as a Attacker");
                    AttackerSucces = true;
                    MapP2Square.color = Color.red;
                    p2Ready = true;
                }
                else if (type2 && !CrafterSucces)
                {
                    Player.p2charaType = true;
                    Debug.Log("SetPlayer02 as a Crafter");
                    CrafterSucces = true;
                    MapP2Square.color = Color.red;
                    p2Ready = true;
                }
               
            }
        }
        else
        {

            if (Input.GetButtonDown(WhichContorller + "ButtonX")) toggleCharacter(2);

            if (Input.GetButtonDown(WhichContorller+ "ButtonA"))
            {
                if (type2 == false && !AttackerSucces)
                {
                    Player.p2charaType = false;
                    Debug.Log("SetPlayer02 as a Attacker");
                    AttackerSucces = true;
                    MapP2Square.color = Color.red;
                    p2Ready = true;
                }
                else if (type2 && !CrafterSucces)
                {
                    Player.p2charaType = true;
                    Debug.Log("SetPlayer02 as a Crafter");
                    if (!CrafterSucces) CrafterSucces = true;
                    MapP2Square.color = Color.red;
                    p2Ready = true;
                }
               
            }
        }

    }
}
