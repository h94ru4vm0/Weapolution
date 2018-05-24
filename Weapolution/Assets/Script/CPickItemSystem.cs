﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPickItemSystem : MonoBehaviour {
    //public List<GameObject> free_list = new List<GameObject>();
    //public List<GameObject> used_list = new List<GameObject>();
    int free_num, used_num, locationID = 0;
    
    int totalNum;
    float spawnTime = 0.0f;
    CPickCollection fireTree;
    public bool test = false;
    public Transform usedCollectList, freeCollectList;
    public List<CPickItem> freePickItemList = new List<CPickItem>(), 
                                    usedPickItemList = new List<CPickItem>();
    public int[] typeCollectNum;
    public float[] typeOppunity;
    public Vector3[] locations;
    public string stage;
    void Awake () {
        CItemDataBase.SetItemDataBase(stage);
        CItemDataBase.SetSpriteList(stage);
        Transform tempFree = transform.GetChild(0);
        //Transform tempUsed = transform.GetChild(1);
        freeCollectList = transform.GetChild(2);
        usedCollectList = transform.GetChild(3);
        used_num = 0;
        free_num = tempFree.childCount;
        for (int i = 0; i < free_num; i++) {
            freePickItemList.Add(tempFree.GetChild(i).GetComponent<CPickItem>());
            freePickItemList[i].GetComponent<CPickItem>().SetSystem(this);
            freePickItemList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < freeCollectList.childCount; i++)
        {
            freeCollectList.GetChild(i).GetComponent<CPickCollection>().pickitem_system = this;
            freeCollectList.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //SpawnInUsed(new Vector3(-7, 0, 0), 5);
        //SpawnInUsed(new Vector3(-10, -6, 0), 5);
        SpawnInUsed(new Vector3(-7, 3, 0), 6);
        SpawnInUsed(new Vector3(-5, 3, 0), 7);
        SpawnInUsed(new Vector3(-4, 3, 0), 8);
        SpawnInUsed(new Vector3(-2, -6, 0), 9);
        SpawnInUsed(new Vector3(0, -6, 0), 10);
        SpawnInUsed(new Vector3(3, -6, 0), 7);
        //SpawnInUsed(new Vector3(-5, 0, 0), 5);
        //SpawnInUsed(new Vector3(-5, 0.7f, 0), 5);
        // SpawnInUsed(new Vector3(-3, 0.3f, 0), 5);
        // SpawnInUsed(new Vector3(-3, 0.7f, 0), 1);
        // SpawnInUsed(new Vector3(-3, 0, 0), 1);
        //if (test) {
        //    SpawnInUsed(new Vector3(-14, -8, 0), 1);
        //    SpawnInUsed(new Vector3(6, 2, 0), 5);
        //} 
        if (!test)
        {
            SpawnPickCollect(locations[0], 0, 1);
            SpawnPickCollect(locations[4], 0, 1);
            SpawnPickCollect(locations[7], 1, 2);
            SpawnPickCollect(locations[5], 1, 2);
        }
    }

    // Update is called once per frame
    void Update () {
        if (test) return;
        if (Input.GetKeyDown(KeyCode.L)) SetThunderLoc() ;
        if (Input.GetKeyDown(KeyCode.H)) SpawnPickCollectInMap();
        spawnTime += Time.deltaTime;
        if (spawnTime > 12.0f) {
            spawnTime = 0.0f;
            SpawnPickCollectInMap();
        }
	}

    void SpawnPickCollectInMap() {
        if (totalNum >= locations.Length) return;
        float opturity = Random.Range(0.0f, 1.0f);
        //Debug.Log(tree_num + "   " + rock_num +"   " + mush_num + "   " + locations.Length);

        for (int i = 0; i < typeOppunity.Length; i++) {
            if (opturity <= typeOppunity[i])
            {
                if (i < 1)
                {
                    if (typeCollectNum[i] < 5 && CheckPickCollectLoc()) SpawnPickCollect(locations[locationID], i, 1);
                }
                else {
                    if (typeCollectNum[i] < 3 && CheckPickCollectLoc()) SpawnPickCollect(locations[locationID], i, 2);
                }
                
            }
        }
        //else if (opturity <= 1.0f)
        //{
        //    if (tree_num < 5 && CheckPickCollectLoc()) SpawnPickCollect(locations[locationID], 1, 10);
        //}

    }


    bool CheckPickCollectLoc()
    {
        Collider2D detect;
        locationID = Random.Range(0, locations.Length - 1);
        for (int i = 0; i < locations.Length; i++)
        {
            locationID += i;
            locationID = locationID % (locations.Length);
            detect = Physics2D.OverlapCircle(locations[locationID], 0.5f);
            if (detect == null)
            {
                return true;
            }
            //else Debug.Log(locations[i]);
        }
        return false;
    }



    public CPickItem SpawnInUsed(Vector3 pos, int id) {
        
        if (free_num <= 0) return null;
        freePickItemList[0].gameObject.SetActive(true);
        freePickItemList[0].SetPickItem(id);
        freePickItemList[0].SetShadowEnable(true);
        freePickItemList[0].transform.position = pos;
        freePickItemList[0].transform.parent = transform.GetChild(1);
        usedPickItemList.Add(freePickItemList[0]);
        freePickItemList.RemoveAt(0);     
        free_num--;
        used_num++;
        //Debug.Log(usedPickItemList[usedPickItemList.Count - 1].name);
        return usedPickItemList[usedPickItemList.Count - 1];
    }

    //public void RecyleUsedList(GameObject pickItem) {
    //    pickItem.transform.parent = usedPickItemList;
    //}

    public void AddFreeList(CPickItem pickItem) {
        pickItem.transform.position = transform.GetChild(0).position;
        pickItem.transform.parent = transform.GetChild(0);
        freePickItemList.Add(pickItem);
        usedPickItemList.Remove(pickItem);
        free_num++;
        used_num--;
    }

    public void SpawnPickCollect(Vector3 pos, int _type, int _itemID)
    {
        Transform spawned;
        spawned = freeCollectList.GetChild(0);
        spawned.parent = usedCollectList;
        spawned.gameObject.SetActive(true);
        spawned.position = pos;
        spawned.GetComponent<CPickCollection>().InitCollects(_type, _itemID);
        for (int i = 0; i < typeCollectNum.Length; i++) {
            if (_type <= i) {
                typeCollectNum[i]++;
                break;
            } 
        }
        totalNum++;
    }
    public void RecyclePickCollect(GameObject _pickCollect) {
        _pickCollect.GetComponent<COutLine>().SetOutLine(false);
        _pickCollect.transform.position = freeCollectList.position;
        _pickCollect.transform.parent = freeCollectList;
        for (int i = 0; i < typeCollectNum.Length; i++)
        {
            if (_pickCollect.GetComponent<CPickCollection>().GetCollectType() <= i) {
                typeCollectNum[i]--;
                break;
            }
        }
        totalNum--;
        //_pickCollect.gameObject.SetActive(false);
        
        Debug.Log("recycle" + _pickCollect.GetComponent<CPickCollection>().GetCollectType());
    }

    public Vector3 SetThunderLoc() {
        for (int i = 0; i < usedCollectList.childCount; i++)
        {
            CPickCollection pickCollection = usedCollectList.GetChild(i).GetComponent<CPickCollection>();
            if (pickCollection.CanOnFire() )//&& Random.Range(0.0f, 1.0f) > 0.85f)
            {
                fireTree = pickCollection;
                fireTree.StartFire();
                return pickCollection.transform.position;
            }
        }
        return new Vector3(-60.0f,0.0f,0.0f);
    }

}
