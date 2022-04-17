using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    SingletonSceneManager singletonSceneManager;

    #region Blue Scene Variables
    [SerializeField] private GameObject crate;
    [SerializeField] private Transform floor;
    [Space]
    #endregion

    #region Green Scene Variables
    [SerializeField] private Transform selectedLevel;
    [SerializeField] private int selectedLevelNumber;
    [Space]
    [SerializeField] private OffMeshLink offMeshLinks;
    public OffMeshLink OffMeshLinks => offMeshLinks;
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        singletonSceneManager = FindObjectOfType<SingletonSceneManager>();
        singletonSceneManager.GameManagerCheckCurrentScene = CheckCurrentScene;
        //StartCoroutine(SpawnCrate());
    }

    void SpawnCrate()
    {
        for (int index = 0; index < 20; index++)
        {
            Vector3 newSpawnPos = new Vector3(RandomXPoint(), 1.5f, RandomZPoint());
            Instantiate(crate, newSpawnPos, floor.rotation);
            //yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float RandomXPoint()
    {
        return Random.Range(floor.position.x - floor.localScale.x / 2.5f, floor.position.x + floor.localScale.x / 2.5f);
    }

    float RandomZPoint()
    {
        return Random.Range(floor.position.z - floor.localScale.z / 2.5f, floor.position.z + floor.localScale.z / 2.5f);
    }

    void CheckCurrentScene()
    {
        switch (singletonSceneManager.CurrentScene)
        {
            case "BlueScene":
                Debug.Log("IN BLUE SCENE!!!!!!!!!!!!");
                SpawnCrate();
                break;

            case "Green Scene":
                Debug.Log("IN GREEN SCENE!!!!!!!!!!!!");
                break;
        }
    }
}
