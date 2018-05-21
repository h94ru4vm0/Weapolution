using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trape : CChildProject
{

    bool isForOut = true;
    float time;
    bool damageOnce;

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(bool _isOut)
    {
        isForOut = _isOut;
    }

    public bool IsOut() {
        return isForOut;
    }

    public override void SetOn(bool _isOuT, Action _callBack)
    {
        Debug.Log("set is out");
        isForOut = _isOuT;
        callBack = _callBack;
    }

    public override void ResetChild()
    {
        time = 0.0f;
        damageOnce = false;
        system.AddFree(this.transform);
        callBack();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //}

}
