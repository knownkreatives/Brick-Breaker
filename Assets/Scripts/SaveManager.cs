using System.Collections;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

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

    public string PlayerName {
        get => n;
        set => n = value;
    }
    string n;
    public int HighScore {
        get => hs;
        set => hs = value > hs ? value : hs;
    }
    int hs;
    public int PrevScore {
        get => ps;
        set => ps = value;
    }
    int ps;
    public string ClanName {
        get => cn;
        set => cn = value;
    }
    string cn;

    [Serializable]
    class PlayerData {
        public string name;
        public int highScore;
        public int previousScore;
        public string clanName;
    }

    class SaveData {
        public PlayerData[] players;
    }

    public void Save() {
        PlayerData p_data = new() {
            name = PlayerName,
            highScore = HighScore,
            previousScore = PrevScore,
            clanName = ClanName
        };

        SaveData data = new() {
            players = new PlayerData[] { p_data }
        };

        string path = Application.persistentDataPath + "/SaveFile.json";

        string json = JsonConvert.SerializeObject(data);

        File.WriteAllText(path, json);

        Debug.Log(path + ":\n" + json);
    }

    public void Load(string name) {
        string path = Application.persistentDataPath + $"/SaveFile.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject(json, typeof(SaveData));

            // SaveData player = Array.Find(data, (x) => x.name == name);

            //if (player != null) {
            //    PlayerName = player.name;
            //    HighScore = player.highScore;
            //    PrevScore = player.previousScore;
            //    ClanName = player.clanName;
            //} else {
            //    Debug.LogError(path + " does not contain player data");

            //    PlayerName = "";
            //    HighScore = 0;
            //    PrevScore = 0;
            //    ClanName = "";
            //}

            Debug.Log(data);
        }
        else {
            Debug.LogError(path + " does not exist");

            PlayerName = "";
            HighScore = 0;
            PrevScore = 0;
            ClanName = "";
        }
    }
}