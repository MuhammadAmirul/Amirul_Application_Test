using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed;
    private WaitForSeconds existTimer = new WaitForSeconds(10f);

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2f, 4f);
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return existTimer;
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }

    void Rolling()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
