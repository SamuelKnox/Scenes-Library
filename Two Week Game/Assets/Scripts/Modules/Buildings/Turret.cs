using UnityEngine;

[RequireComponent(typeof(RangedWeapon))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Npc))]
public class Turret : MonoBehaviour
{
    [Tooltip("How long until the turret will search for a new nearest target in seconds")]
    [Range(0, 10)]
    public float searchTimer = 1.0f;

    [Tooltip("The target which this turret will be aiming at")]
    public Character target;

    private RangedWeapon rangedWeapon;
    private Character character;
    private float totalSearchTime;

    void Awake()
    {
        rangedWeapon = GetComponent<RangedWeapon>();
        character = GetComponent<Character>();
        totalSearchTime = searchTimer;
    }

    void Update()
    {
        UpdateSearchTimer();
        FindNewTarget();
        FireAtTarget();
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
        target = character.FindNearestNotTeammate();
        searchTimer = totalSearchTime;
    }

    private void FireAtTarget()
    {
        if (!target)
        {
            return;
        }
        rangedWeapon.Fire(target.transform);
    }
}