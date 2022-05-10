using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    int roundNum = 0;
    public float waveMultipler = 2;
    public float duckPerWaveMultipler = 2;
    public float waveIntervalMultipler = 1;
    public GameObject roundMenu;
    public GameObject gameOverMenu;
    public int waveCount = 2;
    public int ducksPerWave = 2;
    public int waveInterval = 15;
    public bool roundActive;
    public Text roundNumText;
    float t;

    private void Start()
    {
        StartRound();
        t = Time.time;
        StartCoroutine(gameObject.GetComponent<Spawn>().MelonSpawn());
    }
     
    public void StartRound()
    {

        Time.timeScale = 1;
        roundNum += 1;
        Cursor.lockState = CursorLockMode.Locked;
        roundMenu.SetActive(false);
        PauseGame.GameIsPaused = false;

        StartCoroutine(gameObject.GetComponent<Spawn>().SpawnDuck(waveCount,ducksPerWave,waveInterval));

        waveCount = Mathf.RoundToInt(waveCount * waveMultipler);
        ducksPerWave = Mathf.RoundToInt(ducksPerWave * duckPerWaveMultipler);
        waveInterval = Mathf.RoundToInt(waveInterval * waveIntervalMultipler);
        roundNumText.text = "Round "+roundNum.ToString();

    }
    public IEnumerator EndRound()
    {
        if (Time.time-t>5 && !roundActive)
        {
            yield return new WaitForSeconds(1f);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            roundMenu.SetActive(true);
            PauseGame.GameIsPaused = true;
        }
    }
}
