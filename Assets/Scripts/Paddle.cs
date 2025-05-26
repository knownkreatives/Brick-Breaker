using UnityEngine;

public class Paddle : MonoBehaviour {
    [Min(0)]
    public float speed = 2.0f;
    [Min(0)]
    public float moveRange = 2.0f;

    public float xOffset = 0;
    bool canMove = false;

    void Update() {
        if (!canMove)
            return;
        Move();
    }
    void Move() {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * speed * Time.deltaTime;

        if (pos.x > moveRange + xOffset - transform.localScale.x / 2)
            pos.x = moveRange + xOffset - transform.localScale.x / 2;
        else if (pos.x < -moveRange + xOffset + transform.localScale.x / 2)
            pos.x = -moveRange + xOffset + transform.localScale.x / 2;

        transform.position = pos;
    }

    public void StartMoving() {
        canMove = true;
    }
    public void StopMoving() {
        canMove = false;
    }

    public void ResetPosition() {
        transform.position = Vector3.zero;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(xOffset - moveRange, transform.position.y), new Vector3(xOffset + moveRange, transform.position.y));
    }
}
