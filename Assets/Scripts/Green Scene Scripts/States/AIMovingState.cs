using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amirul.AI.StateMachine;

public class AIMovingState : AIState
{
    public AIMovingState(AIController aiController, StateMachine aiStateMachine) : base(aiController, aiStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        m_aiController.Agent.speed = 1.5f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (m_aiController.WayPoint != null)
        {
            m_aiController.Agent.SetDestination(m_aiController.WayPointsList[1].position);

            float distance = Vector3.Distance(m_aiController.transform.position, m_aiController.OffMeshLinks.startTransform.position);
            if (distance <= m_aiController.Agent.stoppingDistance)
            {
                Debug.Log("OFF MESH LINK: " + distance);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
