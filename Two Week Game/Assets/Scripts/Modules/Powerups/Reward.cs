using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Reward : MonoBehaviour
{
    [Tooltip("Chance that the enemy will be charmed")]
    [Range(0, 1)]
    public float charmChance = 0.0f;

    [Tooltip("Amount of gold gained by this reward")]
    [Range(0, 1000)]
    public float goldIncrease = 0.0f;

    [Tooltip("Amount of experience gained by this reward")]
    [Range(0, 1000)]
    public float experienceIncrease = 0.0f;

    private Health health;
    private Npc npcDefeated;
    private PlayerController player;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        health.DamageDealer += GrantReward;
        ScenesManager.Instance.TemporarySceneLoader += SceneLoaded;
    }

    void OnDisable()
    {
        health.DamageDealer -= GrantReward;
        ScenesManager.Instance.TemporarySceneLoader -= SceneLoaded;
    }

    private void SceneLoaded(Scene temporaryScene)
    {
        npcDefeated = FindObjectOfType<Npc>();
        player = FindObjectOfType<PlayerController>();
    }

    private void GrantReward(Damage damage)
    {
        ApplyCharmReward();
        ApplyGoldReward(damage);
        ApplyExperienceReward(damage);
        ScenesManager.Instance.UnloadTemporaryScene();
    }

    private void ApplyCharmReward()
    {
        if (npcDefeated)
        {
            var npcCharacter = npcDefeated.GetComponent<Character>();
            if (npcCharacter)
            {
                if (Random.Range(0.0f, 1.0f) <= charmChance)
                {
                    npcCharacter.teamType = TeamType.Player;
                    var npcHealth = npcDefeated.GetComponent<Health>();
                    if (npcHealth)
                    {
                        npcHealth.currentHitPoints = npcHealth.maxHitPoints;
                    }
                }
                else
                {
                    npcDefeated.Die();
                }
            }
        }
    }

    private void ApplyGoldReward(Damage attacker)
    {
        if (player)
        {
            var playerInventory = player.GetComponent<Inventory>();
            if (playerInventory)
            {
                playerInventory.gold += goldIncrease;
            }
        }
    }

    private void ApplyExperienceReward(Damage attacker)
    {
        if (player)
        {
            var playerCharacter = player.GetComponent<Character>();
            if (playerCharacter)
            {
                playerCharacter.AddExperience(experienceIncrease);
            }
        }
    }
}
