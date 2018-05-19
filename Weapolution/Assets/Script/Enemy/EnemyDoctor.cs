using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoctor : CEnemy
{
    bool pathFind, onAttacking;
    int pathIndex, normalAtkNum, totalAtkNum;
    float totalTraceTime, breakTime;
    SpriteRenderer render;
    SprintAttacks sprintAtks;
    Path path;

    public float stoppingDis, turnDis, turnSpeed;
    public LayerMask obstacleMask;

    public override void Awake()
    {
        attackOnce = false;
        base.Awake();
        SetState(0,false);
        hp = 15;
        sprintAtks = GetComponent<SprintAttacks>();
        render = transform.Find("img").GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        rambleLayer = 1 << LayerMask.NameToLayer("Obstacle")
                        | 1 << LayerMask.NameToLayer("ObstacleForIn");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
        StateMachine();
        //Ramble();
        //transform.localScale = new Vector3(-Mathf.Sign(go_way.x) * scaleX, scaleY, 1);
        SetAnimator();
    }

    public override void SetState(int num, bool _isForce)
    {
        //Debug.Log("setstate pre" + state);
        //totalTraceTime = 0.0f;
        inState_time = 0.0f;
        state_time = 0.0f;
        attackOnce = false;
        if (num >= 0) state = num;
        else Behavior();
        //traceAgain = false;
        //Debug.Log("setstate after" + state);
    }

    void Behavior() //攻擊行為，用亂數決定攻擊方式
    { 
        if (totalAtkNum > 6)
        {
            totalAtkNum = 0;
            if (CouculatePlayerDis(false, 2.0f)) state = 2;
            else state = 1;
        }
        else
        {
            if (normalAtkNum >= 3)
            {
                normalAtkNum = 0;
                state = 3;
            }
            else {
                if (CouculatePlayerDis(false, 2.0f)) state = 2;
                else state = 1;
            } 
        }
    }

    public override void StateMachine()
    {
        
        switch (state) {
            case 0:
                Idle(breakTime);
                break;
            case 1:
                Trace();
                break;
            case 2:
                NormalAtk();
                break;
            case 3:
                SprintAtk();
                break;
            
        }
    }

    public  void Idle(float _idleTime)
    {
        //Debug.Log("idle" + state_time);
        inState_time += Time.deltaTime;
        if (state_time < 0.1f) state_time = _idleTime;
        if (inState_time > state_time) {
            animator.SetTrigger("nextState");
            SetState(-1, false);
        }
        //if (inState_time > state_time) SetState(-1);
    }

    public override void Trace()
    {
        if (totalTraceTime > 5.0f) {

        }
        if (state_time < 0.1f)
        {
            go_way = Vector3.zero;
            state_time = 5.0f;
            //pathIndex = 0;
            pathFind = false;
            traceTime = 0.0f;
            PathRequestManager.RequestPath(self_pos, playerPos, OnPathFound);
        }
        if (inState_time - traceTime > 0.5f)
        {
            traceTime = inState_time;
            //pathIndex = 0;
            pathFind = false;
            PathRequestManager.RequestPath(self_pos, playerPos, OnPathFound);
        }
        if (pathFind)
        {
            Vector2 Pos2d = new Vector2(self_pos.x, self_pos.y);
            if (path.turnBoundaries[pathIndex].HasCrossedLine(Pos2d))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    //Debug.Log("catch player  " + path.finishLineIndex);
                    pathFind = false;
                }
                else
                {
                    pathIndex++;
                }
            }
            if (pathIndex >= path.slowDownIndex && stoppingDis > 0)
            {
                if (CouculatePlayerDis(false, stoppingDis))
                {
                    //Debug.Log("atkatkatkatkatktaktaktaktktaktakatkatkatkataktak ");
                    animator.SetTrigger("nextState");
                    //onAttacking = true;
                    SetState(2, false);
                    totalTraceTime = 0.0f;
                    //normalAtkNum++;
                    //totalAtkNum++;
                }
            }
            if (pathIndex == 0) go_way = (path.lookPoints[0] - self_pos).V3NormalizedtoV2();
            else
            {
                Vector3 nextWay = (path.lookPoints[pathIndex] - self_pos).V3NormalizedtoV2();
                go_way = Vector3.Lerp(go_way, nextWay, Time.deltaTime * turnSpeed);
            }
            transform.position += go_way * f_speed * 1.7f * Time.deltaTime;
        }
        else {
            if (CouculatePlayerDis(false, stoppingDis))
            {
                animator.SetTrigger("nextState");
                SetState(2, false);
                totalTraceTime = 0.0f;
            }
            go_way = (playerPos - self_pos).V3NormalizedtoV2();
            transform.position += go_way * f_speed * 1.7f * Time.deltaTime;
        }
        inState_time += Time.deltaTime;
        totalTraceTime += Time.deltaTime;
    }

    void NormalAtk() {
//Debug.Log("inATTack");
        if (state_time < 0.1f) {
            state_time = 1.0f;
            onAttacking = true;
            normalAtkNum++;
            totalAtkNum++;
        }
    }

    public override void AttackOver() {
        animator.SetTrigger("nextState");
        onAttacking = false;
        SetState(-1, true);
    }

    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful)
    {
        //transform.Translate(Vector3.forward,Space.Self);
        if (pathSuccessful)
        {
            pathIndex = 0;
            Debug.Log(gameObject.name + "find path");
            pathFind = pathSuccessful;
            path = new Path(wayPoints, self_pos, turnDis, stoppingDis);
        }
    }

    public override bool CouculatePlayerDis(bool more, float compare)
    {
        Vector2 _self_pos = new Vector2(self_pos.x, self_pos.y);
        //Vector2 attack_pos = new Vector2(attackPos.x, attackPos.y);
        Vector2 player_pos = new Vector2(playerPos.x, playerPos.y);
        if (more)
        {
            Vector2 dst = _self_pos - player_pos;
            if (Vector2.SqrMagnitude(dst) > compare * compare) return true;
            else return false;
        }
        else
        {
            Vector2 dst = _self_pos - player_pos;
            if (Vector2.SqrMagnitude(dst) < compare * compare) return true;
            else return false;
        }
    }

    public void StartSprint() {
        render.enabled = false;
        sprintAtks.SetSprintWay(playerPos, SprintAtkOver);
    } 

    void SprintAtk() {
        //Debug.Log("sprintattack");
        if (state_time < 0.1f) {
            onAttacking = true;
            state_time = 2.0f;
            totalAtkNum++;
            //Vector3 sprintWay = (playerPos - self_pos).V3NormalizedtoV2();
            
        }
    }

     void SprintAtkOver(Vector2 pos) {
        animator.SetTrigger("nextState");
        //breakTime = 1.5f;
        //onAttacking = false;
        //SetState(0, false);
        transform.position = new Vector3(pos.x, pos.y, self_pos.z);
        render.enabled = true;
    }

    public void SprintOver() {
        Debug.Log("sprint over");
        breakTime = 1.5f;
        onAttacking = false;
        SetState(0, false);
    }

    public override void SetHurtValue(int _value, int _HitDir)
    {
        //getHurtEffect.SetEffectOn();
        hurtValue = _value;
        hp -= hurtValue;
        if (hp <= 0)
        {
            //animator.Play("EnemyBearDie");
            SetState(5, true);
        }
    }

    public override void SetAnimator()
    {
        //Debug.Log("SetAnimator" + state + "   " + lastState);
        if (!onAttacking)
        {
            if (playerPos.x > transform.position.x)
                transform.localScale = new Vector3(-scaleX, 1, 1);
            else transform.localScale = new Vector3(scaleX, 1, 1);
        }
        if (state != lastState) {
            animator.SetInteger("state", state);
            lastState = state;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !attackOnce) {
            attackOnce = true;
            Debug.Log("boss hit player");
        }
    }

}