using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonSystem : MonoBehaviour {

    GameObject RightCanon, LeftCanon;
    public GameObject RightAim, LeftAim;
    string whichPlayer = "p1";
    int CanonNum;
    float p1_L_JoyX;
    float p1_L_JoyY;
    float p2_L_JoyX;
    float p2_L_JoyY;
    Canon CanonScript;
    public List<Vector3> DefaultAimPos;
    public List<Vector2> Rayway;
    bool ShowRightAim = false, ShowLeftAim = false;
    float speed = 10f;
    float RayDistant = 1f;
    LayerMask unWalkable;
    RaycastHit2D hitWall1;
    RaycastHit2D hitWall2;
    RaycastHit2D hitWall3;
    RaycastHit2D hitWall4;
    public List<bool> IsHitted;

    private void Awake() 
    {
        RightAim.SetActive(false);
        LeftAim.SetActive(false);
        CanonScript = GameObject.Find("Canon").GetComponent<Canon>();
        RightCanon = GameObject.Find("Canon");
        LeftCanon = GameObject.Find("Canon (1)");
        Rayway[0] = new Vector2(0, 1f); //上下左右
        Rayway[1] = new Vector2(0, -1f);
        Rayway[2] = new Vector2(-1f, 0);
        Rayway[3] = new Vector2(1f, 0);
    }
    private void Start()
    {
        if (Player.p2charaType) whichPlayer = Player.p2joystick;
        else whichPlayer = Player.p1joystick;
        DefaultAimPos[0] = new Vector3(13f, -6f, 0);
        DefaultAimPos[1] = new Vector3(-13f, -6f, 0);
        unWalkable = 1 << LayerMask.NameToLayer("Obstacle") |
                      1 << LayerMask.NameToLayer("ObstacleForIn")|
                       1 << LayerMask.NameToLayer("ObstacleForOut");
    }
    void Update()
    {
        LeftListener();
        if (CanonScript.Canon1isfillingPowder)
        {
            
            if (Input.GetButtonDown(whichPlayer + "ButtonA"))
            {
                ReadyToShoot(true);
            }
            if (ShowRightAim)
            {
                RaycastHitWall();
                if (ShowRightAim) AimControl(RightAim);
                CancelShoot();
             }         
        }
        if (CanonScript.Canon2isfillingPowder)
        {

            if (Input.GetButtonDown(whichPlayer + "ButtonA"))
            {
                ReadyToShoot(false);
            }
            if (ShowLeftAim)
            {
                RaycastHitWall();
                if (ShowLeftAim) AimControl(LeftAim);
                CancelShoot();
            }
        }
    }
    void ReadyToShoot(bool isRightCanon)
    {
        ShowAim(isRightCanon);
        if (Player.p2charaType) Player.p2moveAble = false;
        else Player.p1moveAble = false;
    }
    void CancelShoot()
    {
        if (Input.GetButtonDown(whichPlayer + "ButtonB"))
        {
            if (Player.p2charaType) Player.p2moveAble = true;
            else Player.p1moveAble = true;
            RightAim.SetActive(false);
            LeftAim.SetActive(false);
        }

    }
    void ShowAim(bool isRightCanon)
    {
        if (isRightCanon)
        {
            RightAim.SetActive(true);
            RightAim.transform.position = DefaultAimPos[0];
            ShowRightAim = true;
        }
        else
        {
            LeftAim.SetActive(true);
            LeftAim.transform.position = DefaultAimPos[1];
            ShowLeftAim = true;

        }
    }
    void LeftListener()
    {
        p1_L_JoyX = Input.GetAxis("p1LHorizontal");
        p1_L_JoyY = Input.GetAxis("p1LVertical");
        p2_L_JoyX = Input.GetAxis("p2LHorizontal");
        p2_L_JoyY = Input.GetAxis("p2LVertical");
    }

    void AimControl(GameObject whichAim)
    {
        float speedX = 0, speedY = 0;

        if (Player.p2charaType)
        {
            if (Player.p2controller)
            {
                if (whichPlayer == "p1")
                {
                    if (Mathf.Abs(p1_L_JoyY) > 0.1f) speedY = Mathf.Sign(p1_L_JoyY);
                    if (Mathf.Abs(p1_L_JoyX) > 0.1f) speedX = Mathf.Sign(p1_L_JoyX);
                    if (IsHitted[0] && speedY > 0.0F) speedY = 0.0f;
                    if (IsHitted[1] && speedY < 0.0F) speedY = 0.0f;
                    if (IsHitted[2] && speedX < 0.0F) speedX = 0.0f;
                    if (IsHitted[3] && speedX > 0.0F) speedX = 0.0f;
                }
                else {
                    if (Mathf.Abs(p2_L_JoyY) > 0.1f) speedY = Mathf.Sign(p1_L_JoyY);
                    if (Mathf.Abs(p2_L_JoyX) > 0.1f) speedX = Mathf.Sign(p1_L_JoyX);
                    if (IsHitted[0] && speedY > 0.0F) speedY = 0.0f;
                    if (IsHitted[1] && speedY < 0.0F) speedY = 0.0f;
                    if (IsHitted[2] && speedX < 0.0F) speedX = 0.0f;
                    if (IsHitted[3] && speedX > 0.0F) speedX = 0.0f;
                }
                whichAim.transform.position += Time.deltaTime * speed * new Vector3(speedX, speedY, 0);
            }
            else { }//用鍵盤
        }
        else
        {
            if (Player.p1controller)
            {
                if (whichPlayer == "p1")
                {
                    if (Mathf.Abs(p1_L_JoyY) > 0.1f) speedY = Mathf.Sign(p1_L_JoyY);
                    if (Mathf.Abs(p1_L_JoyX) > 0.1f) speedX = Mathf.Sign(p1_L_JoyX);
                    if (IsHitted[0] && speedY > 0.0F) speedY = 0.0f;
                    if (IsHitted[1] && speedY < 0.0F) speedY = 0.0f;
                    if (IsHitted[2] && speedX < 0.0F) speedX = 0.0f;
                    if (IsHitted[3] && speedX > 0.0F) speedX = 0.0f;
                }
                else
                {
                    if (Mathf.Abs(p2_L_JoyY) > 0.1f) speedY = Mathf.Sign(p1_L_JoyY);
                    if (Mathf.Abs(p2_L_JoyX) > 0.1f) speedX = Mathf.Sign(p1_L_JoyX);
                    if (IsHitted[0] && speedY > 0.0F) speedY = 0.0f;
                    if (IsHitted[1] && speedY < 0.0F) speedY = 0.0f;
                    if (IsHitted[2] && speedX < 0.0F) speedX = 0.0f;
                    if (IsHitted[3] && speedX > 0.0F) speedX = 0.0f;
                }
                whichAim.transform.position += Time.deltaTime * speed * new Vector3(speedX, speedY, 0);
            }
            else { }//用鍵盤
        }
    }

    void RaycastHitWall()
    {

        hitWall1 = Physics2D.Raycast(RightAim.transform.position, Rayway[0],
                                                   RayDistant, unWalkable);
        hitWall2 = Physics2D.Raycast(RightAim.transform.position, Rayway[1],
                            RayDistant, unWalkable);

        hitWall3 = Physics2D.Raycast(RightAim.transform.position, Rayway[2],
                            RayDistant, unWalkable);

        hitWall4 = Physics2D.Raycast(RightAim.transform.position, Rayway[3],
                            RayDistant, unWalkable);
        if (hitWall1.collider != null) IsHitted[0] = true;
        else IsHitted[0] = false;
        if (hitWall2.collider != null) IsHitted[1] = true;
        else IsHitted[1] = false;
        if (hitWall3.collider != null) IsHitted[2] = true;
        else IsHitted[2] = false;
        if (hitWall4.collider != null) IsHitted[3] = true;
        else IsHitted[3] = false;
    }
}
