using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGameManager : MonoBehaviour
{
    [SerializeField] private GameObject crate;
    [SerializeField] private Transform floor; // Spawn the crate at the floor.

    // Start is called before the first frame update
    void Start()
    {
        SpawnCrate();
    }

    void SpawnCrate()
    {
        for (int index = 0; index < 20; index++)
        {
            // Instantiate the crates at the floor using the newSpawnPos variable.
            Vector3 newSpawnPos = new Vector3(RandomXPoint(), 1.5f, RandomZPoint());
            Instantiate(crate, newSpawnPos, floor.rotation);
        }
    }

    // Spawn the crates randomly on the X axis of the floor.
    float RandomXPoint()
    {
        return Random.Range(floor.position.x - floor.localScale.x / 2.5f, floor.position.x + floor.localScale.x / 2.5f);
    }

    // Spawn the crates randomly on the Z axis of the floor.
    float RandomZPoint()
    {
        return Random.Range(floor.position.z - floor.localScale.z / 2.5f, floor.position.z + floor.localScale.z / 2.5f);
    }
}
