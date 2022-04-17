using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Amirul.AI.StateMachine;

public class AIController : MonoBehaviour
{
    [Header("Script Component")]
    [SerializeField] private GreenGameManager greenGameManager;
    public GreenGameManager GreenGameManager => greenGameManager;
    private enum Contestant { First, Second, Third, Fourth, Fifth, Sixth };

    [Header("Enum Component")]
    [SerializeField] Contestant contestant;
    
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
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Animator animator;
    #region Properties
    public Rigidbody Rigidbody
    {
        get { return rigidbody; }
        set { rigidbody = value; }
    }
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    #endregion

    public StateMachine _AIStateMachine;

    public AIMovingState _AIMovingState;
    public AIIdleState _AIIdleState;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWayPoint());

        _AIStateMachine = new StateMachine();

        _AIMovingState = new AIMovingState(this, _AIStateMachine);
        _AIIdleState = new AIIdleState(this, _AIStateMachine);

        _AIStateMachine.Initialize(_AIIdleState);
    }

    IEnumerator GetWayPoint()
    {
        yield return new WaitForSeconds(0.1f);
        for (int index = 0; index < greenGameManager.lanes.childCount; index++)
        {
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
    }

    void CheckForIndividualLane(int index, string laneNumber)
    {
        if (greenGameManager.lanes.GetChild(index).name.Contains(laneNumber))
        {
            lane = greenGameManager.lanes.GetChild(index);
            GetStartAndEndWayPoints(laneNumber);
        }
    }

    void GetStartAndEndWayPoints(string laneNumber)
    {
        for (int index = 0; index < lane.childCount; index++)
        {
            if (lane.GetChild(index).name.Contains(laneNumber))
            {
                wayPointsList.Add(lane.GetChild(index));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _AIStateMachine.CurrentState.LogicUpdate();
    }
}
