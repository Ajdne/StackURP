using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameManager gm;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private GameObject levelFail;
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject endGame;

    [SerializeField] private GameObject joystickCanvas;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gm = GameManager.Instance;
        Time.timeScale = 0;
    }

    public void Play()
    {       
        Time.timeScale = 1.0f;
        startGame.SetActive(false);
        joystickCanvas.SetActive(true);

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        // activate pause menu

        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        joystickCanvas.SetActive(false);
    }


    #region Pause Menu
    public void Resume()
    {
        // deactivate pause menu
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        joystickCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Quit App
    public void Quit()
    {
        // save player progress here

        // maybe add "thank you for playing" or something

        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    #endregion
    #endregion

    #region Level End
    public void LevelComplete()
    {
        // activate canvas
        levelComplete.SetActive(true);

        // deactivate joystick
        joystickCanvas.SetActive(false);
    }

    public void LevelFailed()
    {
        // activate canvas
        levelFail.SetActive(true);

        // deactivate joystick
        joystickCanvas.SetActive(false);
    }
    #endregion

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
