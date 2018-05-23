using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : CChildProject {
    int aniID;
    float aniTime, scale;
    SpriteRenderer Img;
    public Sprite[] effectAni;
    // Use this for initialization
    private void Awake()
    {
        scale = transform.localScale.x;
        Img = transform.Find("Img").GetComponent<SpriteRenderer>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        aniTime += Time.deltaTime;
        if (aniTime > 0.07f) {
            aniID++;
            aniTime = 0.0f;
            if (aniID < 4)
            {
                transform.localScale = new Vector3(scale + aniID * 0.2f, scale + aniID * 0.2f,1);
                Img.sprite = effectAni[aniID];
            }
            else {
                Debug.Log("asdadasdadadasdasd" + scale);
                transform.localScale = new Vector3(scale , scale , 1);
                aniID = 0;
                Img.sprite = effectAni[aniID];
                system.AddFree(transform);
            }
            
        }
	}

}
