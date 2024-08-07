using MyProject.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.State
{

    public class MoveState : BaseState
    {
        public override void Enter()
        {
            player.stateStay = 0;
        }

        public override void Update()
        {
            player.text.text = $"MoveDistance:{player.moveDistance:n1}";
            player.moveDistance += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log($"{GetType().Name} Á¾·á");
        }
    }
}
