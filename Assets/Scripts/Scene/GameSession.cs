using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int HighScore { get; set; } = 0;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI pipUI;
    //public TextMeshProUGUI livesUI;

    public GameObject gameOverScreen;
    public GameObject winGameScreen;

    GameObject player;
    Character character;
    List<GameObject> pips = null;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    public enum eState
    {
        Load,
        StartSession,
        Session,
        EndSession,
        GameOver,
        WinGame
    }

    public eState State { get; set; } = eState.Load;
    //public eState State { get; set; } = eState.StartSession;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HighScore = GameController.Instance.highScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameController.Instance.OnPause();
        }

        switch (State)
        {
            case eState.Load:
                Score = 0;
                if (highScoreUI != null) highScoreUI.text = string.Format("{0:D4}", HighScore);
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    character = player.GetComponent<Character>();
                }
                pips = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pip"));
                State = eState.StartSession;
                break;
            case eState.StartSession:
                if (gameOverScreen != null) gameOverScreen.SetActive(false);
                if (winGameScreen != null) winGameScreen.SetActive(false);
                GameController.Instance.transition.StartTransition(Color.clear, 1);
                State = eState.Session;
                break;
            case eState.Session:
                CheckDeath();
                break;
            case eState.EndSession:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                State = eState.GameOver;
                break;
            case eState.GameOver:
                if (gameOverScreen != null) gameOverScreen.SetActive(true);
                break;
            case eState.WinGame:
                //GameController.Instance.timeScale = Time.timeScale;
                //Time.timeScale = 0;
                if (winGameScreen != null) winGameScreen.SetActive(true);
                break;
            default:
                break;
        }        
    }

    public void AddPoints(int points)
    {
        Score += points;
        if (scoreUI != null) scoreUI.text = string.Format("{0:D4}", Score);

        SetHighScore();
    }

    public void QuitToMainMenu()
    {
        //if (Time.timeScale == 0) Time.timeScale = GameController.Instance.timeScale;
        GameController.Instance.OnLoadMenuScene("MainMenu");
    }

    private void SetHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            GameController.Instance.SetHighScore(HighScore);
            if (highScoreUI != null) highScoreUI.text = string.Format("{0:D4}", HighScore);
        }
    }

    private void CheckDeath()
    {
        if (character.isDead)
        {
            State = eState.EndSession;
            player.SetActive(false);
            player = null;
            character = null;
        }
    }

    public void UpdatePips(GameObject pip)
    {
        pips.Remove(pip);
        if (pipUI != null) pipUI.text = string.Format("{0:D3}", pips.Count);
        if (pips.Count <= 0)
        {
            State = eState.WinGame;
        }
    }
}
