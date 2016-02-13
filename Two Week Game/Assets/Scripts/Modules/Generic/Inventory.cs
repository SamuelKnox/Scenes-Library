using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("Amount of gold stored in inventory")]
    [Range(0, 1000)]
    public float gold;
}