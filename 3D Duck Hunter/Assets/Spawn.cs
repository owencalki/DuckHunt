using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawner;
    public GameObject duck;
    public float spawnSpread = 100;
    public List<GameObject> ducks;
    public IEnumerator SpawnDuck(int waveCount, int duckPerWave, int waveInterval)
    {
        gameObject.GetComponent<RoundManager>().roundActive = true;
        while (waveCount > 0)
        {
            for (int i = 0; i < duckPerWave; i++)
            {
                ducks.Add(Instantiate(duck, new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z + Random.Range(-spawnSpread, spawnSpread)), Quaternion.identity));
            }
            waveCount -= 1;
            yield return new WaitForSeconds(waveInterval);
        }
        gameObject.GetComponent<RoundManager>().roundActive = false;
    }


    public GameObject melon;
    public IEnumerator MelonSpawn()
    {
        yield return new WaitForSeconds(Random.Range(20, 40));
        Instantiate(melon, new Vector3(30 * Mathf.Sign(Random.Range(-1, 1)), 2, -50), Quaternion.Euler(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)));
        StartCoroutine(MelonSpawn());
    }
}
