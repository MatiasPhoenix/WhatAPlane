using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Prefab Players")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;
    public GameObject player4Prefab;
    public GameObject explosionPlanePrefab;


    [Header("Multiplayer Scenario")]
    public GameObject twoPlayersPlay; //Only two players
    public GameObject fourPlayersPlay; //Until four players


    [Header("Player Flags")]
    public GameObject flagPlayer1;
    public GameObject flagPlayer2;
    public GameObject flagPlayer3;
    public GameObject flagPlayer4;


    [Header("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;


    // [Header("Life Points")]
    // public Text player1ScoreText;
    // public Text player2ScoreText;
    // public Text player3ScoreText;
    // public Text player4ScoreText;


    //Private variables
    bool fourPlayers = false;
    bool internalBool4Players = true;
    int player1Lifes = 5, player2Lifes = 5, player3Lifes = 0, player4Lifes = 0;

    bool player1Active = false, player2Active = false, player3Active = false, player4Active = false;
    bool ifPlayer3Plays = false, ifPlayer4Plays = false;
    private float initialYFlagPlayer1, initialYFlagPlayer2, initialYFlagPlayer3, initialYFlagPlayer4;


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player1Active = true;
        player2Active = true;
    }
    private void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);

        DontDestroyOnLoad(gameObject);

        initialYFlagPlayer1 = flagPlayer1.transform.position.y;
        initialYFlagPlayer2 = flagPlayer2.transform.position.y;


    }

    private void Update()
    {

        if (fourPlayers && internalBool4Players) //Play with 4 players
        {
            twoPlayersPlay.SetActive(false);
            fourPlayersPlay.SetActive(true);

            internalBool4Players = false; //Control one shot using
        }

        //Who is the WINNER?
        if (player1Lifes >= 1 && player2Lifes <= 0 && player3Lifes <= 0 && player4Lifes <= 0)
        {
            MenuManager.Instance.WinnerPlayer("Player1");
        }
        else if (player1Lifes <= 0 && player2Lifes >= 1 && player3Lifes <= 0 && player4Lifes <= 0)
        {
            MenuManager.Instance.WinnerPlayer("Player2");
        }
        else if (player1Lifes <= 0 && player2Lifes <= 0 && player3Lifes >= 1 && player4Lifes <= 0)
        {
            MenuManager.Instance.WinnerPlayer("Player3");
        }
        else if (player1Lifes <= 0 && player2Lifes <= 0 && player3Lifes <= 0 && player4Lifes >= 1)
        {
            MenuManager.Instance.WinnerPlayer("Player4");
        }

        //Press button for new Playes
        if (Input.GetKeyDown(KeyCode.U) && fourPlayers == true && player3Active == false)
        {
            player3Active = true;
            ifPlayer3Plays = true;
            player3Lifes = 5;
            StartCoroutine(SpawnPlayer("Player3"));
        }
        else if (Input.GetKeyDown(KeyCode.I) && fourPlayers == true && player4Active == false)
        {
            player4Active = true;
            ifPlayer4Plays = true;
            player4Lifes = 5;
            StartCoroutine(SpawnPlayer("Player4"));
        }
    }

    public IEnumerator SpawnPlayer(string player)
    {
        yield return new WaitForSeconds(3f);

        if (player == "Player1" && player1Active && GameObject.FindWithTag("Player1") == null)
        {
            Instantiate(player1Prefab, spawnPoint1.position, Quaternion.identity);
        }
        else if (player == "Player2" && player2Active && GameObject.FindWithTag("Player2") == null)
        {
            Instantiate(player2Prefab, spawnPoint2.position, player2Prefab.transform.rotation);
        }
        else if (player == "Player3" && player3Active && GameObject.FindWithTag("Player3") == null)
        {
            Instantiate(player3Prefab, spawnPoint3.position, player3Prefab.transform.rotation);
        }
        else if (player == "Player4" && player4Active && GameObject.FindWithTag("Player4") == null)
        {
            Instantiate(player4Prefab, spawnPoint4.position, Quaternion.identity);
        }

        yield return null;
    }


    public void CreateExplosion(Vector3 position)
    {
        Instantiate(explosionPlanePrefab, position, Quaternion.identity);
    }

    public void PointPlayer(string player)
    {
        if (player == "Player1")
        {
            // player1ScoreText.text = (int.Parse(player1ScoreText.text) + 1).ToString();
            player1Lifes--;
            PlayerFlagDamage("Player1");
            UnityEngine.Debug.Log("Player1 lifes: " + player1Lifes);

            // if (player1Lifes <= 0)
            // {
            //     player1Active = false;
            // }
        }
        else if (player == "Player2")
        {
            // player2ScoreText.text = (int.Parse(player2ScoreText.text) + 1).ToString();
            player2Lifes--;
            PlayerFlagDamage("Player2");
            UnityEngine.Debug.Log("Player2 lifes: " + player2Lifes);

            // if (player2Lifes <= 0)
            // {
            //     player2Active = false;
            // }
        }
        else if (player == "Player3")
        {
            // player3ScoreText.text = (int.Parse(player3ScoreText.text) + 1).ToString();
            player3Lifes--;
            PlayerFlagDamage("Player3");
            UnityEngine.Debug.Log("Player3 lifes: " + player3Lifes);

            // if (player3Lifes <= 0)
            // {
            //     player3Active = false;
            // }
        }
        else if (player == "Player4")
        {
            // player4ScoreText.text = (int.Parse(player4ScoreText.text) + 1).ToString();
            player4Lifes--;
            PlayerFlagDamage("Player4");
            UnityEngine.Debug.Log("Player4 lifes: " + player4Lifes);

            // if (player4Lifes <= 0)
            // {
            //     player4Active = false;
            // }
        }
    }

    public void StartCoroutineManager(string player)
    {
        StartCoroutine(SpawnPlayer(player));
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        if (!player1Active)
        {
            player1Lifes = 5;
            player1Active = true;
            RefreshFlagPosition(flagPlayer1, initialYFlagPlayer1);
            StartCoroutineManager("Player1");
        }
        else
        {
            player1Lifes = 5;
            RefreshFlagPosition(flagPlayer1, initialYFlagPlayer1); 
        }

        if (!player2Active)
        {
            player2Lifes = 5;
            player2Active = true;
            RefreshFlagPosition(flagPlayer2, initialYFlagPlayer2);
            StartCoroutineManager("Player2");
        }
        else
        {
            player2Lifes = 5;
            RefreshFlagPosition(flagPlayer2, initialYFlagPlayer2);
        }

        if (fourPlayers)
        {
            if (ifPlayer3Plays && !player3Active)
            {
                player3Lifes = 5;
                player3Active = true;
                RefreshFlagPosition(flagPlayer3, initialYFlagPlayer3);
                StartCoroutineManager("Player3");
            }
            else if (ifPlayer3Plays && player3Active)
            {
                player3Lifes = 5;
                RefreshFlagPosition(flagPlayer3, initialYFlagPlayer3);
            }

            if (ifPlayer4Plays && !player4Active)
            {
                player4Lifes = 5;
                player4Active = true;
                RefreshFlagPosition(flagPlayer4, initialYFlagPlayer4);
                StartCoroutineManager("Player4");
            }
            else if (ifPlayer4Plays && player4Active)
            {
                player4Lifes = 5;
                RefreshFlagPosition(flagPlayer4, initialYFlagPlayer4);
            }
        }
    }




    public void FourPlayers()
    {
        if (!fourPlayers)
        {
            initialYFlagPlayer3 = flagPlayer3.transform.position.y;
            initialYFlagPlayer4 = flagPlayer4.transform.position.y;
        }
        fourPlayers = true;
        MenuManager.Instance.fourPlayers = true;
        MenuManager.Instance.StartGame();
    }


    private void UpdateFlagPosition(GameObject flag, int lifes, float initialY, float step = 3f)
    {
        lifes = Mathf.Clamp(lifes, 0, 5); // Assicura che il range sia valido
        float newY = initialY - step * (5 - lifes);
        flag.transform.position = new Vector3(
            flag.transform.position.x,
            newY,
            flag.transform.position.z
        );
    }


    private void RefreshFlagPosition(GameObject flag, float initialY)
    {
        flag.transform.position = new Vector3(
            flag.transform.position.x,
            initialY,
            flag.transform.position.z
        );
    }


    public void PlayerFlagDamage(string playerPlane)
    {
        if (playerPlane == "Player1")
        {
            UpdateFlagPosition(flagPlayer1, player1Lifes, initialYFlagPlayer1);
            if (player1Lifes <= 0) player1Active = false;
        }
        else if (playerPlane == "Player2")
        {
            UpdateFlagPosition(flagPlayer2, player2Lifes, initialYFlagPlayer2);
            if (player2Lifes <= 0) player2Active = false;
        }

        if (fourPlayers)
        {
            if (playerPlane == "Player3")
            {
                UpdateFlagPosition(flagPlayer3, player3Lifes, initialYFlagPlayer3);
                if (player3Lifes <= 0) player3Active = false;
            }
            else if (playerPlane == "Player4")
            {
                UpdateFlagPosition(flagPlayer4, player4Lifes, initialYFlagPlayer4);
                if (player4Lifes <= 0) player4Active = false;
            }
        }
    }



}
