using UnityEngine.SceneManagement;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;
    void Update()
    {
        if (GameObject.Find("duck"))
        {
            if (Vector3.Distance(gameObject.transform.position, GameObject.Find("duck").transform.position) < 3f)
            {
                GameIsOver();
            }
        }
    }
    public void GameIsOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        PauseGame.GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
