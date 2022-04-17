using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class GreenGameManager : MonoBehaviour
{
    [Header("Contestants")]
    [SerializeField] private List<GameObject> contestants;
    [SerializeField] private List<NavMeshAgent> agentList;

    [Header("Transform Properties")]
    [SerializeField] private Transform levelParentTransform;
    [SerializeField] private Transform selectedLevel;
    [SerializeField] public Transform lanes;
    [SerializeField] private List<Transform> individualLane;
    public Transform Lanes => lanes;
    [Space]
    [SerializeField] private Transform leftObstacle, rightObstacle;
    [SerializeField] private GameObject rock;
    [SerializeField] private float spawnTime;
    [SerializeField] private int completedLap;
    [SerializeField] private bool startRace;
    public int CompletedLap
    {
        get { return completedLap; }
        set { completedLap = value; }
    }
    public bool StartRace
    {
        get { return startRace; }
        set { startRace = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the transform of the selected level whenever the randm number changes.
        selectedLevel = levelParentTransform.GetChild(0);

        GetContestants();
        GetLanesTransform();
        GetIndividualLane();
    }

    void GetContestants()
    {
        contestants = GameObject.FindGameObjectsWithTag("AI").ToList();

        for (int index = 0; index < contestants.Count; index++)
        {
            NavMeshAgent agent = contestants[index].GetComponent<NavMeshAgent>();
            agentList.Add(agent);
            agent.speed = 0f;
        }
    }

    public void EnableContestants()
    {
        for (int index = 0; index < agentList.Count; index++)
        {
            agentList[index].speed = 3.5f;
        }

        startRace = true;
    }

    // Lop through selectedLevel transform to find the transform named Lanes.
    void GetLanesTransform()
    {
        for (int index = 0; index < selectedLevel.childCount; index++)
        {
            if (selectedLevel.GetChild(index).name.Equals("Lanes"))
            {
                lanes = selectedLevel.GetChild(index);
                break;
            }
            else
            {
                continue;
            }
        }
    }

    // Get all individual lanes and add to individualLane list.
    void GetIndividualLane()
    {
        for (int index  = 0; index < lanes.childCount; index++)
        {
            individualLane.Add(lanes.GetChild(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0f)
        {
            SpawnRocksOnObstacle(leftObstacle);
            SpawnRocksOnObstacle(rightObstacle);
            spawnTime = Random.Range(1f, 4f);
        }
    }

    void SpawnRocksOnObstacle(Transform obstacle)
    {
        float randomLeft = obstacle.position.z - obstacle.localScale.x / 2.5f;
        float randomRight = obstacle.position.z + obstacle.localScale.x / 2.5f;
        float randomPosZ = Random.Range(randomLeft, randomRight);
        Instantiate(rock, new Vector3(obstacle.localPosition.x, obstacle.localPosition.y, randomPosZ), obstacle.rotation);
    }
}
