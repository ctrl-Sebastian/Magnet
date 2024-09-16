using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int points;
    
    public GameObject EndGamePanel;
    public GameObject PausePanel;
    public GameObject TitlePanel;
    public GameObject TutorialPanel;
    public GameObject ControlsPanel;

    public GameObject UI;

    public Transform playerPos;

    public bool isPaused = false;
    public bool gameHasEnded = false;
    public TMP_Text endGamePointsText;

    private AudioSource audioSource;
    public AudioClip endGameSfx;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 0;
        UI.SetActive(false);
        Cursor.visible = true;
    }

    void Update()
    {

        if(Input.GetKeyDown("escape") && !isPaused && !gameHasEnded){
            PauseGame();
        } else if(Input.GetKeyDown("escape") && isPaused){
            ResumeGame();
        }

        if(CountDownTimer.currentTime <= 0.0f){
            EndGame();
        }

        if(PlayerIsOutOfBounds()){
            Debug.Log("out of bounds");
            playerPos.position = Vector3.zero;
        }
    }

    public bool PlayerIsOutOfBounds()
    {
        if((playerPos.position.x < -37 || playerPos.position.x > 37) 
        || (playerPos.position.y < -10 || playerPos.position.y > 10) 
        || (playerPos.position.z < -37 || playerPos.position.z > 37))
        {
            return true;
        } 
        return false;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        UI.SetActive(true);
        TitlePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowTutorial()
    {
        TutorialPanel.SetActive(true);
        TitlePanel.SetActive(false);
    }

    public void ShowControls()
    {
        ControlsPanel.SetActive(true);
        TitlePanel.SetActive(false);
    }

    public void GoBackFromTutorial()
    {
        TutorialPanel.SetActive(false);
        TitlePanel.SetActive(true);
    }

    public void GoBackFromControls()
    {
        ControlsPanel.SetActive(false);
        TitlePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        CountDownTimer.currentTime = CountDownTimer.startingTime;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void EndGame()
    {
        audioSource.clip = endGameSfx;
        gameHasEnded = true;
        audioSource.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGamePanel.SetActive(true);
        Time.timeScale = 0;
        endGamePointsText.text = "Final Points: " + points.ToString();

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
