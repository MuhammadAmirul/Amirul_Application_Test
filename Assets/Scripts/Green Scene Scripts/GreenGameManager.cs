using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class GreenGameManager : MonoBehaviour
{
    [Header("Contestants")]
    [SerializeField] private List<GameObject> contestants;
    //[SerializeField] private List<NavMeshAgent> agentList;

    [Header("Levels Properties")]
    [SerializeField] private Transform levelParentTransform;
    [SerializeField] private Transform selectedLevel;
    [SerializeField] private Transform lanes;
    [SerializeField] private List<Transform> individualLane;
    public Transform Lanes => lanes;
    [Space]
    [Header("Rocks Spawning Proporties")]
    [SerializeField] private Transform leftObstacle, rightObstacle; // Left and right obstacle where the rock will spawn.
    [SerializeField] private GameObject rock; // Rock to spawn.
    [SerializeField] private List<GameObject> rocksList;
    [SerializeField] private float spawnTime; // Spawn Time for the rocks to spawn.
    [Space]
    [SerializeField] private int completedLap; // Completed lap is to check if all AI has returned to their starting point.
    [SerializeField] private bool startRace; // A bool to start the race after the Start Race button is pressed.
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

    private int levelNum;
    private int previousLevelNum = 1;

    private void Awake()
    {
        DisableLevels();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableLevelRandomly();
        
        GetContestants();
        GetLanesTransform();
        GetIndividualLane();
    }

    #region Startup Functions
    // Disable all the child transform in the levelParentTransform.
    void DisableLevels()
    {
        for (int index = 0; index < levelParentTransform.childCount; index++)
        {
            GameObject childLevel = levelParentTransform.GetChild(index).gameObject;
            childLevel.SetActive(false);
        }
    }

    // Enable the start level on Start.
    void EnableStartingLevel()
    {
        // Get the transform of the selected level based on the level number.
        if (levelNum != previousLevelNum)
        {
            selectedLevel = levelParentTransform.GetChild(levelNum);
            selectedLevel.gameObject.SetActive(true);
            previousLevelNum = levelNum;
        }
    }

    // Get all the AIs in the scene.
    void GetContestants()
    {
        contestants = GameObject.FindGameObjectsWithTag("AI").ToList();

        for (int index = 0; index < contestants.Count; index++)
        {
            // Set their speed to 0 so the AIs will not move when the game starts
            // until the start button is pressed.
            NavMeshAgent agent = contestants[index].GetComponent<NavMeshAgent>();
            agent.speed = 0f;
        }
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
        individualLane.Clear();

        for (int index  = 0; index < lanes.childCount; index++)
        {
            individualLane.Add(lanes.GetChild(index));
        }
    }

    void EnableLevelRandomly()
    {
        if (levelNum == levelParentTransform.childCount - 1)
        {
            levelNum = 0;
        }
        else
        {
            levelNum++;
        }

        selectedLevel = levelParentTransform.GetChild(levelNum);
        selectedLevel.gameObject.SetActive(true);
        previousLevelNum = levelNum;
    }

    // Enable the game to commence when Start Race button is pressed.
    public void EnableStartRace()
    {
        if (completedLap == 6)
        {
            DisableLevels();
            EnableLevelRandomly();
            GetLanesTransform();
            GetIndividualLane();
            EnableAI();
            DestroyRocks();
            completedLap = 0;
        }

        startRace = true;
    }

    void EnableAI()
    {
        for (int index = 0; index < contestants.Count; index++)
        {
            contestants[index].SetActive(true);
            AIController aiController = contestants[index].GetComponent<AIController>();
            aiController.GetWayPoints?.Invoke();
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        StopRace();
    }

    void StopRace()
    {
        if (completedLap == 6)
        {
            startRace = false;
        }

        SpawnRocks();
    }

    #region Level 1 Functions
    void SpawnRocks()
    {
        if (startRace && levelNum == 0)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0f)
            {
                SpawnRocksOnObstacle(leftObstacle);
                SpawnRocksOnObstacle(rightObstacle);
                spawnTime = Random.Range(1f, 4f);
            }
        }
    }

    void SpawnRocksOnObstacle(Transform obstacle)
    {
        float randomLeft = obstacle.position.z - obstacle.localScale.x / 2.5f;
        float randomRight = obstacle.position.z + obstacle.localScale.x / 2.5f;
        float randomPosZ = Random.Range(randomLeft, randomRight);
        GameObject spawnedRock = Instantiate(rock, new Vector3(obstacle.localPosition.x, obstacle.localPosition.y, randomPosZ), obstacle.rotation);
        AddRocksToList(spawnedRock);
    }

    void AddRocksToList(GameObject rock)
    {
        rocksList.Add(rock);
    }

    void DestroyRocks()
    {
        rocksList = GameObject.FindGameObjectsWithTag("Rock").ToList();
        for (int index = 0; index < rocksList.Count; index++)
        {
            if (rocksList[index] != null)
            {
                Destroy(rocksList[index]);
            }
        }
        rocksList.Clear();
    }
    #endregion
}
