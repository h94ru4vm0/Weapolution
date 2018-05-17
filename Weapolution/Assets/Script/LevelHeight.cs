using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeight : MonoBehaviour {
    MapInfo.Area[] areaHeight;
    ZArrange zArrange;
	// Use this for initialization
	void Awake () {
        zArrange = GetComponent<ZArrange>();
        areaHeight = GameObject.Find("map").GetComponent<MapInfo>().heightArea;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = transform.position;
        if (pos.x >= areaHeight[0].width.x && pos.x <= areaHeight[0].width.x &&
            pos.y >= areaHeight[0].height.x && pos.y <= areaHeight[0].height.y) {
            if (pos.x >= areaHeight[1].width.x && pos.x <= areaHeight[1].width.y &&
                pos.y >= areaHeight[1].height.x && pos.y >= areaHeight[1].height.y)
            {
                zArrange.f_base = -100.0f;
            }
            else
            {
                if (pos.x >= areaHeight[2].width.x && pos.x <= areaHeight[2].width.y &&
                pos.y >= areaHeight[2].height.x && pos.y >= areaHeight[2].height.y)
                {
                    zArrange.f_base = 0.0f;
                }
                else {
                    zArrange.f_base = 100.0f;
                }
            }
        }
	}
}
