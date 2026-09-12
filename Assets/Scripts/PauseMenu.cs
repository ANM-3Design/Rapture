using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // drag your pause UI panel in
    bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnResumeButton() => TogglePause();

    public void OnRestartButton()
    {
        Time.timeScale = 1f; // NEW — must un-pause before loading, or the next scene starts frozen
        GameManager.Instance.RestartGame();
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f; // NEW — harmless here since quitting, but keeps the habit consistent
        GameManager.Instance.QuitGame();
    }
}
