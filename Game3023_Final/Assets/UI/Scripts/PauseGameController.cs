using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameController : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject MenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if(IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    void PauseGame()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        IsPaused = true;
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
