using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonSystem : MonoBehaviour {

    GameObject RightCanon, LeftCanon;
    public GameObject RightAim, LeftAim;
    string whichPlayer = "p1";
    int CanonNum;
    float p1_L_JoyX;
    float p1_L_JoyY;
    float p2_L_JoyX;
    float p2_L_JoyY;

    private void Awake()
    {
        RightAim.SetActive(false);
        LeftAim.SetActive(false);

        RightCanon = GameObject.Find("Canon");
        LeftCanon = GameObject.Find("Canon (1)");
    }
    void Update()
    {
        LeftListener();
    }
    void ShowAim()
    {
        if (CanonNum == 0)
        {
            RightAim.SetActive(true);
            //RightAim.transform.position = DefaultAimPos[0];
            //ShowRightAim = true;
        }
        else
        {
            LeftAim.SetActive(true);
            //LeftAim.transform.position = DefaultAimPos[1];
            //ShowLeftAim = true;

        }
    }
    void LeftListener()
    {
        p1_L_JoyX = Input.GetAxis("p1LHorizontal");
        p1_L_JoyY = Input.GetAxis("p1LVertical");
        p2_L_JoyX = Input.GetAxis("p2LHorizontal");
        p2_L_JoyY = Input.GetAxis("p2LVertical");
    }

    void AimControl()
    {

        if (Player.p2charaType)
        {
            if (Player.p2controller)
            {
                //if (whichPlayer == "p1") RightAim.transform.position += new Vector3(p1_L_JoyX / speed, p1_L_JoyY / speed, 0);
                //else RightAim.transform.position += new Vector3(p2_L_JoyX / speed, p2_L_JoyY / speed, 0);
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
}
