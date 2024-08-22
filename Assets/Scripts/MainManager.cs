using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {
    public static MainManager Instance {
        get; private set;
    }

    public Rigidbody ball;

    public Level[] levels;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject playObject;
    public GameObject gameOverObject;
    public GameObject gameCompleteObject;

    private bool m_Started = false, m_Finished = false, m_GameOver = false;

    private int m_Points, m_CurrentLevelIndex = 0;
    
    // Start is called before the first frame update
    void Start() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SaveManager.Instance.Load(SaveManager.Instance.PlayerName);
        highScoreText.text = $"{SaveManager.Instance.PlayerName}'s best score is: <color=yellow>{SaveManager.Instance.HighScore}</color>";
    }

    void Update() {
        ManageGame();
    }

    void ManageGame() {
        if (!m_Started) {
            WaitToStartNewGame();
        } else if (m_Finished) {
            FinishGame();
        } else if (m_GameOver) {
            GameOver();
        }
    }

    void StartNewGame() {
        m_Started = true;

        levels [m_CurrentLevelIndex].BuildBricks();

        playObject.SetActive(false);

        ball.GetComponent<Ball>().StartMovingInRandomDirection();
        ball.transform.SetParent(null);
    }

    void FinishGame() {
        m_Finished = true;

        if (levels.Length < m_CurrentLevelIndex - 1) {
            WaitToPlayNextGame();
        } else {
            GameOver();
        }
    }

    void GameOver() {
        m_GameOver = true;

        if (Input.GetKeyDown(KeyCode.Space)) {
            gameOverObject.SetActive(false);

            WaitToStartNewGame();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            SceneManager.LoadScene("Menu");
        }
    }

    void WaitToStartNewGame() {
        m_GameOver = false;
        m_Finished = false;
        m_Started = false;

        if (!m_Finished && !m_GameOver)
            playObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartNewGame();
        }
    }

    void WaitToPlayNextGame() {
        m_Started = false;

        if (Input.GetKeyDown(KeyCode.Space)) {
            ++m_CurrentLevelIndex;

            gameCompleteObject.SetActive(false);

            WaitToStartNewGame();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void EndCurrentGame() {
        m_GameOver = true;

        gameOverObject.SetActive(true);

        ball.GetComponent<Ball>().StopMoving();
        ball.GetComponent<Ball>().ResetPosition();

        SaveManager.Instance.HighScore = m_Points;
        SaveManager.Instance.Save();
    }

    public void CompleteCurrentGame() {
        m_Finished = true;

        gameCompleteObject.SetActive(true);

        ball.GetComponent<Ball>().StopMoving();
        ball.GetComponent<Ball>().ResetPosition();
    }

    public void AddPoint(int point) {
        m_Points += point;
        scoreText.text = $"Current score: <color=yellow>{m_Points}</color>";
    }
}
