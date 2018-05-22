using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonSystem : MonoBehaviour {

    GameObject RightCanon, LeftCanon;
    private void Awake()
    {
        RightCanon = GameObject.Find("Canon");
        LeftCanon = GameObject.Find("Canon (1)");
    }
    void Update()
    {

    }
}
