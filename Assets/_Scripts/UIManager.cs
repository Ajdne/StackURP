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

    public TextMeshProUGUI moneyText;

    [SerializeField] private GameObject joystick;

    [SerializeField] private Animator moneyAnimator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gm = GameManager.Instance;

        //joystick.SetActive(true);

        //pauseMenu.SetActive(false);
        //winMenu.SetActive(false);
        //loseMenu.SetActive(false);

        // set money text
        //moneyText.text = "$ " + (gm.Money / 1000).ToString() + "." + (gm.Money % 1000).ToString() + " / " + (gm.MaxStacks / 10).ToString() + "k";
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        // activate pause menu

        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        joystick.SetActive(false);
    }

    public void UpdateMoney()
    {
        //moneyAnimator.Play("MoneyTextAnim");
        //moneyText.text = "$ " + (gm.Money / 1000).ToString() + "." + (gm.Money % 1000).ToString() + " / " + (gm.MaxStacks / 10).ToString() + "k";
    }

    #region Pause Menu
    public void Resume()
    {
        // deactivate pause menu
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
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
