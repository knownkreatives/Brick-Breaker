using UnityEngine;

public class Ball : MonoBehaviour {
    [Range(0, 1)]
    public float accelerationPerBounce = 0.01f;
    [Min(0.01f)]
    public float maxVelocity = 3;

    Vector3 originalPosition;

    private Rigidbody m_Rigidbody;

    void Start() {
        originalPosition = transform.position; 
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionExit(Collision other) {
        var velocity = m_Rigidbody.linearVelocity;

        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 1f) {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        // after a collision we accelerate a bit
        velocity += velocity.normalized * accelerationPerBounce;

        // max velocity
        if (velocity.magnitude > maxVelocity) {
            velocity = velocity.normalized * maxVelocity;
        }

        m_Rigidbody.linearVelocity = velocity;
    }

    public void StartMovingInRandomDirection() {
        float randomDirection = Random.Range(-1, 1);
        Vector3 forceDir = new (randomDirection, 1, 0);

        forceDir.Normalize();

        m_Rigidbody.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    public void StopMoving() {
        m_Rigidbody.linearVelocity = Vector3.zero;
    }

    public void ResetPosition() {
        transform.SetParent(GameObject.Find("Paddle").transform);
        transform.position = m_Rigidbody.position;
    }
}
