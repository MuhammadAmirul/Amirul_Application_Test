using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amirul.AI.StateMachine;

public class AIState : State
{
    protected AIController m_aiController;
    protected StateMachine m_AIStateMachine;

    protected int wayPointIndex;
    protected float distanceFromWayPoint;
    protected bool lastLap;

    public AIState(AIController aiController, StateMachine aiStateMachine)
    {
        m_aiController = aiController;
        m_AIStateMachine = aiStateMachine;
    }
}
