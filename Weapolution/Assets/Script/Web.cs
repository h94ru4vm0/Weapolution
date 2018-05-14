using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : CChildProject
{
    bool damageOnce;
    float lifeTime, time;
	// Use this for initialization
	void Start () {
        lifeTime = 15.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (time < lifeTime) time += Time.deltaTime;
        else
        {
            ResetChild();
            system.AddFree(this.transform);
        }
    }

    public override void ResetChild()
    {
        time = 0.0f;
        damageOnce = false;
    }

}
