using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{

    public GameObject duck;
    public float spawnInterval;
    public int duckCount;
    float t = 0;
    private void Update()
    {

        if (Time.time - t > spawnInterval) //Interval of Duck Spawns
        {
            for (int i = 0; i < duckCount; i++)
            {
                Instantiate(duck, transform.position, Quaternion.identity);
            }


            t = Time.time;
        }
    }

}
