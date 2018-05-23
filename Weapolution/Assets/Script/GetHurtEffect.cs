﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHurtEffect : MonoBehaviour {
    SpriteRenderer image;
    CChildProjectSystem hitEffects;
	// Use this for initialization
	void Awake () {
        //image = transform.Find("Img").GetComponent<SpriteRenderer>();
        hitEffects = GameObject.Find("HitEffects").GetComponent<CChildProjectSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEffect(Vector3 pos) {
        hitEffects.AddUsed(pos);
    }

    public void SetEffectOn() {
        StopCoroutine(EffectOn());
        StartCoroutine(EffectOn());
    }
    IEnumerator EffectOn()
    {
        image.enabled = false;
        yield return new WaitForSeconds(0.1f);
        image.enabled = true;
        yield return null;
    }
}
