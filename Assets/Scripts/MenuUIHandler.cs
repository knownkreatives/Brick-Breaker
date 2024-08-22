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
    public TextMeshProUGUI prevScoreText;
    public TextMeshProUGUI clanNameText;
    public TextMeshProUGUI clanStatsText;

    public TMP_InputField playerNameInputText;
    public TMP_InputField clanNameInputText;

    void Awake() {
        LoadUserData();
    }

    public void SaveUserData() {
        SaveManager.Instance.Load(playerNameInputText.text);

        SaveManager.Instance.PlayerName = playerNameInputText.text;
        SaveManager.Instance.HighScore = SaveManager.Instance.HighScore;
        SaveManager.Instance.PrevScore = SaveManager.Instance.PrevScore;
        SaveManager.Instance.ClanName = clanNameInputText.text;

        SaveManager.Instance.Save();
    }

    public void LoadUserData() {
        SaveManager.Instance.Load(playerNameInputText.text);

        string playerName = SaveManager.Instance.PlayerName;
        int highScore = SaveManager.Instance.HighScore;
        int prevScore = SaveManager.Instance.PrevScore;
        string clanName = SaveManager.Instance.ClanName;

        playerNameText.text = $"Name: {playerName}";
        highScoreText.text = $"High Score: {highScore}";
        prevScoreText.text = $"Previous Score: {prevScore}";
        clanNameText.text = $"Clan: {clanName}";
    }

    public void ShowClanStats() {
        string result = "";

        clanStatsText.text = result;
    }

    public void Play() {
        SceneManager.LoadScene("Main");
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