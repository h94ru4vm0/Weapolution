﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour {
    int final_id;
    float throwDegree = 0.0f;
    bool can_pick = false, b_handling, can_collect = false;
    bool[] craft_records;
    Vector3 throwVec3;
    Transform Arrow,collectBarBk, collectBar;
    GameObject handling_item;
    SpriteRenderer handle, slot_img, slot_item_img;
    Animator Crafteranimator;
    CPickItem picking_item;
    CPickCollection pick_collect;
    CItem craftA, craftB, craftC;
    CraftMenu craftMenu;
    [HideInInspector]
    public bool craftFunc = true , useController = false;
    [HideInInspector]
    public string whichPlayer;
    public bool test;
    public float throwSpeed;
    public int spikeNum = 0;
    public CItem[] items;
    public GameObject Slot;
    public bool isCraftPlayer;
    public TutorialRequest tutorialRequest;

    // CCraftItem craftReslut;

    // Use this for initialization
    void Awake () {      
        if (!isCraftPlayer) this.gameObject.SetActive(false);
        handle = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        handle.enabled = false;
        //items = ItemDataBase.GetItemDataBase();
        //ItemDataBase.SetSpriteList();
        //CItemDataBase.SetItemDataBase();
        //CItemDataBase.SetSpriteList();
        items = CItemDataBase.items;
        slot_img = Slot.GetComponent<SpriteRenderer>();
        slot_item_img = Slot.transform.GetChild(0).GetComponent<SpriteRenderer>();
        collectBarBk = transform.Find("CollectBar").GetChild(0);
        collectBar = transform.Find("CollectBar").GetChild(1);
        slot_img.enabled = true;
        slot_item_img.enabled = true;
        //craftReslut = Slot.GetComponent<CCraftItem>();
        ChangeSlot(true, 0);
        craft_records = new bool[items.Length];
        Arrow = this.transform.Find("Arrow");
        //craftMenu = GameObject.Find("CraftMenu").GetComponent<CraftMenu>();
        //Crafteranimator = GameObject.Find("character2").GetComponent<Animator>();
        craftFunc = true;
    }

    private void Start()
    {
        if (Player.p1charaType)
        {
            if (Player.p1controller) //p1用搖桿
            {
                useController = true;
                whichPlayer = Player.p1joystick;
            }
            else
            {
                useController = false;

            }

        }
        else
        {
            if (Player.p2controller) //p1用搖桿
            {
                useController = true;
                whichPlayer = Player.p2joystick;
            }
            else
            {
                useController = false;

            }
        }
        //Debug.Log(whichPlayer);
        //whichPlayer = "p2";
        //useController = false;
    }
    // Update is called once per frame
    void Update () {
        if (StageManager.timeUp) return;
        if (craftFunc) {
            if (can_pick) CheckPickItem();
            ThrowItem();
            PickItem();
            Collect();
        } 
        //是否靠近鍛造爐
        //if (forge.showUp && Vector2.Distance(this.transform.position, forge.fixed_pos) < forge_dis)
        //    NearForge();
        //else ThrowItem();



        //if (Input.GetButton(whichPlayer + "ButtonA"))
        //{
        //    Debug.Log("trap");
        //    if (spikeNum < 3 && !trapOnce) {
        //        Instantiate(spike, transform.parent.position, Quaternion.identity);
        //        spikeNum++;
        //        trapOnce = true;
        //    } 
        //}
        //if (trapTime < 1.0f) trapTime += Time.deltaTime;
        //else {
        //    trapTime = 0.0f;
        //    trapOnce = false;
        //} 

    }

    void CheckPickItem() {
        if (picking_item != null) {
            if (!picking_item.gameObject.activeSelf) {
                can_pick = false;
                picking_item = null;
                ChangeSlot(true, -1);
            }
        }
    }

    void PickItem()
    {
        if (!can_pick || picking_item == null) return;
        if (useController)
        {
            if (Input.GetButtonDown(whichPlayer + "LB"))
            {
                handle.enabled = true;
                can_pick = false;
                //if (picking_item == null) return;
                //Crafteranimator.SetBool("is_gather", true);
               // Debug.Log("設動畫");
                if (!b_handling)
                {
                    b_handling = true;
                    craftA = items[picking_item.id];
                    handling_item = picking_item.gameObject;
                    handling_item.transform.parent = transform.GetChild(0);
                    handling_item.transform.position = this.transform.position;
                    handling_item.GetComponent<COutLine>().SetOutLine(false);
                    handling_item.SetActive(false);
                    handle.sprite = CItemDataBase.spriteList[craftA.id];
                    handling_item.GetComponent<CPickItem>().SetLifeTime(0.0f);
                    ChangeSlot(true, 0);
                    ArrowEnable(true);
                    if (test)tutorialRequest.DonePickUp(true);
                }
                else
                {
                    OnCrafting();
                }
                can_pick = false;
                picking_item = null;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if (picking_item == null) return;
                //Crafteranimator.SetBool("is_gather", true);

                handle.enabled = true;
                if (!b_handling)
                {
                    b_handling = true;
                    craftA = items[picking_item.id];
                    handling_item = picking_item.gameObject;
                    handling_item.transform.parent = transform.GetChild(0);
                    handling_item.transform.position = this.transform.position;
                    handling_item.GetComponent<COutLine>().SetOutLine(false);
                    handling_item.SetActive(false);
                    handle.sprite = CItemDataBase.spriteList[craftA.id];
                    ChangeSlot(true, 0);
                    ArrowEnable(true);
                    if (test) tutorialRequest.DonePickUp(true);
                }
                else
                {
                    OnCrafting();
                }
                can_pick = false;
                picking_item = null;
            }
        }
      
    }

    void CheckCrafting() {
        //if (craftA.id == 3 || craftB.id == 3 || 
        //    craftA.id == items.Length-1 || craftB.id == items.Length - 1) return; //合成元素為火
        final_id = craftA.elementID + craftB.elementID;
        for (int i = 0; i < items.Length; i++)
        {
            if (final_id == items[i].craftingID)
            {
                Debug.Log("final_id" + final_id);
                if (craft_records[i]) ChangeSlot(false, i);
                else ChangeSlot(false, 0);
                Debug.Log("craft final" + final_id);
                craftC = items[i];
                return;
            }
        }
        //Debug.Log();
        ChangeSlot(false, 0);
        craftC = items[items.Length - 1];
    }

    void OnCrafting() {
        if (craftC == null) return;
        ChangeSlot(true, 0);
        craftA = craftC;
        picking_item.SetInFree();

        if (craftC.id > 0)
        {
            handle.sprite = CItemDataBase.spriteList[craftA.id];
            handling_item.GetComponent<CPickItem>().SetPickItem(craftA.id);
            if (!craft_records[craftA.id])
            {
                craft_records[craftA.id] = true;
                //craftMenu.UpdateMenuInfo(craftA.id);
            }
            if (test) tutorialRequest.DoneCraft();
        }
        else {
            handle.sprite = CItemDataBase.spriteList[items.Length-1];
            handling_item.GetComponent<CPickItem>().SetPickItem(items.Length - 1);
        }
        craftC = null;
    }

    void ChangeSlot(bool clear ,int id) {
        if (clear) {
            slot_img.enabled = false;
            slot_item_img.sprite = null;
        }
        else{
            slot_img.enabled = true;
            slot_item_img.enabled = true;
            if (id < 0) {
                slot_item_img.sprite = CItemDataBase.fail_sprite;
                return;
            } 
            slot_item_img.sprite = CItemDataBase.spriteList[id];
        }

    }

    void ThrowItem() {
        if (!b_handling) return;
        //int throw_way = -1;
        //if (Input.GetKeyDown(KeyCode.W)) throw_way = 0;
        //else if (Input.GetKeyDown(KeyCode.S)) throw_way = 1;
        //else if (Input.GetKeyDown(KeyCode.A)) throw_way = 2;
        //else if (Input.GetKeyDown(KeyCode.D)) throw_way = 3;
        //ArrowEnable(true);
        bool goingThrow = false;
        if (useController)
        {
            Vector3 throw_vec3 = new Vector3(Input.GetAxis(whichPlayer + "RHorizontal"), Input.GetAxis(whichPlayer + "RVertical")).V3NormalizedtoV2();
            if (Mathf.Abs(throw_vec3.x) + Mathf.Abs(throw_vec3.y) > 0.15f)
                throwVec3 = throw_vec3;
            throwDegree = -Mathf.Atan2(throwVec3.x, throwVec3.y) * Mathf.Rad2Deg;
            Arrow.rotation = Quaternion.Euler(0, 0, throwDegree);
            if (Input.GetButton(whichPlayer + "RB")) goingThrow = true;
        }
        else {
            throwVec3 = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).V3NormalizedtoV2();
            throwDegree = -Mathf.Atan2(throwVec3.x, throwVec3.y)* Mathf.Rad2Deg;
            Arrow.rotation = Quaternion.Euler(0,0,throwDegree);
            if (Input.GetMouseButtonDown(0)) goingThrow = true;
        }

        if (goingThrow)
        {
            handling_item.SetActive(true);
            handling_item.GetComponent<CPickItem>().SetInField();
            handling_item.transform.position = this.transform.GetChild(0).position;
            handling_item.GetComponent<CPickItem>().SetThrow(throwVec3, throwSpeed);
            ArrowEnable(false);
            if (can_pick) ChangeSlot(false, picking_item.id);
            else ChangeSlot(true,-1);
            //can_pick = false;
            //picking_item = null;
            handle.enabled = false;
            b_handling = false;
            goingThrow = false;
            if (test) tutorialRequest.DoneThrow();
        }

    }

    //void NearForge() {
    //    ArrowEnable(false);
    //    if (b_handling && craftA.id == 3)forge.CheckForge();  //初步確認目前熔爐內容物並改圖片
    //    if (useController)
    //    {
    //        //往鍛造爐丟材料
    //        if (b_handling && Input.GetButtonDown(whichPlayer + "RB"))
    //        {
    //            if (forge.ThrowIn(craftA.id))
    //            {
    //                handling_item.GetComponent<CPickItem>().SetInFree();
    //                ChangeSlot(true, 0);
    //                picking_item = null;
    //                handle.enabled = false;
    //                b_handling = false;
    //                ArrowEnable(false);
    //            }
    //        }
    //        //鍛造
    //        if (Input.GetButtonDown(whichPlayer + "LB"))
    //        {
    //            forge.OnForging();
    //        }
    //    }
    //    else
    //    {
    //        //往鍛造爐丟材料
    //        if (b_handling && Input.GetMouseButtonDown(0))
    //        {
    //            if (forge.ThrowIn(craftA.id))
    //            {
    //                handling_item.GetComponent<CPickItem>().SetInFree();
    //                ChangeSlot(true, 0);
    //                picking_item = null;
    //                handle.enabled = false;
    //                b_handling = false;
    //                ArrowEnable(false);
    //            }
    //        }
    //        //鍛造
    //        if(Input.GetKeyDown(KeyCode.E)) {
    //            forge.OnForging();
    //        }
    //    }

    //}

    void Collect() {
        if (!can_collect) return;
        if (useController)
        {
            
            if (Input.GetButtonDown(whichPlayer + "LB")) {
                Debug.Log("CONTROLLER" + whichPlayer);
                switchMove(false);
                StartCoroutine("OnCollecting");
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.E)) {
                switchMove(false);
                StartCoroutine("OnCollecting");
            }
        }
    }

    IEnumerator OnCollecting() {
        float time = 0.0f;
        collectBarBk.GetComponent<SpriteRenderer>().enabled = true;
        collectBar.GetComponent<SpriteRenderer>().enabled = true;
        while (time < 1.0f)
        {
            time += Time.deltaTime*1.25f;
            collectBar.localScale = new Vector3(Mathf.Lerp(0.0f, 1.05f, time),0.1f,1.0f);
            yield return null;
        }
        collectBarBk.GetComponent<SpriteRenderer>().enabled = false;
        collectBar.GetComponent<SpriteRenderer>().enabled = false;
        switchMove(true);
        pick_collect.ThrowItemOut();
        can_collect = false;
        pick_collect = null;
        if (test) tutorialRequest.DoneCollect();
    }

    void switchMove(bool enable) {
        if (enable)
        {
            if (Player.p1charaType) Player.p1moveAble = true;
            else Player.p2moveAble = true;
        }
        else {
            if (Player.p1charaType) Player.p1moveAble = false;
            else Player.p2moveAble = false;
        }
        
    }
    public void ThrowOut() {
        handling_item.GetComponent<CPickItem>().SetInFree();
        ChangeSlot(true, 0);
        picking_item = null;
        handle.enabled = false;
        b_handling = false;
        ArrowEnable(false);
    }

    public void ArrowEnable(bool enable) {
        Arrow.GetChild(0).GetComponent<SpriteRenderer>().enabled = enable;
        Arrow.GetChild(1).GetComponent<SpriteRenderer>().enabled = enable;
    }
    public CItem CheckHandle() {
        if (!b_handling) return items[0];
        return craftA;
    }

    public bool CheckIsFree() {
        if (can_pick || can_collect) return false;
        else return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickItem") {
            if (!can_pick && !can_collect && craftFunc) {
                picking_item = collision.gameObject.GetComponent<CPickItem>();
                picking_item.GetComponent<COutLine>().SetOutLine(true);
                if (!b_handling)
                {
                    Debug.Log("going to pick");
                    ChangeSlot(false, picking_item.id);
                }
                else
                {
                    Debug.Log("going to craft");
                    craftB = items[picking_item.id];
                    CheckCrafting();
                }
                can_pick = true;
            }
        }
        else if (!can_collect && collision.tag == "PickCollection" && !can_pick)
        {
            if (!craftFunc) return;
            can_collect = true;
            pick_collect = collision.transform.GetComponent<CPickCollection>();
            pick_collect.GetComponent<COutLine>().SetOutLine(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("collision stay" + collision.name);
        if (collision.tag == "PickItem")
        {
            if (!can_pick && !can_collect && craftFunc)
            {
                Debug.Log("empty");
                picking_item = collision.GetComponent<CPickItem>();
                picking_item.GetComponent<COutLine>().SetOutLine(true);
                if (!b_handling)
                {
                    ChangeSlot(false, picking_item.id);
                }
                else
                {
                    craftB = items[picking_item.id];
                    CheckCrafting();
                }
                can_pick = true;
            }
        }
        else if (!can_collect && collision.tag == "PickCollection" && !can_pick && craftFunc)
        {
            can_collect = true;
            pick_collect = collision.transform.GetComponent<CPickCollection>();
            pick_collect.GetComponent<COutLine>().SetOutLine(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PickItem")
        {
            //if (pick_collect == null) return;
            if (picking_item != null && collision.gameObject == picking_item.gameObject) {
                Debug.Log("the same");
                picking_item.GetComponent<COutLine>().SetOutLine(false);
                picking_item = null;
                ChangeSlot(true,0);
                if (can_pick) can_pick = false;
            }
            //if (can_pick) can_pick = false;
        }
        if (can_collect && collision.tag == "PickCollection" ) {
            Debug.Log("LEAVE COLLECT");
            //if (pick_collect == null) return;
            if (pick_collect != null && collision.gameObject == pick_collect.gameObject) {
                pick_collect.GetComponent<COutLine>().SetOutLine(false);
                can_collect = false;
                pick_collect = null;
            }
            
        }
    }
}
