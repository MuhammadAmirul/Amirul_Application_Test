using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [Space]
    [SerializeField] private float randomMinValue;
    [SerializeField] private float randomMaxValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 randomPos = new Vector3(Random.Range(-randomMinValue, randomMaxValue), Random.Range(randomMinValue, randomMaxValue), Random.Range(-randomMinValue, randomMaxValue));
            rigidbody.AddForce(randomPos);
            rigidbody.AddTorque(randomPos);
        }
    }
}
