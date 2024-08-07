using MyProject.Skill;
using MyProject.State;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 상태 패턴 구현을 위해 현재 움직이는 중인지 아니면 멈춰있는지 판단하고 싶음.
public class Player : MonoBehaviour
{

    public enum State

    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Attack = 3,
    }

    private CharacterController cc;
    public float moveSpeed = 10;
    public TextMeshPro text;

    public State currentState; // 0: idle, 1: move 추후 확장이 되면 2면 공격 3이면 점프 등등..
    public float stateStay; // 현재 상태에 머문 시간.
    public float moveDistance; // 누적 이동거리.

    public Transform shotPoint; // 스킬 시전시 투사체가 생성될 자리
    private SkillContext skillContext;
    private StateMachine stateMachine;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        stateMachine = GetComponent<StateMachine>();
        skillContext = GetComponentInChildren<SkillContext>();

        SkillBehaviour[] skills = skillContext.GetComponentsInChildren<SkillBehaviour>();

        foreach (var sk in skills)
        {
            skillContext.Addskill(sk);
        }
        skillContext.SetCurrentSkill(0);

    }

    private void Start()
    {
        currentState = State.Idle;
    }
    private void Update()
    {
        Move();
       // StateUpdate();
        if (Input.GetButtonDown("Fire1"))
        {
            skillContext.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillContext.SetCurrentSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillContext.SetCurrentSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillContext.SetCurrentSkill(2);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skillContext.SetCurrentSkill(3);
        }


    }


    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0, z);

        cc.Move(moveDirection * moveSpeed * Time.deltaTime);

        /* if (moveDirection.magnitude < 0.1f)
         {
             ChangeState(State.Idle);
         }
         else
         {
             ChangeState(State.Move);
         }*/
        if (moveDirection.magnitude < 0.1f) stateMachine.Transition(stateMachine.idleState);
        else stateMachine.Transition(stateMachine.moveState);
    }

    //상태 전이
    public void ChangeState/*Transition*/(State nextState)
    {
        if (currentState != nextState)
        {
            //exit
            switch (currentState)
            {
                case State.Idle:
                    print("대기상태 종료");

                    break;
                case State.Move:
                    print("이동상태 종료");
                    break;
            }

            //enter
            switch (nextState)
            {
                case State.Idle:
                    print("대기상태 시작");
                    break;
                case State.Move:
                    print("이동상태 시작");
                    break;

            }
            currentState = nextState;
            stateStay = 0;

        }

    }
    public void StateUpdate()
    {


        switch (currentState)
        {
            case State.Idle:
                // 현재 상태가 Idle이면
                text.text = $"{State.Idle} state : {stateStay.ToString("n0")}";

                break;
            case State.Move:
                // 현재 상태가 Move이면
                text.text = $"{State.Move} state : {stateStay:n0}"; // n0는 소수점 이하를 버림.


                break;

        }
       

    }
}
