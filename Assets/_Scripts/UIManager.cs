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
    [SerializeField] private GameObject startGame; 

    [SerializeField] private GameObject joystick;

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
        Debug.Log("e");
        
        Time.timeScale = 1.0f;
        startGame.SetActive(false);
        joystick.SetActive(true);

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        // activate pause menu

        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        joystick.SetActive(false);
    }


    #region Pause Menu
    public void Resume()
    {
        // deactivate pause menu
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        joystick.SetActive(true);
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

}
