using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Manager")]
    public GameObject menuStartGame;
    public GameObject menuPause;
    public GameObject menuPlayerWin;
    public GameObject tutorialMenu;

    [Header("Winner Manager")]
    public TextMeshProUGUI textWinner;


    //Private variables
    bool turorial = false;
    bool pausaGame = false;
    bool playerWin = false;
    public bool fourPlayers = false;

    public static MenuManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 0;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.P) && playerWin == true)
        {
            RestartGame();
        }
    }


    public void ShowTutorialMenu()
    {
        if (turorial)
        {
            tutorialMenu.SetActive(false);
            menuStartGame.SetActive(true);
            turorial = false;
        }
        else
        {
            tutorialMenu.SetActive(true);
            menuStartGame.SetActive(false);
            turorial = true;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartPlaneFight());
    }

    public IEnumerator StartPlaneFight()
    {
        Time.timeScale = 1;
        menuStartGame.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        if (fourPlayers == false)
        {
            GameManager.Instance.StartCoroutineManager("Player1");
            GameManager.Instance.StartCoroutineManager("Player2");
        }
        else if (fourPlayers == true)
        {
            GameManager.Instance.StartCoroutineManager("Player1");
            GameManager.Instance.StartCoroutineManager("Player2");
            GameManager.Instance.StartCoroutineManager("Player3");
            GameManager.Instance.StartCoroutineManager("Player4");
        }
    }

    public void PauseGame()
    {
        if (pausaGame == true)
        {
            Time.timeScale = 1;
            menuPause.SetActive(false);
            pausaGame = false;
        }
        else
        {
            Time.timeScale = 0;
            menuPause.SetActive(true);
            pausaGame = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.RestartGame();
        playerWin = false;
        menuPlayerWin.SetActive(false);

    }

    public void WinnerPlayer(string player)
    {
        menuPlayerWin.SetActive(true);
        textWinner.text = player;
        Time.timeScale = 0;
        playerWin = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
