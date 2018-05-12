using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChildProject : MonoBehaviour {

    public bool bananaFly;
    public CChildProjectSystem system;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetOn(int _type) {
        switch (_type) {
            case 0:
                bananaFly = true;
                break;
            case 1:
                break;
        }
    }

    public virtual void ResetChild() {

    }

}
