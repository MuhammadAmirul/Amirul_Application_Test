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
        m_aiController.Agent.speed = 0f; // Set the agent speed to 0.
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Change the AI state to moving state once the start race button is pressed and lastLap is false.
        if (m_aiController.GreenGameManager.StartRace && !lastLap)
        {
            m_aiController.AIStateMachine.ChangeState(m_aiController.AIMovingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        m_aiController.Animator.SetTrigger("isMoving"); // Set the animation trigger to isMoving to play the running animation of the AI.
    }
}
