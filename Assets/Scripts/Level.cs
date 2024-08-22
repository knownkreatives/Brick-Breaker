using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "level", menuName = "Level", order = 1)]
public class Level : ScriptableObject {
    public UnityEvent onCompleted;

    public Brick brickPrefab;
    public LineOfBricks [] formation;

    [HideInInspector]
    public List<GameObject> m_Bricks;

    private void Awake() {
        m_Bricks = new List<GameObject>();
    }

    Brick AddBrick(Vector3 position) {
        Brick brick = Instantiate(brickPrefab, position, Quaternion.identity);
        m_Bricks.Add(brick.gameObject);
        CheckIfNoBricksRemian();
        return brick;
    }
    void RemoveBrick(GameObject brick) {
        m_Bricks.Remove(brick);
        CheckIfNoBricksRemian();
    }

    public void CheckIfNoBricksRemian() {
        if (m_Bricks.Count == 0)
            onCompleted?.Invoke();
    }

    public void BuildBricks() {
        var lineCount = formation.Length;

        for (int i = 0; i < lineCount; ++i) {
            var brickCount = formation [i].bricks.Length;

            for (int j = 0; j < brickCount; ++j) {
                Brick brick = AddBrick(new Vector3(formation [i].bricks [j].xPosition, formation [i].yPosition));

                brick.PointValue = formation [i].bricks [j].points;

                brick.onDestroyed.AddListener(MainManager.Instance.AddPoint);
                brick.onDestroyed.AddListener(p => RemoveBrick(brick.gameObject));
            }
        }
    }

    [Serializable]
    public class LineOfBricks {
        [HideInInspector]
        public int TotalBrickCount;

        public PointsForBricks [] bricks;
        public float yPosition;
    }

    [Serializable]
    public class PointsForBricks {
        public int points;
        public float xPosition;
    }
}
