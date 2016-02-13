using UnityEngine;

[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Tooltip("Particle System to be played when Damage is dealt by this projectile")]
    public ParticleSystem damageEffect;

    private Damage damage;
    private Rigidbody2D body2D;

    void Awake()
    {
        damage = GetComponent<Damage>();
        body2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        damage.DamageDealer += DealtDamage;
    }

    void OnDisable()
    {
        damage.DamageDealer -= DealtDamage;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the Damage for this Projectile
    /// </summary>
    public void SetDamage(Character character, float criticalHitChance, float criticalHitMultiplier, float levelDamageMultiplier)
    {
        damage.SetDamage(character, criticalHitChance, criticalHitMultiplier, levelDamageMultiplier);
    }

    /// <summary>
    /// Gets the Rigidbody2D for the Projectile
    /// </summary>
    public Rigidbody2D GetRigidBody2D()
    {
        return body2D;
    }

    private void DealtDamage(Health defender)
    {
        if (damageEffect)
        {
            ParticleSystemFactory.PlayParticleSystem(damageEffect, transform.position);
        }
        Destroy(gameObject);
    }
}