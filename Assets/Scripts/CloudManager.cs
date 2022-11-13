using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject cloud;
    public float speed;
    public float offset = 7;

    public void Awake()
    {
        SpawnCloud();
    }

    public void SpawnCloud()
    {
        GameObject c = Instantiate(cloud, new Vector3(Random.Range(-15, 15), Camera.main.transform.position.y + offset, 0), Quaternion.identity);
        c.GetComponent<Cloud>().speed = speed;
        Invoke("SpawnCloud", Random.Range(0.1f, 0.2f));
    }
}
