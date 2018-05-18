using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SprintAttacks : MonoBehaviour {
    int aniID;
    float time, sprintTime;
    Vector3 targetPos, oringinPos;
    Vector3[] sprintWay;
    Transform sprints;
    Transform[] sprint;
    SpriteRenderer[] sprintRender;
    Action<Vector3> callBack;

    public bool startSprint;
    public float speed;
    public Sprite[] AniImg;

	// Use this for initialization
	void Awake () {
         sprints = GameObject.Find("SprintAttacks").transform;
        sprintRender = new SpriteRenderer[3];
        sprint = new Transform[3];
        sprintWay = new Vector3[3];
        for (int i = 0; i < 3; i++) {
            sprint[i] = sprints.GetChild(i);
            sprintRender[i] = sprint[i].GetComponent<SpriteRenderer>();
        }
        sprints.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (startSprint) {
            for (int i = 0; i < 3; i++)
            {
                if (time > 0.1f) {
                    if (i == 3) {
                        aniID++;
                        time = 0.0f;
                    }
                    if(aniID < 5)sprintRender[i].sprite = AniImg[aniID];
                }
                if (i == 1) sprint[1].position = Vector3.Lerp(oringinPos, targetPos, sprintTime);
                sprint[i].position += Time.deltaTime * sprintWay[i] * speed;
            }
            //if (aniID >= 5) {
            //    FinishSprint();
            //}
            time += Time.deltaTime;
            if (sprintTime < 1.0f) sprintTime += Time.deltaTime*2.0f;
            else {
                sprintTime = 1.0f;
                FinishSprint();
            } 
        }
	}

    public void SetSprintWay(Vector3 _playerPos, Action<Vector3> _callBack) {
        Vector3 selfPos = transform.position;
        //Vector3 playerPos = new Vector3(_playerPos.x, _playerPos.y + 1.0f, selfPos.z);
        Vector3 way = (_playerPos - selfPos).V3NormalizedtoV2();
        targetPos = new Vector3(_playerPos.x - way.x*1.0f, _playerPos.y - way.y * 1.0f, selfPos.z);
        sprintWay[0] = Quaternion.AngleAxis(-20.0f, Vector3.forward) * way;
        sprintWay[2] = Quaternion.AngleAxis(20.0f, Vector3.forward) * way;
        oringinPos = sprint[1].position;
        startSprint = true;
        sprints.gameObject.SetActive(true);
        sprintTime = 0.0f;
        time = 0.0f;
        callBack = _callBack;
    }

    void FinishSprint() {
        startSprint = false;
        callBack(targetPos - new Vector3(0.0f, 2.0f, 0.0f));
        for (int i = 0; i < 3; i++)
        {
            sprint[i].position = sprints.position + new Vector3(0.0f, 2.0f, 0.0f);
        }
        aniID = 0;
        sprints.gameObject.SetActive(false);
        
    }

}
