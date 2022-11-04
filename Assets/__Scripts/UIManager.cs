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
    [Space]

    [Header("Music Settings"), Space(5f)]
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private AudioSource musicSource;   // only the background music source - this canvas

    [Space(5f)]
    [Header("Sound Settings"), Space(5f)]
    [SerializeField] private AudioListener audioListener;   // main camera
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;

    [Header("Joystick Canvas"), Space(5f)]
    [SerializeField] private GameObject joystickCanvas;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(GameManager.FirstLoad)
        {
            startGame.SetActive(true);
            GameManager.FirstLoad = false;
            Time.timeScale = 0;
        }
        else
        {
            Play();
        }

        gm = GameManager.Instance;

        audioListener = Camera.main.GetComponent<AudioListener>();

        // load users sound setting
        if (PlayerPrefs.GetInt("sound", 1) == 0) TurnSoundOff();
        if (PlayerPrefs.GetInt("music", 1) == 0) TurnMusicOff();
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
        // increment level
        GameManager.level++;

        // save level data
        PlayerPrefs.SetInt("level", GameManager.level);


        if (GameManager.level > 10)
        {
            // randomize next scene - recursive
            SceneManager.LoadScene(GetRandomlevel());
        }
        else
        {
            // load next scene
            SceneManager.LoadScene(GameManager.level);
        }


    }

    private int GetRandomlevel()    // recursive function
    {
        int randomLevel = Random.Range(0, 11);

        if (randomLevel == SceneManager.GetActiveScene().buildIndex)
        {
            return GetRandomlevel();
        }
        return randomLevel;
    }

    #region Audio Controlls
    public void TurnMusicOff()
    {
        musicOn.SetActive(false);
        musicOff.SetActive(true);

        musicSource.enabled = false;
    }

    public void TurnMusicOn()
    {
        musicOff.SetActive(false);
        musicOn.SetActive(true);

        musicSource.enabled = true;
    }

    public void TurnSoundOff()
    {
        soundOn.SetActive(false);
        soundOff.SetActive(true);

        audioListener.enabled = false;
    }

    public void TurnSoundOn()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);

        audioListener.enabled = true;
    }


    #endregion
}
