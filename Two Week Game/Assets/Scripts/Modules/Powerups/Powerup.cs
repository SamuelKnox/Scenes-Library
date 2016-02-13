using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Tooltip("Speed at which this Powerup will rotate before being collected")]
    [Range(0, 10)]
    public float rotationSpeed = 5.0f;

    void FixedUpdate()
    {
        transform.Rotate(new Vector2(0, rotationSpeed));
    }
}