using UnityEngine;

[CreateAssetMenu(fileName = "pallet", menuName = "Brick/Pallet", order = 1)]
public class BrickPallet : ScriptableObject {
    public int points;
    public int maxHealth;
    public Color col;
}