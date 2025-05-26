using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level", menuName = "Levels", order = 1)]
public class Level : ScriptableObject {
    public Brick prefab;
    public BrickFormation formation;

    public bool IsReady {
        get; private set;
    } = false;

    public void BuildLevel(Transform parent) {
        var lineCount = formation.lines.Count;
        for (int i = 0; i < lineCount; ++i) {
            var brickCount = formation.lines [i].bricks.Count;

            for (int j = 0; j < brickCount; ++j) {
                Vector3 position = new(formation.lines [i].bricks [j].xPosition, formation.lines [i].yPosition);
                Brick brick = Instantiate(prefab, position, Quaternion.identity, parent);

                brick.SetBrickData(formation.lines [i].bricks [j].pallet);

                brick.onDestroyed.AddListener(MainManager.Instance.AddPoint);
                brick.onDestroyed.AddListener(p => brickCount--);
            }
        }

        IsReady = true;
    }

    [Serializable]
    public class BrickFormation {
        public List<LineData> lines;

        [Serializable]
        public class LineData {
            public List<BrickData> bricks;
            public float yPosition;
        }

        [Serializable]
        public class BrickData {
            public BrickPallet pallet;
            public float xPosition;
        }
    }
}
