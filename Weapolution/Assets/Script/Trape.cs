using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trape : CChildProject
{

    bool isForOut;
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

    public override void ResetChild()
    {
        time = 0.0f;
        damageOnce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy") {
            ResetChild();
            system.AddFree(this.transform);
        }
    }

}
