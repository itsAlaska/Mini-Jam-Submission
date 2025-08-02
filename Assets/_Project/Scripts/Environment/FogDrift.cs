using UnityEngine;

public class FogDrift : MonoBehaviour
{
    [Tooltip("Minimum drift speed.")]
    public float minSpeed = 0.01f;

    [Tooltip("Maximum drift speed.")]
    public float maxSpeed = 0.05f;

    [Tooltip("Direction of movement (normalized).")]
    public Vector2 driftDirection = Vector2.right;

    private float driftSpeed;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        driftSpeed = Random.Range(minSpeed, maxSpeed);
        Debug.Log($"driftSpeed: {driftSpeed}");
    }

    void Update()
    {
        Vector2 offset = driftDirection.normalized * driftSpeed * Time.deltaTime;
        transform.Translate(offset);
    }
}