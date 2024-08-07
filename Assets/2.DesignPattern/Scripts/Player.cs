using MyProject.Skill;
using MyProject.State;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ���� ���� ������ ���� ���� �����̴� ������ �ƴϸ� �����ִ��� �Ǵ��ϰ� ����.
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

    public State currentState; // 0: idle, 1: move ���� Ȯ���� �Ǹ� 2�� ���� 3�̸� ���� ���..
    public float stateStay; // ���� ���¿� �ӹ� �ð�.
    public float moveDistance; // ���� �̵��Ÿ�.

    public Transform shotPoint; // ��ų ������ ����ü�� ������ �ڸ�
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

    //���� ����
    public void ChangeState/*Transition*/(State nextState)
    {
        if (currentState != nextState)
        {
            //exit
            switch (currentState)
            {
                case State.Idle:
                    print("������ ����");

                    break;
                case State.Move:
                    print("�̵����� ����");
                    break;
            }

            //enter
            switch (nextState)
            {
                case State.Idle:
                    print("������ ����");
                    break;
                case State.Move:
                    print("�̵����� ����");
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
                // ���� ���°� Idle�̸�
                text.text = $"{State.Idle} state : {stateStay.ToString("n0")}";

                break;
            case State.Move:
                // ���� ���°� Move�̸�
                text.text = $"{State.Move} state : {stateStay:n0}"; // n0�� �Ҽ��� ���ϸ� ����.


                break;

        }
       

    }
}
