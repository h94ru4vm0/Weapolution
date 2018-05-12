using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySystem : MonoBehaviour {
    int enemyNumber, deathNumber = 0;
    Transform freeEnemyIn, UsedEnemyIn;    
    //一維為左右，二維是可不可用, true為右
    bool[]canAttackLoc = new bool[2] {  true, true };
    public bool test, bossAppear;
    public float spawnLocY;
    //public Vector2 InsidefieldMinLimit, InsidefieldMaxLimit;
    public GameObject Inplayer, Outplayer;
    //public List<Transform> InEnemys, OutEnemys;
    //public GameObject forge;
    CEnemy boss;
    //public CPickItemSystem pickItemSystem;
    
    // Use this for initialization
    void Awake()
    {
        enemyNumber = 0;
        Inplayer = GameObject.Find("character1");
        Outplayer = GameObject.Find("character2");
        freeEnemyIn = transform.GetChild(0).Find("InsideFree");
        UsedEnemyIn = transform.GetChild(0).Find("InsideUsed");
        //pickItemSystem = GameObject.Find("PickItemSystem").GetComponent<CPickItemSystem>();
    }
    private void Start()
    {
        CEnemy enemy;
        //boss獨立給玩家參照
        boss = transform.GetChild(0).GetChild(2).GetComponent<CEnemy>();
        boss.player = Inplayer;
        boss.SetEnemySystem(this);
        //所有敵人分別給內外玩家參照
        for (int i = 0; i < freeEnemyIn.childCount; i++)
        {
            enemy =  freeEnemyIn.GetChild(i).GetComponent<CEnemy>();
           enemy.player = Inplayer;
            enemy.SetEnemySystem(this);
           // if (i == 1) InEnemys[i].gameObject.SetActive(true);
            //InEnemys[i].GetComponent<CEnemy>().attackDetect += Inplayer.OnHurt;
        }
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            enemy = (transform.GetChild(1).GetChild(i)).GetComponent<CEnemy>();
            //OutEnemys.Add(enemy.transform);
            enemy.IsOut = true;
            enemy.player = Outplayer;
            enemy.SetEnemySystem(this);
            //OutEnemys[i].GetComponent<CEnemy>().player = Outplayer.gameObject;
            // if (i == 1) OutEnemys[i].gameObject.SetActive(true);
            //InEnemys[i].GetComponent<CEnemy>().attackDetect += Outplayer.OnHurt;
        }
        if (test) return;
        AddUsedList(new Vector3(-5.8f,spawnLocY,0));
        AddUsedList(new Vector3(6.6f, spawnLocY, 0));
        AddUsedList(new Vector3(-0.5f, spawnLocY, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (test) return;
        if (deathNumber <= 2)
        {
            if (enemyNumber < 3) AddUsedList(new Vector3(Random.Range(-7.0f, 8.0f), spawnLocY, 0));
        }
        else if (deathNumber >= 5) {
            if (!bossAppear) {
                bossAppear = true;
                boss.gameObject.SetActive(true);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            boss.gameObject.SetActive(true);
            //forge.GetComponent<CForge>().ShowUp();
        } 
    }

    public void  AddFreeList(Transform trans)
    {
        Debug.Log("recycle enemy" + enemyNumber);
        CEnemy temp = trans.GetComponent<CEnemy>();
        float side = temp.whichSide;
        trans.parent = freeEnemyIn;
        trans.gameObject.SetActive(false);
        enemyNumber--;
        deathNumber++;
        if (Mathf.Abs(side) > 0.5f) {    //如果回收的敵人有占住一個攻擊位置，把位置給其他人
            if (side >0.1f) canAttackLoc[1] = true;
            else if (side <-0.1f) canAttackLoc[0] = true;
            for (int i = 0; i < UsedEnemyIn.childCount; i++)
            {
                temp = UsedEnemyIn.GetChild(i).GetComponent<CEnemy>();
                if (Mathf.Abs(temp.whichSide) < 0.2f)
                {
                    Debug.Log("get position");
                    if (canAttackLoc[0])
                    {
                        Debug.Log("left");
                        temp.whichSide = -1.0f;
                        canAttackLoc[0] = false;
                    }
                    else if (canAttackLoc[1])
                    {
                        Debug.Log("right");
                        temp.whichSide = 1.0f;
                        canAttackLoc[1] = false;
                    }
                }
            }
        }
    }

    public void AddUsedList(Vector3 pos)
    {
        Debug.Log("ememy respawn");
        Transform temp = freeEnemyIn.GetChild(0);
        temp.parent = UsedEnemyIn;
        temp.position = pos;
        temp.gameObject.SetActive(true);
        enemyNumber++;
        if (canAttackLoc[0]) {
            temp.GetComponent<CEnemy>().whichSide = -1.0f;
            canAttackLoc[0] = false;
        }
        else if (canAttackLoc[1]) {
            temp.GetComponent<CEnemy>().whichSide = 1.0f;
            canAttackLoc[1] = false;
        } 
        else temp.GetComponent<CEnemy>().whichSide = 0.0f;
    }

    public int GetDeathNumber() {
        return deathNumber;
    }
}
