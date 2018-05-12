﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dialog : MonoBehaviour {

    int dialogLine;
    string[] currentDialog;
    Text content;
    Action callTrans;

    public string[] winDialog;
    public string[] loseDialog;

    private void Awake()
    {
        content = transform.Find("Content").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        GetInput();
	}

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("p1ButtonA")
              || Input.GetButtonDown("p2ButtonA"))
        {
            if (dialogLine < currentDialog.Length - 1)
            {
                dialogLine++;
                content.text = currentDialog[dialogLine];
            }
            else {
                dialogLine = 0;
                callTrans();
            }

        }
    }

    public void SetDialogOn(bool _isWin, Action callback) {
        if (_isWin) currentDialog = winDialog;
        else currentDialog = loseDialog;
        dialogLine = 0;
        content.text = currentDialog[dialogLine];
        callTrans = callback;
    }

}