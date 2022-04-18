using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private GreenGameManager greenGameManager;

    private float timer;
    private float randomEulerY;

    private void Awake()
    {
        greenGameManager = FindObjectOfType<GreenGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartRandomRotate();
    }

    void StartRandomRotate()
    {
        if (greenGameManager.StartRace)
        {
            timer -= Time.deltaTime;

            if (timer <= 1.5f && timer > 0f)
            {
                transform.rotation = RandomRotate();
            }
            else if (timer <= 0f)
            {
                timer = 3f;
                randomEulerY = Random.Range(-90f, 90f);
            }
        }
    }

    Quaternion RandomRotate()
    {
        Quaternion euler = Quaternion.Euler(0f, randomEulerY, 0f);
        var rotation = Quaternion.Lerp(transform.rotation, euler, 10f * Time.deltaTime);
        return rotation;
    }
}
