using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {

    public bool isfillingPowder = false;
    public bool startFilled = false;
    public bool canFiiled = false;
    string whichPlayer = "p1";
    bool readyToShoot = false;

    

    GameObject RightCanon, LeftCanon;
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
    }
    // Update is called once per frame
    void Update()
    {
        
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
            //if (!readyToShoot) AimControl();
            //else ShootBullet();

        }

    }

   

    void ShootBullet()
    {

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
