using UnityEngine;

public class Building : MonoBehaviour
{
    [Tooltip("This is the cost in gold to spawn the building")]
    [Range(0, 100)]
    public float cost = 1.0f;
}