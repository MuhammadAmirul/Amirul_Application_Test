using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

public class GreenGameManager : MonoBehaviour
{
    [SerializeField] private Button startRaceButton;
    [Header("Contestants")]
    [SerializeField] private List<GameObject> contestants;
    public List<GameObject> Contestants => contestants;

    [Header("Levels Properties")]
    [SerializeField] private Transform levelParentTransform; // The parent that holds the 2 levels of the green scene.
    [SerializeField] private Transform selectedLevel; // Selected level after every finished race.
    [SerializeField] private Transform lanes; // Lanes that exists in each level
    [SerializeField] private List<Transform> individualLane; // Individual lanes that will act as way points for the 6 AIs.
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

    private int levelNum = 1; // Level number that changes when a race is finished to choose different level.

    private void Awake()
    {
        DisableLevels();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableLevel();
        
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

    // Enable the level on Start.
    void EnableLevel()
    {
        // Load the first level if the level number is equals to the levelParentTransform child count - 1.
        // Otherwise, load the second level.
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

    // Enable the game to commence when Start Race button is pressed.
    // This method is added into the OnClick of the StartRaceButton.
    public void EnableStartRace()
    {
        if (completedLap == 6)
        {
            DisableLevels();
            EnableLevel();
            GetLanesTransform();
            GetIndividualLane();
            EnableAI();
            DestroyRocks();
            completedLap = 0;
        }

        startRace = true;
        startRaceButton.gameObject.SetActive(false);
    }

    void EnableAI()
    {
        // Loop through the contestants list to set active true for the next level.
        for (int index = 0; index < contestants.Count; index++)
        {
            // Call AIController GetWayPoints action to move them to their new
            // starting position when the next level is loaded.
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
        // Make the bool false once all AIs have returned to their starting point.
        if (completedLap == 6)
        {
            startRace = false;
            startRaceButton.gameObject.SetActive(true);
        }

        SpawnRocks();
    }

    #region Level 1 Functions
    // Spawn Rocks when the first level is loaded and the startRace bool is true.
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

    // Spawn the rock on both left and right side of the obstacle.
    void SpawnRocksOnObstacle(Transform obstacle)
    {
        float randomLeft = obstacle.position.z - obstacle.localScale.x / 2.5f;
        float randomRight = obstacle.position.z + obstacle.localScale.x / 2.5f;
        float randomPosZ = Random.Range(randomLeft, randomRight);
        GameObject spawnedRock = Instantiate(rock, new Vector3(obstacle.localPosition.x, obstacle.localPosition.y, randomPosZ), obstacle.rotation);
        AddRocksToList(spawnedRock);
    }

    // Add the spawn rocks into rocksList.
    void AddRocksToList(GameObject rock)
    {
        rocksList.Add(rock);
    }

    // Destroy the rocks after the first level is completed.
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
