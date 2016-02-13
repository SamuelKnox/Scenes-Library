using UnityEngine;

public class TimeManager : ManagerBehaviour<TimeManager>
{
    [Tooltip("Time in seconds for each day")]
    [Range(0, 600)]
    public float dayLength = 60.0f;
}
