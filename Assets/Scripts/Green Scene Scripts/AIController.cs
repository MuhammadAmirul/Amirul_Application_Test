using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [Space]
    [SerializeField] private Transform lane;
    [SerializeField] private List<Transform> wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (lane != null)
        {
            for (int index = 0; index < lane.childCount; index++)
            {
                wayPoints.Add(lane.GetChild(index));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lane != null)
        {
            agent.SetDestination(wayPoints[1].position);
        }
    }
}
