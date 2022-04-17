using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amirul.AI.StateMachine;

public class AIIdleState : AIState
{
    public AIIdleState(AIController aiController, StateMachine aiStateMachine) : base(aiController, aiStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        m_aiController.Agent.speed = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (m_aiController.GreenGameManager.StartRace)
        {
            m_aiController._AIStateMachine.ChangeState(m_aiController._AIMovingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        m_aiController.Animator.SetTrigger("isMoving");
    }
}
