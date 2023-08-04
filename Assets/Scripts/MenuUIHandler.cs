using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour {
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI highScoreText;
    public TMP_InputField playerNameInputText;

    void Awake() {
        LoadNameAndScore();
    }

    public void SaveData() {
        SaveManager.Instance.SetPlayerName(playerNameInputText.text);
        SaveManager.Instance.SetHighScore(0);
        SaveManager.Instance.Save();
    }

    public void LoadNameAndScore() {
        SaveManager.Instance.LoadData();
        var playerName = SaveManager.Instance.GetPlayername();
        var highScore = SaveManager.Instance.GetHighScore();

        playerNameText.text = $"Name: {playerName}";
        highScoreText.text = $"High Score: {highScore}";
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Exit() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif

        SaveManager.Instance.Save();
    }
}