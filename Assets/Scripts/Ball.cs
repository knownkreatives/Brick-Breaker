using UnityEngine;

public class Ball : MonoBehaviour {
    [Min(0)]
    public float accelerationPerBounce = 0.01f;
    [Min(0.01f)]
    public float maxSpeed = 3;
    [Min(0.01f)]
    public float startSpeed = 2;
    [Min(0)]
    public float minSpeed = 0.1f;

    public float yOffset = 0.4f;

    private Rigidbody rb;

    public bool IsMoving {
        get; private set;
    } = false;
    public bool IsDead {
        get; private set;
    } = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    float random1;
    Vector3 velocity;

    private void FixedUpdate() {
        random1 = Random.Range(-1.0f, 1.0f);
        velocity = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Brick")) {
            NormalBounce(other.contacts [0].normal);
            Accelerate();
        } else if (other.gameObject.CompareTag("Player")) {
            NormalBounce(other.contacts [0].normal);
            Accelerate();
        } else if (other.gameObject.CompareTag("DeadZone")) {
            IsDead = true;
            StopMoving();
        }

        ClampSpeed();
    }

    void NormalBounce(Vector3 normal) {
        velocity = Vector3.Reflect(velocity, normal) * velocity.magnitude;

        rb.linearVelocity = velocity;
    }

    void Accelerate() {
        velocity += velocity.normalized * accelerationPerBounce;

        rb.linearVelocity = velocity;
    }
    void ClampSpeed() {
        if (!IsMoving)
            return;

        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        } else if (velocity.magnitude < minSpeed) {
            velocity += velocity.normalized * random1;
        }

        rb.linearVelocity = velocity;
    }

    public void ResetDead() {
        IsDead = false;
    }

    public void StartMovingInRandomDirection() {
        IsMoving = true;

        Vector3 forceDir = new(random1, 1);

        rb.AddForce(forceDir.normalized * startSpeed, ForceMode.Impulse);
    }
    public void StopMoving() {
        IsMoving = false;

        rb.linearVelocity = Vector3.zero;
    }

    public void ResetToPaddleParent() {
        transform.SetParent(GameObject.FindGameObjectsWithTag("Player") [0].transform);
        transform.localPosition = new Vector3(0, yOffset, 0);
    }
    public void DetachFromPaddleParent() {
        transform.SetParent(GameObject.FindGameObjectsWithTag("Player") [0].transform.parent);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        if (rb != null)
            Gizmos.DrawLine(transform.position, transform.position + rb.linearVelocity);
    }
}
