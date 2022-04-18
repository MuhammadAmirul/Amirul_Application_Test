using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCrate : MonoBehaviour
{
    [SerializeField] private Transform target; // Focuses on the crate that is set by the target.

    [SerializeField] private Vector3 offset; // Offset position for the camera to follow the crate.

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
