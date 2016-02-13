using UnityEngine;

[RequireComponent(typeof(Character))]
public class Health : MonoBehaviour
{
    public delegate void DamageDealt(Damage damage);
    public event DamageDealt DamageDealer;

    [Tooltip("Hit points remaining")]
    [Range(0, 1000)]
    public float currentHitPoints = 100.0f;

    [Tooltip("Maximum hit points allowed")]
    [Range(0, 1000)]
    public float maxHitPoints = 100.0f;

    [Tooltip("Multiplier for maximum hit points based on level")]
    [Range(1, 10)]
    public float levelMultiplier = 1.1f;

    [Tooltip("Whether or not this GameObject is invincible")]
    public bool invincible = false;

    [Tooltip("How long the Health is invincible in seconds after taking Damage")]
    public float invincibleTime = 1.0f;

    [Tooltip("Whether or not Damage behaves as though damage was dealt even if it is still under the invincibility timer")]
    public bool fakeDamageDealt = true;

    private Character character;
    private float maxInvincibilityTime;

    void Awake()
    {
        character = GetComponent<Character>();
        maxInvincibilityTime = invincibleTime;
    }

    void Update()
    {
        UpdateInvincibilityTime();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damage = other.GetComponent<Damage>();
        if (damage && !invincible)
        {
            AddDamage(damage);
        }
    }

    void OnValidate()
    {
        currentHitPoints = Mathf.Min(currentHitPoints, maxHitPoints);
    }

    void OnEnable()
    {
        character.LevelChange += ChangeLevel;
    }

    void OnDisable()
    {
        character.LevelChange -= ChangeLevel;
    }

    /// <summary>
    /// Checks if the Health has run out of current hit points
    /// </summary>
    public bool IsDead()
    {
        return currentHitPoints <= 0;
    }

    private void ChangeLevel(int levelChange)
    {
        while (levelChange > 0)
        {
            maxHitPoints *= levelMultiplier;
            levelChange--;
        }
        while (levelChange < 0)
        {
            maxHitPoints /= levelMultiplier;
            levelChange++;
        }
        currentHitPoints = maxHitPoints;
    }

    private void AddDamage(Damage damage)
    {
        var damageCharacter = damage.GetComponent<Character>();
        if (!damageCharacter)
        {
            Debug.LogError(damage.name + " is missing a Character!");
        }
        bool validTeamCombination = character.teamType != damageCharacter.teamType || character.teamType == TeamType.Neutral || damageCharacter.teamType == TeamType.Neutral;

        if ((invincibleTime <= 0 || fakeDamageDealt) && (validTeamCombination || GameManager.Instance.friendlyFire))
        {
            if (invincibleTime <= 0)
            {
                currentHitPoints -= damage.GetDamage();
                ClampHealth();
                invincibleTime = maxInvincibilityTime;
            }
            if (DamageDealer != null)
            {
                DamageDealer(damage);
            }
            damage.DealtDamage(this);
        }
    }

    private void ClampHealth()
    {
        currentHitPoints = Mathf.Clamp(currentHitPoints, 0, maxHitPoints);
    }

    private void UpdateInvincibilityTime()
    {
        invincibleTime -= Time.deltaTime;
        invincibleTime = Mathf.Max(0, invincibleTime);
    }
}