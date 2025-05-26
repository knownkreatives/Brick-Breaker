using System;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour {
    [HideInInspector]
    public UnityEvent<int> onDestroyed;

    int pointsOnDestroy;
    int currentHealth;

    Renderer rend;

    public void SetBrickData(BrickPallet pallet) {
        rend = GetComponent<Renderer>();

        if (pallet == null) {
            rend.sharedMaterial.color = Color.red;

            pointsOnDestroy = 0;
            currentHealth = 0;
        } else {
            rend.sharedMaterial.color = pallet.col;

            pointsOnDestroy = pallet.points;
            currentHealth = pallet.maxHealth;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ball")) {
            currentHealth--;
        }

        if (currentHealth <= 0) {
            onDestroyed?.Invoke(pointsOnDestroy);

            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        }
    }
}
