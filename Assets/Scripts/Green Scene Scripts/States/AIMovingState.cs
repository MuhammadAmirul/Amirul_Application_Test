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
        wayPointIndex = 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        distanceFromWayPoint = CheckWayPointDistance(wayPointIndex); Debug.Log("DISTANCE: " + distanceFromWayPoint);

        MovingToWayPoints();
    }

    public override void Exit()
    {
        base.Exit();
        m_aiController.GreenGameManager.CompletedLap++;
    }

    void MovingToWayPoints()
    {
        m_aiController.Agent.SetDestination(m_aiController.WayPointsList[wayPointIndex].position);
        
        if (distanceFromWayPoint < m_aiController.Agent.stoppingDistance && wayPointIndex < m_aiController.WayPointsList.Count - 1)
        {
            wayPointIndex++;
        }
        else if (distanceFromWayPoint < m_aiController.Agent.stoppingDistance && wayPointIndex == m_aiController.WayPointsList.Count - 1)
        {
            wayPointIndex = 0;
            lastLap = true;
        }
        //Debug.Log("POINTS: " + wayPointIndex);

        if (CheckWayPointDistance(0) < m_aiController.Agent.stoppingDistance && lastLap)
        {
            m_aiController._AIStateMachine.ChangeState(m_aiController._AIIdleState);
        }
    }

    float CheckWayPointDistance(int wayPointIndex)
    {
        return Vector3.Distance(m_aiController.transform.position, m_aiController.WayPointsList[wayPointIndex].position);
    }
}
