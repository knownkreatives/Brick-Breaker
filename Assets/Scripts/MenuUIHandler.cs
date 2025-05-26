using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour {
    public TMP_InputField playerNameInputText;
    public TextMeshProUGUI playerNameText;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI prevScoreText;

    public TMP_InputField clanNameInputText;
    public TextMeshProUGUI clanNameText;

    public TextMeshProUGUI clanStatsText;
    public TextMeshProUGUI currentScoreText;


    void Awake() {
        LoadUserData();
    }

    public void SaveUserData() {
        var data = SaveManager.Instance.LoadPlayer("saves", playerNameInputText.text);

        data = new() {
            Name = playerNameInputText.text ?? data.Name,
            HighScore = data.HighScore,
            PreviousScore = data.PreviousScore,
            ClanName = clanNameInputText.text
        };

        SaveManager.Instance.SavePlayer("saves", data);
    }

    public void LoadUserData() {
        var data = SaveManager.Instance.LoadPlayer("saves", playerNameInputText.text);

        playerNameText.text = $"Name: {data.Name}";
        highScoreText.text = $"High Score: {data.HighScore}";
        prevScoreText.text = $"Previous Score: {data.PreviousScore}";
        clanNameText.text = data.ClanName != "" ? $"Clan: {data.ClanName}" : "You are not in any clan!";
    }

    public void ShowClanStats() {
        string result = "";

        clanStatsText.text = result;
    }

    public void Play() {
        SceneManager.LoadScene("Main");
    }

    public void Stop() {
        SceneManager.LoadScene("Menu");
    }

    public void Exit() {
        SaveManager.Instance.SavePlayer("saves", new() {
            Name = playerNameInputText.text,
            HighScore = int.Parse(highScoreText.text),
            PreviousScore = int.Parse(prevScoreText.text),
            ClanName = clanNameText.text
        });

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}