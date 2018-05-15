using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoctor : CEnemy {


    public override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        rambleLayer = 1 << LayerMask.NameToLayer("Obstacle")
                        | 1 << LayerMask.NameToLayer("ObstacleForIn");
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePos();
        Ramble();
        transform.localScale = new Vector3(-Mathf.Sign(go_way.x) * scaleX, scaleY, 1);
    }
}
