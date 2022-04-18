using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private List<GameObject> contestants; // Contestants list to have the camera move to the AI position.
    [Space]
    [SerializeField] private TextMeshProUGUI aiNameText; // Display the AI's name when the camera is spectating them.

    [SerializeField] private Vector3 offset; // Offset position for the camera to properly spectate the AIs.
    [SerializeField] private int number; // Number to go through all the AIs when switching camera positions.

    private float lerpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        GetContestantsList();

        aiNameText.text = contestants[number].name;
    }

    // Get all the contestants.
    void GetContestantsList()
    {
        contestants = GameObject.FindGameObjectsWithTag("AI").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraPosition();
    }

    private void LateUpdate()
    {
        transform.position = CameraLerp();
    }

    // Change the camera position to look at each contestants based on the list element index.
    void ChangeCameraPosition()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (number == contestants.Count - 1)
            {
                number = 0;
            }
            else
            {
                number++;
            }
            aiNameText.text = contestants[number].name;
        }
    }

    // Lerp the camera when following the contestants.
    Vector3 CameraLerp()
    {
        return Vector3.Lerp(transform.position, contestants[number].transform.position + offset, lerpSpeed * Time.deltaTime);
    }
}
