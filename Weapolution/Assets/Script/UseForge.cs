﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseForge : MonoBehaviour {
    bool closeEnough, craftFuncInForge;
    CraftSystem craftSystem;
    CForge forge;
    public float forge_dis;
    // Use this for initialization
    void Awake () {
        craftSystem = this.transform.GetComponent<CraftSystem>();
        forge = GameObject.Find("Forge").GetComponent<CForge>();
    }
	
	// Update is called once per frame
	void Update () {
        if (StageManager.timeUp) return;
        NearForge();
        ThrowInForge();
    }

    void NearForge() {
        if (forge.showUp) {
            if (!closeEnough)
            {
                if (!craftSystem.CheckIsFree()) return;
                if (Vector2.Distance(this.transform.position, forge.fixed_pos) < forge_dis) {
                    if (!forge.isHeat)
                    {
                        if (craftSystem.CheckHandle().id == 3)
                        {
                            closeEnough = true;
                            craftSystem.ArrowEnable(false);
                            craftSystem.craftFunc = false;
                            forge.transform.GetComponent<COutLine>().SetOutLine(true);
                        }

                    }
                    else {
                        closeEnough = true;
                        craftSystem.ArrowEnable(false);
                        craftSystem.craftFunc = false;
                        forge.transform.GetComponent<COutLine>().SetOutLine(true);
                    }

                }
            }
            else {
                if (Vector2.Distance(this.transform.position, forge.fixed_pos) >= forge_dis) {
                    closeEnough = false;
                    craftSystem.ArrowEnable(true);
                    craftSystem.craftFunc = true;
                    forge.transform.GetComponent<COutLine>().SetOutLine(false);

                }
            }
        }
    }

    void ThrowInForge()
    {
        if (closeEnough)
        {
            if (craftSystem.useController)
            {
                //往鍛造爐丟材料
                if (Input.GetButtonDown(craftSystem.whichPlayer + "RB"))
                {
                    if (craftSystem.CheckHandle().id == 3) {
                        forge.ThrowFireIn();
                        craftSystem.ThrowOut();
                    }
                    else
                    {
                        if (forge.ThrowElementIn(craftSystem.CheckHandle().id)) {
                            craftSystem.ThrowOut();
                        }
                        
                    }
                }
                else if (Input.GetButtonDown(craftSystem.whichPlayer + "LB")) {
                    forge.OnForging();
                }
            }
            else
            {
                //往鍛造爐丟材料
                if (Input.GetMouseButtonDown(0))
                {
                    if (craftSystem.CheckHandle().id == 3)
                    {
                        forge.ThrowFireIn();
                        craftSystem.ThrowOut();
                    }
                    else
                    {
                        if (forge.ThrowElementIn(craftSystem.CheckHandle().id))
                        {
                            craftSystem.ThrowOut();
                        }

                    }
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    forge.OnForging();
                }
            }
        }
    }

}
