using UnityEngine;

public class GameManager : ManagerBehaviour<GameManager>
{
    [Tooltip("Whether or not members of the same team can damage eachother")]
    public bool friendlyFire = false;
}