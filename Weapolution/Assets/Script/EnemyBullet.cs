using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    bool startShoot;
    
    Vector3 shootWay;
    EnemyBulletSystem system;

    public float shootSpeed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (startShoot) {
            transform.position += Time.deltaTime* shootSpeed * shootWay;
        }
    }

    public void SetSystem(EnemyBulletSystem _system){
        system = _system;
    }

    public void SetWay(Vector3 _shootWay) {
        shootWay = new Vector3(_shootWay.x, _shootWay.y, 0.0f).V3NormalizedtoV2();
        startShoot = true;
    }

    public  void ResetChild()
    {
        startShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            //Debug.Log("sadasdasdsadadasdsdsad hit player");
            ResetChild();
            system.AddFree(this);
        }
        if (collision.tag == "Wall") {
            //Debug.Log("sadasdasdsadadasdsdsad hit wall");
            ResetChild();
            system.AddFree(this);
        }
    }

}
