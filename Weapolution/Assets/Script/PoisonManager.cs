using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonManager : MonoBehaviour {
    
    List<Poison> freePoisons, usedPoisons;

    public int freeNum;
    // Use this for initialization
    void Awake () {
        freePoisons = new List<Poison>();
        usedPoisons = new List<Poison>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform temp = transform.GetChild(i);
            freePoisons.Add(temp.GetComponent<Poison>());
            temp.gameObject.SetActive(false);
            freeNum++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition() {

    }
    public void AddUsedPosition(Vector3 pos, Vector3 _targetPos) {
        usedPoisons.Add(freePoisons[0]);
        freePoisons[0].transform.position = pos;
        freePoisons[0].gameObject.SetActive(true);
        freePoisons[0].Init(_targetPos);
        freePoisons.RemoveAt(0);
        freeNum--;
    }
}
