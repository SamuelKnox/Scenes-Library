using UnityEngine;

[RequireComponent(typeof(NpcMoverTopDown))]
[RequireComponent(typeof(Character))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("How long until the enemy will search for a new nearest target in seconds")]
    [Range(0, 10)]
    public float searchTimer = 1.0f;
    
    private NpcMoverTopDown npcMoverTopDown;
    private Character character;
    private float totalSearchTime;

    void Awake()
    {
        npcMoverTopDown = GetComponent<NpcMoverTopDown>();
        character = GetComponent<Character>();
        totalSearchTime = searchTimer;
    }

    void Update()
    {
        UpdateSearchTimer();
        FindNewTarget();
    }

    private void UpdateSearchTimer()
    {
        searchTimer -= Time.deltaTime;
        searchTimer = Mathf.Max(0, searchTimer);
    }

    private void FindNewTarget()
    {
        if (searchTimer > 0)
        {
            return;
        }
        var target = character.FindNearestNotTeammate();
        if (target)
        {
            npcMoverTopDown.Chase(target.transform);
            searchTimer = totalSearchTime;
        }
    }
}