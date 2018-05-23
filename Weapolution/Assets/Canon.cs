using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {

    public bool Canon1isfillingPowder = false;
    public bool Canon2isfillingPowder = false;

    public bool startFilled = false;
    public bool Canon1canFiiled = false;
    public bool Canon2canFiiled = false;

    int Canon1PowderNum = 0;
    int Canon2PowderNum = 0;

    public bool Canon1Filled = false;
    public bool Canon2Filled = false;

    bool readyToShoot = false;
    string whichPlayer = "p1";

    GameObject RightCanon, LeftCanon;
    public int CanonNum;
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
    }
    // Update is called once per frame
    void Update()
    {
        if (CanonNum == 0)
        {
            if (!Canon1isfillingPowder) //notFillingInPowder
            {
                if (Canon1canFiiled)
                {
                    FillingInPowder();
                    if (startFilled) CrafterScript.Gathering();
                }
            }
        }
        else
        {
            if (!Canon2isfillingPowder) //notFillingInPowder
            {
                if (Canon2canFiiled)
                {
                    FillingInPowder();
                    if (startFilled) CrafterScript.Gathering();
                }
            }
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

    public void OverFinlling()
    {
        if (Canon1Filled)
        {
            if (Canon1PowderNum != 0)
            {
                Canon1isfillingPowder = true;
            }
            if (Canon1PowderNum >= 2)
            {
                Canon1isfillingPowder = true;
                Canon1canFiiled = false;
            }
            Canon1Filled = false;
        }

        if (Canon2Filled)
            {
                if (Canon2PowderNum != 0)
                {
                    Canon2isfillingPowder = true;
                }
                if (Canon2PowderNum >= 2)
                {
                    Canon2isfillingPowder = true;
                    Canon2canFiiled = false;
                }
                Canon2Filled = false;
            }
               
    }
    public void CallCraftSystemFucion()
    {
        CraftSystemScript.ThrowOut();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (CraftSystemScript.CheckHandle().id == 3)
            {
                if(CanonNum == 0)
                {
                    Canon1canFiiled = true;
                    Canon1PowderNum++;
                    Canon1Filled = true;
                }
                else
                {
                    Canon2canFiiled = true;
                    Canon2PowderNum++;
                    Canon2Filled = true;
                }
            }
                
            else return;

        }
    }
}
