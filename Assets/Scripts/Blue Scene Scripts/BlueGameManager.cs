using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGameManager : MonoBehaviour
{
    #region Blue Scene Variables
    [SerializeField] private GameObject crate;
    [SerializeField] private Transform floor;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SpawnCrate();
        //StartCoroutine(SpawnCrate());
    }

    // Update is called once per frame
    void Update()
    {

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

    float RandomXPoint()
    {
        return Random.Range(floor.position.x - floor.localScale.x / 2.5f, floor.position.x + floor.localScale.x / 2.5f);
    }

    float RandomZPoint()
    {
        return Random.Range(floor.position.z - floor.localScale.z / 2.5f, floor.position.z + floor.localScale.z / 2.5f);
    }
}
