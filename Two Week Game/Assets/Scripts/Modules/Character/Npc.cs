using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Health))]
public class Npc : MonoBehaviour
{
    [Tooltip("Particle System to be played when this NPC dies")]
    public ParticleSystem deathEffect;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        health.DamageDealer += DamageDealt;
    }

    void OnDisable()
    {
        health.DamageDealer -= DamageDealt;
    }

    /// <summary>
    /// Kills the NPC
    /// </summary>
    public void Die()
    {
        ParticleSystemFactory.PlayParticleSystem(deathEffect, transform.position);
        Destroy(gameObject);
    }

    private void DamageDealt(Damage attacker)
    {
        if (!health.IsDead())
        {
            return;
        }
        var attackerCharacter = attacker.GetComponent<Character>();
        var defenderCharacter = GetComponent<Character>();
        if (attackerCharacter.teamType == TeamType.Player && defenderCharacter.teamType != TeamType.Player)
        {
            ScenesManager.Instance.LoadTemporaryScene(SceneNames.RewardSelection, false, 1.5f, FindObjectOfType<PlayerController>().gameObject, gameObject);
        }
        else
        {
            Die();
        }
    }
}
