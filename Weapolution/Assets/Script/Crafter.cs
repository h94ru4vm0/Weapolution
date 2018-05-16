using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour {

    int face_way;
    public int last_way; //上一個面向

    public bool test;
    public int character_num;

    float L_JoyX = 0.0f;
    float L_JoyY = 0.0f;
    float K_JoyX = 0.0f;
    float K_JoyY = 0.0f;
    public float Speed = 5.0f;
    int inFuntionTime = 0;

    public Animator animator;
    SpriteRenderer img;

    public Player PlayerScript;
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("face_way", 5);
        //animator.SetInteger("animation_state", 0);
        PlayerScript = GameObject.Find("character1").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (StageManager.timeUp) return;
        if (PlayerScript.p2_die) return;
        if (Player.p2charaType)
        {
            Movement(Player.p2moveAble, Player.p2controller, Player.p2joystick);
        }
        else
        {
            Movement(Player.p1moveAble, Player.p1controller, Player.p1joystick);
        }
        
        //CheckHp();
    }

    public void Gameover()
    {
        PlayerScript.p2_die = true;
        if (inFuntionTime == 0)
        {
            animator.SetBool("is_die", true);
            Player.p2moveAble = false;
            inFuntionTime++;
        }

    }
    public void GetHurt()
    {
        if (test) return;
        if (PlayerScript.p2_die) return;
        animator.SetTrigger("is_hurt");
        if (Player.p2charaType) Player.p2moveAble = false;
        else Player.p1moveAble = false;
        TeamHp.teamHp -= 0.05f;
        GameObject.Find("MonsterAudio").GetComponent<MonsterVoice>().SetAudio(2,1f);
        GameObject.Find("CharacterAudio").GetComponent<CharacterVoice>().SetAudio(1);
    }
    public void OverBeHurt()
    {
        if (Player.p2charaType) Player.p2moveAble = true;
        else Player.p1moveAble = true;
    }
    void Movement(bool move, bool ctrlmode, string Joystick_num)
    {
        if (move)
        {

            if (!ctrlmode) //鍵盤
            {
                if (K_JoyX == 0 && K_JoyY == 0)
                {
                    animator.SetBool("is_walk", false);
                    if (Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position += Time.deltaTime * new Vector3(Speed, 0, 0);
                        animator.SetInteger("face_way", 3);
                        face_way = 3;
                        K_JoyX = Speed;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position -= Time.deltaTime * new Vector3(Speed, 0, 0);
                        animator.SetInteger("face_way", 2);
                        face_way = 2;
                        K_JoyX = -1 * Speed;
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position += Time.deltaTime * new Vector3(0, Speed, 0);
                        animator.SetInteger("face_way", 0);
                        face_way = 0;
                        K_JoyY = Speed;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position -= Time.deltaTime * new Vector3(0, Speed, 0);
                        animator.SetInteger("face_way", 1);
                        face_way = 1;
                        K_JoyY = -1 * Speed;
                    }
                    if (last_way != face_way)
                    {
                        animator.SetTrigger("change_face");
                    }
                    last_way = face_way;
                    //else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                    //{
                    //    animator.SetBool("is_walk", false);
                    //    K_JoyY = 0;
                    //    K_JoyX = 0;
                    //}
                }
                else if (K_JoyX != 0 || K_JoyY != 0) //至少按著一個鍵
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position += Time.deltaTime * new Vector3(Speed, 0, 0);
                        //face_way = 3;
                        K_JoyX = Speed;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position -= Time.deltaTime * new Vector3(Speed, 0, 0);
                        //face_way = 2;
                        K_JoyX = -1 * Speed;
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position += Time.deltaTime * new Vector3(0, Speed, 0);
                        //face_way = 0;
                        K_JoyY = Speed;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        animator.SetBool("is_walk", true);
                        transform.position -= Time.deltaTime * new Vector3(0, Speed, 0);
                        //face_way = 1;
                        K_JoyY = -1 * Speed;
                    }
                    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                    {
                        K_JoyY = 0;
                        K_JoyX = 0;
                    }

                }


            }
            else //搖桿
            {

                L_JoyX = Input.GetAxis(Joystick_num + "LHorizontal");
                L_JoyY = Input.GetAxis(Joystick_num + "LVertical");

                if (Mathf.Abs(L_JoyX) <= 0.1f && Mathf.Abs(L_JoyY) <= 0.1f) //idle
                {
                    animator.SetBool("is_walk", false);
                }
                #region 可斜走
                else
                {
                    if (Mathf.Abs(L_JoyX) > Mathf.Abs(L_JoyY))
                    {
                        animator.SetBool("is_walk", true);

                        if (L_JoyX > 0)
                        {
                            animator.SetInteger("face_way", 3);
                            transform.position += Time.deltaTime * new Vector3(L_JoyX, 0, 0) * Speed;
                            face_way = 3;
                        }
                        else
                        {
                            animator.SetInteger("face_way", 2);
                            transform.position += Time.deltaTime * new Vector3(L_JoyX, 0, 0) * Speed;
                            face_way = 2;
                        }
                        if (Mathf.Abs(L_JoyY) >= 0.2f) transform.position += Time.deltaTime * new Vector3(0, L_JoyY, 0) * Speed;
                        if (last_way != face_way)
                        {
                            animator.SetTrigger("change_face");
                        }
                        last_way = face_way;
                        //else if (Mathf.Abs(L_JoyY) >= 0.2f) transform.position -= Time.deltaTime * new Vector3(0, L_JoyY, 0) * Speed;
                    }
                    else if (Mathf.Abs(L_JoyX) < Mathf.Abs(L_JoyY))
                    {
                        animator.SetBool("is_walk", true);
                        if (L_JoyY > 0)
                        {
                            animator.SetInteger("face_way", 0);
                            transform.position += Time.deltaTime * new Vector3(0, L_JoyY, 0) * Speed;
                            face_way = 0;
                        }
                        else
                        {
                            animator.SetInteger("face_way", 1);
                            transform.position += Time.deltaTime * new Vector3(0, L_JoyY, 0) * Speed;
                            face_way = 1;
                        }
                        if (Mathf.Abs(L_JoyX) >= 0.2f) transform.position += Time.deltaTime * new Vector3(L_JoyX, 0, 0) * Speed;
                        if (last_way != face_way)
                        {
                            animator.SetTrigger("change_face");
                        }
                        last_way = face_way;
                        //else if(Mathf.Abs(L_JoyX) >= 0.2f)transform.position += Time.deltaTime * new Vector3(L_JoyX, 0, 0) * Speed;
                    }
                }



            }
            #endregion
            #region 不可斜走  
            //else if (Mathf.Abs(L_JoyX) > Mathf.Abs(L_JoyY))
            //{
            //    if (L_JoyX > 0)
            //    {
            //        animator.SetInteger("face_way", 3);
            //    }
            //    else
            //    {
            //        animator.SetInteger("face_way", 2);
            //    }
            //    transform.position += Time.deltaTime * new Vector3(L_JoyX * Speed, 0, 0);
            //}
            //else if (Mathf.Abs(L_JoyX) < Mathf.Abs(L_JoyY))
            //{
            //    if (L_JoyY > 0)
            //    {
            //        animator.SetInteger("face_way", 0);
            //    }
            //    else
            //    {
            //        animator.SetInteger("face_way", 1);
            //    }
            //    transform.position += Time.deltaTime * new Vector3(0, L_JoyY * Speed, 0);
            //}
            #endregion
        }



    }

   
}
