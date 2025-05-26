using UnityEngine;

public class MainManager : MonoBehaviour {
    public static MainManager Instance {
        get; private set;
    }
    public int CurrentPoints { get; private set; } = 0;
    public int CurrentLevelIndex { get; set; } = 0;

    private void Start() {
        Application.targetFrameRate = 5;

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Level [] levels;
    public Ball ball;
    public Paddle paddle;
    public Transform bricksParent;

    public void AddPoint(int points) {
        CurrentPoints += points;
    }
    public void RemovePoint(int points) {
        CurrentPoints -= points;
    }

    public void ResetData() {
        CurrentPoints = 0;
        CurrentLevelIndex = 0;
        ball.ResetDead();
    }
}
