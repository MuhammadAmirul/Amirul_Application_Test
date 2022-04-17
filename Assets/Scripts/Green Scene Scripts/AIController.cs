using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Amirul.AI.StateMachine;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private OffMeshLink offMeshLinks;
    public NavMeshAgent Agent
    {
        get { return agent; }
        set { agent = value; }
    }
    public OffMeshLink OffMeshLinks => offMeshLinks;
    [Space]
    [SerializeField] private Transform wayPoint;
    [SerializeField] private List<Transform> wayPointsList;
    public Transform WayPoint => wayPoint;
    public List<Transform> WayPointsList
    {
        get { return wayPointsList; }
        set { wayPointsList = value; }
    }

    private StateMachine _AIStateMachine;

    private AIMovingState _AIMovingState;

    // Start is called before the first frame update
    void Start()
    {
        if (wayPoint != null)
        {
            for (int index = 0; index < wayPoint.childCount; index++)
            {
                wayPointsList.Add(wayPoint.GetChild(index));
            }
        }

        _AIStateMachine = new StateMachine();

        _AIMovingState = new AIMovingState(this, _AIStateMachine);
        _AIStateMachine.Initialize(_AIMovingState);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
