using System.Collections;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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

    public void SavePlayer(string setpath, PlayerData data) {
        string fullpath = Application.persistentDataPath + $"/{setpath}/{data.Name}.json";

        File.WriteAllText(fullpath, JsonConvert.SerializeObject(data));

        Debug.Log($"{fullpath}:\n\t{JsonConvert.SerializeObject(data)}");
    }

    public PlayerData LoadPlayer(string getpath, string name) {
        string fullpath = Application.persistentDataPath + $"/{getpath}/{name}.json";

        if (File.Exists(fullpath)) {
            PlayerData data = (PlayerData)JsonConvert.DeserializeObject(File.ReadAllText(fullpath), typeof(PlayerData));

            Debug.Log($"{fullpath}:\n\t{data}");

            return data;
        }

        return null;
    }

    public void SavePlayers(string setpath, SaveData data) {
        foreach (var item in data) {
            SavePlayer(setpath, item);
        }
    }

    public SaveData LoadPlayers(string getpath) {
        SaveData data = new();

        string fullpath = Application.persistentDataPath + $"/{getpath}";
        string [] files = Directory.GetFiles(fullpath, "*.json");

        foreach (var file in files) {
            data.Add(LoadPlayer(getpath, Path.GetFileName(file)));
        }

        data.OrderByDescending(data => data.HighScore);

        return data;
    }
}

[Serializable]
public class PlayerData {
    public string @Name { get; set; }
    public int HighScore { get => highscore; set => highscore = value > highscore ? highscore : value; } int highscore;
    public int PreviousScore { get; set; }
    public string ClanName { get; set; }

    public PlayerData() {

    }

    public PlayerData(string name, int highScore) {
        @Name = name;
        HighScore = highScore;
        PreviousScore = 0;
        ClanName = "";
    }
}

public class SaveData : ICollection<PlayerData> {
    readonly PlayerData [] _players;

    public int Count => _players.Length;

    public bool IsReadOnly => _players.IsReadOnly;

    public void Add(PlayerData item) {
        if (item == null) throw new ArgumentNullException("item");
        _players.Append(item);
    }

    public void Clear() {
        _players.ToList().Clear();
    }

    public bool Contains(PlayerData item) {
        if (item == null) throw new ArgumentNullException("item");
        return _players.Contains(item);
    }

    public void CopyTo(PlayerData [] array, int arrayIndex) {
        if (array == null) throw new ArgumentNullException("array");
        _players.CopyTo(array, arrayIndex);
    }

    public IEnumerator<PlayerData> GetEnumerator() {
        return _players.AsEnumerable().GetEnumerator();
    }

    public bool Remove(PlayerData item) {
        if (item == null) throw new ArgumentNullException("item");
        return _players.ToList().Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}