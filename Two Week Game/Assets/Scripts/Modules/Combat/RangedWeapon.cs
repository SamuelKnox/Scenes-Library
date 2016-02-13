using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Character))]
public class RangedWeapon : MonoBehaviour
{
    [Tooltip("Projectile shot by this Ranged Weapon")]
    public Projectile projectile;

    [Tooltip("Speed at which the Projectile fires")]
    [Range(0, 100)]
    public float force = 10.0f;

    [Tooltip("Rate at which the RangedWeapon can fire")]
    [Range(0, 10)]
    public float fireCooldown = 1.0f;

    [Tooltip("Whether or not this RangedWeapon has unlimited range")]
    public bool unlimitedRange = false;

    [Tooltip("How far this Ranged Weapon can fire, assuming it does not have unlimited range.  It will not fire if its target is beyond this distance.")]
    [Range(1, 1000)]
    public float range = 100.0f;

    [Tooltip("Damage multiplier for Projectiles fired from this RangedWeapon")]
    [Range(0, 10)]
    public float levelDamageMultiplier = 1.1f;

    [Tooltip("Chance increase of a critical hit")]
    [Range(0, 1)]
    public float criticalHitChance = 0.0f;

    [Tooltip("Multiplier used for critical hits")]
    [Range(1, 10)]
    public float criticalHitMultiplier = 2.0f;

    [Tooltip("Position where the projectiles will spawn")]
    public Vector3 projectileSpawnOffset;

    private Character character;
    private float totalCooldown;
    private Vector3 oldRightVector;

    void Awake()
    {
        character = GetComponent<Character>();
        totalCooldown = fireCooldown;
        oldRightVector = transform.right;
    }

    void Update()
    {
        UpdateProjectileSpawnPosition();
        UpdateFireCooldown();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position + projectileSpawnOffset, GizmoNames.GizmoProjectileSpawnPointIcon);
    }

    /// <summary>
    /// Fires the equipped Projectile towards the RangedWeapon's right vector
    /// </summary>
    public bool Fire()
    {
        return Fire(transform.right);
    }

    /// <summary>
    /// Fires the equipped Projectile at the target taking gravity into account
    /// </summary>
    public bool Fire(Transform target)
    {
        return Fire(target.position - transform.position);
    }

    /// <summary>
    /// Fires the equipped Projectile in the direction
    /// </summary>
    public bool Fire(Vector2 direction)
    {
        if (!IsValidShot(direction))
        {
            return false;
        }
        direction.Normalize();
        var projectileInstance = Instantiate(projectile, transform.position + projectileSpawnOffset, Quaternion.identity) as Projectile;
        GameObjectFactory.ChildCloneToContainer(projectileInstance.gameObject);
        var projectileBody = projectileInstance.GetRigidBody2D();
        if (projectileBody.gravityScale != 0)
        {
            projectileBody.AddForce(direction * force, ForceMode2D.Impulse);
        }
        else
        {
            projectileBody.velocity = direction * force;
        }
        projectileInstance.SetDamage(character, criticalHitChance, criticalHitMultiplier, levelDamageMultiplier);
        DisableProjectileColliders(projectileInstance);
        fireCooldown = totalCooldown;
        return true;
    }

    private void DisableProjectileColliders(Projectile projectileInstance)
    {
        var rangedWeaponColliders = GetComponents<Collider2D>();
        var projectileColliders = projectileInstance.GetComponents<Collider2D>();
        if (rangedWeaponColliders.Length > 0 && projectileColliders.Length > 0)
        {
            foreach (var rangedWeaponCollider in rangedWeaponColliders)
            {
                foreach (var projectileCollider in projectileColliders)
                {
                    Physics2D.IgnoreCollision(rangedWeaponCollider, projectileCollider);
                }
            }
        }
    }

    private void UpdateProjectileSpawnPosition()
    {
        if (transform.right == oldRightVector)
        {
            return;
        }
        bool clockwiseRotation = Vector3.Cross(transform.right, oldRightVector).z > 0;
        var angleToRotate = Vector2.Angle(oldRightVector, transform.right);
        if (clockwiseRotation)
        {
            angleToRotate *= -1;
        }
        var updatedDirection = (Quaternion.AngleAxis(angleToRotate, Vector3.forward) * projectileSpawnOffset).normalized;
        projectileSpawnOffset = updatedDirection * projectileSpawnOffset.magnitude;
        oldRightVector = transform.right;
    }

    private bool IsValidShot(Vector2 direction)
    {
        if (fireCooldown > 0 || !unlimitedRange && direction.magnitude > range)
        {
            return false;
        }
        if (!projectile)
        {
            Debug.LogWarning(name + " is missing a Projectile.");
            return false;
        }
        return true;
    }

    private void UpdateFireCooldown()
    {
        fireCooldown -= Time.deltaTime;
        fireCooldown = Mathf.Max(0, fireCooldown);
    }
}
