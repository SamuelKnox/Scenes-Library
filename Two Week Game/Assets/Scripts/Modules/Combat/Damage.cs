using UnityEngine;

[RequireComponent(typeof(Character))]
public class Damage : MonoBehaviour
{
    public delegate void DamageDealt(Health health);
    public event DamageDealt DamageDealer;

    [Tooltip("Base damage dealt")]
    [Range(0, 100)]
    public float damagePoints = 10.0f;

    [Tooltip("Randomness of damage dealt")]
    [Range(0, 1)]
    public float randomness = 0.1f;

    private Character character;

    void Awake()
    {
        character = GetComponent<Character>();
    }

    /// <summary>
    /// To be called when Damage is dealt to Health
    /// </summary>
    public void DealtDamage(Health health)
    {
        if (DamageDealer != null)
        {
            DamageDealer(health);
        }
    }

    /// <summary>
    /// Gets the damage value dealt by this Damage given its multipliers and critical hit chance
    /// </summary>
    public float GetDamage()
    {
        float randomModifier = 1.0f - randomness + Random.Range(0.0f, randomness * 2.0f);
        return damagePoints * randomModifier;
    }

    /// <summary>
    /// Sets the damage and character for this Damage component
    /// </summary>
    public void SetDamage(Character character, float criticalHitChance, float criticalHitMultiplier, float levelDamageMultiplier)
    {
        this.character.Copy(character);
        if (Random.Range(0.0f, 1.0f) <= criticalHitChance)
        {
            damagePoints *= criticalHitMultiplier;
        }
        int level = character.level;
        while (level > 1)
        {
            damagePoints *= levelDamageMultiplier;
            level--;
        }
    }
}