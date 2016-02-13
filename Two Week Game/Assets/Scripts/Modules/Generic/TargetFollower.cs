using UnityEditor;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [Tooltip("Target which will be followed")]
    public Transform target;

    [Tooltip("Which axes to mimic the target's position on")]
    public PositionFollowing positionFollowing;

    [Tooltip("Which axes to mimic the target's rotation on")]
    public RotationFollowing rotationFollowing;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    void Awake()
    {
        if (!target)
        {
            Debug.LogError(name + " requires a Transform target to follow!");
        }
        positionOffset = transform.position - target.transform.position;
        rotationOffset = transform.rotation.eulerAngles - target.transform.rotation.eulerAngles;
    }

    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        var targetPosition = target.transform.position;
        var followerPosition = transform.position;
        float xAdjusted = targetPosition.x + positionOffset.x;
        float yAdjusted = targetPosition.y + positionOffset.y;
        float zAdjusted = targetPosition.z + positionOffset.z;
        xAdjusted = positionFollowing.xPosition ? xAdjusted : followerPosition.x;
        yAdjusted = positionFollowing.yPosition ? yAdjusted : followerPosition.y;
        zAdjusted = positionFollowing.zPosition ? zAdjusted : followerPosition.z;
        transform.position = new Vector3(xAdjusted, yAdjusted, zAdjusted);
    }

    private void UpdateRotation()
    {
        var targetRotation = target.transform.rotation.eulerAngles;
        var followerRotation = transform.rotation.eulerAngles;
        float xAdjusted = targetRotation.x + rotationOffset.x;
        float yAdjusted = targetRotation.y + rotationOffset.y;
        float zAdjusted = targetRotation.z + rotationOffset.z;
        xAdjusted = rotationFollowing.xRotation ? xAdjusted : followerRotation.x;
        yAdjusted = rotationFollowing.yRotation ? yAdjusted : followerRotation.y;
        zAdjusted = rotationFollowing.zRotation ? zAdjusted : followerRotation.z;
        transform.rotation = Quaternion.Euler(new Vector3(xAdjusted, yAdjusted, zAdjusted));
    }
}

[System.Serializable]
public class PositionFollowing
{
    [Tooltip("Whether or not to mimic the target's x position")]
    public bool xPosition = true;

    [Tooltip("Whether or not to mimic the target's y position")]
    public bool yPosition = true;

    [Tooltip("Whether or not to mimic the target's z position")]
    public bool zPosition = true;
}

[System.Serializable]
public class RotationFollowing
{
    [Tooltip("Whether or not to mimic the target's x rotation")]
    public bool xRotation = false;

    [Tooltip("Whether or not to mimic the target's y rotation")]
    public bool yRotation = false;

    [Tooltip("Whether or not to mimic the target's z rotation")]
    public bool zRotation = false;
}