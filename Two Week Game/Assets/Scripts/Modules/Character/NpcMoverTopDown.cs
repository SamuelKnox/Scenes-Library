using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NpcMoverTopDown : MonoBehaviour
{
    [Tooltip("The target which this enemy will be focused on")]
    public Transform target;

    [Tooltip("Range where NPC will start to chase the target")]
    [Range(0, 100)]
    public float minChaseRange = 5.0f;

    [Tooltip("Range where NPC will stop chasing the target")]
    [Range(0, 100)]
    public float maxChaseRange = 25.0f;

    [Tooltip("Speed of this enemy")]
    [Range(0, 25)]
    public float speed = 1.0f;

    [Tooltip("Whether or not this NPC will move")]
    public bool moveable = true;

    private Rigidbody2D body2D;

    void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ChaseTarget();
    }

    /// <summary>
    /// Moves the NPC in a direction for a frame
    /// </summary>
    public void Move(float horizontalMovement, float verticalMovement)
    {
        Move(new Vector2(horizontalMovement, verticalMovement));
    }

    /// <summary>
    /// Moves the NPC in a direction for a frame
    /// </summary>
    public void Move(Vector2 movement)
    {
        if (!moveable)
        {
            return;
        }
        body2D.AddForce(movement * speed);
    }

    /// <summary>
    /// Sets the NPC's velocity to move towards the target
    /// </summary>
    /// <param name="target"></param>
    public void MoveTowards(Vector2 target)
    {
        if (!moveable)
        {
            return;
        }
        body2D.velocity = (target - (Vector2)transform.position).normalized * speed;
    }

    /// <summary>
    /// Has the NPC start following the target while the target is within range
    /// </summary>
    public void Chase(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// Stops the NPC from following the target
    /// </summary>
    public void StopChasing()
    {
        Chase(null);
    }

    private void ChaseTarget()
    {

        if (!target || Vector2.Distance(transform.position, target.position) < minChaseRange || Vector2.Distance(transform.position, target.position) > maxChaseRange)
        {
            body2D.velocity = Vector2.zero;
            return;
        }
        MoveTowards(target.position);
    }
}