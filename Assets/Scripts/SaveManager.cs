using System.Collections;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance {
        get; private set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    string playerName;

    public void SetPlayerName(string name) {
        playerName = name; 
    }

    public string GetPlayername() {
        return playerName;
    }

    int highScore;

    public void SetHighScore(int h_score) {
        highScore = h_score;
    }

    public int GetHighScore() {
        return highScore;
    }

    [System.Serializable]
    class SaveData {
        public string playerName;
        public int highScore;
    }

    public void Save() {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string j = File.ReadAllText(path);
            data.highScore = 
                (JsonUtility.FromJson<SaveData>(j).highScore > highScore) && (JsonUtility.FromJson<SaveData>(j).playerName == playerName) ?
                JsonUtility.FromJson<SaveData>(j).highScore:
                highScore;
        }
        else {
            data.highScore = highScore;
        }


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log(Application.persistentDataPath + "/savefile.json" + json);
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
        }
        else {
            Debug.LogError(path + " does not exist");

            playerName = "";
            highScore = 0;
        }
    }
}