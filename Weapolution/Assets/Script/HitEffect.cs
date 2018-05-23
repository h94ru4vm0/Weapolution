using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {
    float aniTime;

    public Sprite[] effectAni;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        aniTime += Time.deltaTime;
	}
}
