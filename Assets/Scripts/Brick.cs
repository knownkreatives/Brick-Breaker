using System;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour {
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;
    private int maxHealth;
    private int currentHealth;

    void Start() {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new();
        switch (PointValue) {
            case 1:
            block.SetColor("_BaseColor", Color.white);
            maxHealth = 0;
            break;
            case 2:
            block.SetColor("_BaseColor", Color.green);
            maxHealth = 1;
            break;
            case 5:
            block.SetColor("_BaseColor", Color.blue);
            maxHealth = 2;
            break;
            case 10:
            block.SetColor("_BaseColor", new Color(0.45f, 0, 0.90f));
            maxHealth = 3;
            break;
            case 20:
            block.SetColor("_BaseColor", Color.yellow);
            maxHealth = 4;
            break;
            default:
            block.SetColor("_BaseColor", Color.gray);
            maxHealth = 0;
            break;
        }

        currentHealth = maxHealth;

        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionExit(Collision other) {
        if (currentHealth < 1) {
            onDestroyed.Invoke(PointValue);

            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        } else {
            currentHealth--;

            transform.localScale = new Vector3(transform.localScale.x * (currentHealth / maxHealth) + 0.1f, transform.localScale.y, transform.localScale.z);
        }
    }
}
