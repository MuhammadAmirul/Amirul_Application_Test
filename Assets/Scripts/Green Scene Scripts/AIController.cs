using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Amirul.AI.StateMachine;
using System;

public class AIController : MonoBehaviour
{
    [Header("Script Component")]
    [SerializeField] private GreenGameManager greenGameManager;
    public GreenGameManager GreenGameManager => greenGameManager;
    private enum Contestant { First, Second, Third, Fourth, Fifth, Sixth };

    [Header("Enum Component")]
    [SerializeField] Contestant contestant; // Contestants enum to indicate each AI is in which lane.
    
    [Header("NavMesh Agent Component")]
    [SerializeField] private NavMeshAgent agent;
    #region Nav Mesh Properties
    public NavMeshAgent Agent
    {
        get { return agent; }
        set { agent = value; }
    }
    #endregion
    
    [Header("Transform Components")]
    [SerializeField] private Transform lane;
    [SerializeField] private List<Transform> wayPointsList;
    #region Transform Properties
    public Transform WayPoint => lane;
    public List<Transform> WayPointsList
    {
        get { return wayPointsList; }
        set { wayPointsList = value; }
    }
    #endregion
    [Space]
    [SerializeField] private Animator animator;
    #region Properties
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    #endregion

    public StateMachine AIStateMachine;

    public AIMovingState AIMovingState;
    public AIIdleState AIIdleState;
    public AIStunnedState AIStunnedState;

    public Action GetWayPoints;

    // Start is called before the first frame update
    void Start()
    {
        greenGameManager = FindObjectOfType<GreenGameManager>();

        Invoke("GetWayPoint", 0.1f);

        // Set the GetWayPoint to the GetWayPoints Action to be called again when a new race starts.
        GetWayPoints = GetWayPoint;

        AIStateMachine = new StateMachine();

        AIMovingState = new AIMovingState(this, AIStateMachine);
        AIIdleState = new AIIdleState(this, AIStateMachine);
        AIStunnedState = new AIStunnedState(this, AIStateMachine);

        // Start the AI state as Idle.
        AIStateMachine.Initialize(AIIdleState);
    }

    void GetWayPoint()
    {
        // Loops through all the child of the Lane transform to get the way points.
        for (int index = 0; index < greenGameManager.Lanes.childCount; index++)
        {
            // Based on the switch, the AI will go through the way points respectively.
            switch (contestant)
            {
                case Contestant.First:
                    CheckForIndividualLane(index, "1st");
                    break;

                case Contestant.Second:
                    CheckForIndividualLane(index, "2nd");
                    break;

                case Contestant.Third:
                    CheckForIndividualLane(index, "3rd");
                    break;

                case Contestant.Fourth:
                    CheckForIndividualLane(index, "4th");
                    break;

                case Contestant.Fifth:
                    CheckForIndividualLane(index, "5th");
                    break;

                case Contestant.Sixth:
                    CheckForIndividualLane(index, "6th");
                    break;
            }
        }
        SpawnOnStartingPoint();
    }

    void CheckForIndividualLane(int index, string laneNumber)
    {
        // Checks the transform lane and check if the name contains 1st, 2nd, etc to
        // start assigning the AI lanes.
        if (greenGameManager.Lanes.GetChild(index).name.Contains(laneNumber))
        {
            lane = greenGameManager.Lanes.GetChild(index);
            GetStartAndEndWayPoints(laneNumber);
        }
    }

    void GetStartAndEndWayPoints(string laneNumber)
    {
        // Clear the list for the new level.
        wayPointsList.Clear();

        // After getting the lane, get the starting and ending point of the transform from the
        // parent transform.
        for (int index = 0; index < lane.childCount; index++)
        {
            if (lane.GetChild(index).name.Contains(laneNumber))
            {
                wayPointsList.Add(lane.GetChild(index));
            }
        }
    }

    void SpawnOnStartingPoint()
    {
        // Spawns the AI on the starting way point.
        transform.position = wayPointsList[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        AIStateMachine.CurrentState.LogicUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AIStateMachine.CurrentState.OnCollisionEnter(collision);
    }
}
