using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {
    bool setOn;
    float time, lifeTime;
    Vector3 targetDropPos;
    public float posZ;

	// Use this for initialization
	void Awake () {
        lifeTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(Vector3 _dropPos) {
        targetDropPos = _dropPos;
        setOn = true;
    }

}
