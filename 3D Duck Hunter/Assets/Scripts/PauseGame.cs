using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Menu Pick");
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Settings()
    {
        FindObjectOfType<AudioManager>().Play("Menu Pick");
    }
    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Menu Pick");
        Application.Quit();
    }

    public void OnSelect ()
    {

    }
}
