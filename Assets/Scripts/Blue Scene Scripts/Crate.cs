using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [Space]
    [SerializeField] private float randomMinValue; // A random minimum value for the crate to bounce about.
    [SerializeField] private float randomMaxValue; // A random maximum value for the crate to bounce  about.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Give a random position for the crate to bounce when space key is pressed.
            Vector3 randomPos = new Vector3(Random.Range(-randomMinValue, randomMaxValue), Random.Range(randomMinValue, randomMaxValue), Random.Range(-randomMinValue, randomMaxValue));

            // Add force and torque using the randomPos to bounce at a random direction.
            rigidbody.AddForce(randomPos, ForceMode.Impulse);
            rigidbody.AddTorque(randomPos);
        }
    }
}
