using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amirul.AI.StateMachine;

public class AIPausedState : AIState
{
    float pauseTime;

    public AIPausedState(AIController aiController, StateMachine aiStateMachine) : base(aiController, aiStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        pauseTime = 1.5f; // Set duration time for pausedTime.
        m_aiController.Agent.enabled = false; // Disable agent for the AI to stop moving.
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        pauseTime -= Time.deltaTime;

        // Once pausedTimer reaches 0, change the state back to moving state.
        if (pauseTime <= 0f)
        {
            m_aiController.AIStateMachine.ChangeState(m_aiController.AIMovingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        m_aiController.Agent.enabled = true; // Enable the agent so the AI can continue to move.
    }
}
