using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public TextMeshProUGUI timerText;
    public float timeRemaining = 600; // 10 minutes
    public bool timerIsRunning = false;

    private void Start()
    {
        timerIsRunning = true;
        DisplayTime(timeRemaining);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = 0; // Stop the game
                Cursor.lockState = CursorLockMode.None; // Enable the cursor
                Cursor.visible = true;
                timerIsRunning = false;
                timerText.gameObject.SetActive(false);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}