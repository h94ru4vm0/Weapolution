using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamHp : MonoBehaviour {


    public GameObject TeamHpLine;
    public Player PlayerScript;
    public Crafter CrafterScript;
    PuaseOnCanvas PuaseScript;
    public Image HpBoarder;
    bool changeColor01;
    bool changeColor02;
    static public float teamHp = 1; //滿血是1
    int inFuctionTime = 0; 

    void Awake () {
        teamHp = 1;

    }
    void Start()
     {
        TeamHpLine = GameObject.Find("TeamHp");
        PlayerScript = GameObject.Find("character1").GetComponent<Player>();
        CrafterScript = GameObject.Find("character2").GetComponent<Crafter>();
        PuaseScript = GameObject.Find("map").GetComponent<PuaseOnCanvas>();
        HpBoarder = GameObject.Find("bloodImage").GetComponent<Image>();
        changeColor01 = false;
        changeColor02 = false;
    }

    private void Update()
    {
        RenderUI();
        CheckHp();
    }

    void RenderUI()
     {
        TeamHpLine.GetComponent<Image>().fillAmount = teamHp; //render

        if (teamHp > 0.2f && teamHp <= 0.5f) //hp 30%~50%
        {
            if (!changeColor01)
            {
                TeamHpLine.GetComponent<Image>().color = new Color32(255, 176, 92, 255);
                HpBoarder.sprite = Resources.Load<Sprite>("image/Stage/1/HpImage/blood50_");
                changeColor01 = true;
            }
            else return;
        }
        else if (teamHp > 0 && teamHp <= 0.2f) //hp 0~20%
        {
            if (!changeColor02)
            {
                TeamHpLine.GetComponent<Image>().color = new Color32(249, 79, 68, 255);
                HpBoarder.sprite = Resources.Load<Sprite>("image/Stage/1/HpImage/blood20_");
                changeColor02 = true;
            }
            else return;
        }
        else if (teamHp <= 0)
        {
            HpBoarder.sprite = Resources.Load<Sprite>("image/Stage/1/HpImage/blood0_");
        }

    }
    void CheckHp()
    {
        if (teamHp<0 && inFuctionTime ==0)
        {
            Debug.Log(inFuctionTime + "///" + this.gameObject.name);
            PlayerScript.Gameover();
            CrafterScript.Gameover();      
            inFuctionTime++;
        }
    }
    
}
