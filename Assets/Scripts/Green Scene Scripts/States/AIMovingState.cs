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
        // Make sure its not the last lap so the AI will continue to move to the right way point.
        if (!lastLap)
        {
            wayPointIndex = 1; // Set waypointIndex to 1 so the AIs will move to the first way point.
        }
        m_aiController.Agent.speed = 3.5f; // Set the speed of the agent to 3.5f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        distanceFromWayPoint = CheckWayPointDistance(wayPointIndex); // Check the distance of the current way point the AI is moving to.

        MovingToWayPoints();
    }

    public override void Exit()
    {
        base.Exit();
        // once it is the last lap and AI reaches the last way point, run the codes below.
        if (CheckWayPointDistance(0) < m_aiController.Agent.stoppingDistance && lastLap)
        {
            lastLap = false;
            m_aiController.GreenGameManager.CompletedLap++; // Each AI will contribute to the CompletedLap points, resulting in 6, which is the total AI in the scene.
            m_aiController.Animator.ResetTrigger("isMoving"); // Reset the trigger animation to change the animation to idle.
            m_aiController.gameObject.SetActive(false); // Set active of the AI gameObject to false.
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        // If AI collides with either the rock or barricade, pause the AI.
        if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Barricade")
        {
            m_aiController.AIStateMachine.ChangeState(m_aiController.AIPausedState);
        }
    }

    void MovingToWayPoints()
    {
        m_aiController.Agent.SetDestination(m_aiController.WayPointsList[wayPointIndex].position); // Move the AI to the current destination.
        
        // Increase the wayPointIndex when the AI has reached the way point and if the wayPointIndex value is not larger than the WayPointsList count.
        if (distanceFromWayPoint < m_aiController.Agent.stoppingDistance && wayPointIndex < m_aiController.WayPointsList.Count - 1)
        {
            wayPointIndex++;
        }
        // Set the wayPointIndex to 0 when the AI has reached the final way point to return back to starting way point.
        else if (distanceFromWayPoint < m_aiController.Agent.stoppingDistance && wayPointIndex == m_aiController.WayPointsList.Count - 1)
        {
            wayPointIndex = 0;
            lastLap = true;
        }
        // Change the AI State to idle once it is the last lap and reaches the starting way point.
        if (CheckWayPointDistance(0) < m_aiController.Agent.stoppingDistance && lastLap)
        {
            m_aiController.AIStateMachine.ChangeState(m_aiController.AIIdleState);
        }
    }

    float CheckWayPointDistance(int wayPointIndex)
    {
        return Vector3.Distance(m_aiController.transform.position, m_aiController.WayPointsList[wayPointIndex].position);
    }
}
