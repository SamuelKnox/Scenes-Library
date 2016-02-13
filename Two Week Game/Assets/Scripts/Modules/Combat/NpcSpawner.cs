using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [Tooltip("Rate at which enemies spawn in seconds")]
    [Range(0, 60)]
    public float spawnRate = 1.0f;

    [Tooltip("Radius of circle on which enemies will be spawned")]
    [Range(0, 100)]
    public float spawnCircleRadius = 10.0f;

    [Tooltip("NPC which can be spawned")]
    public Npc[] npcsSpawned;

    private float totalSpawnRate;

    void Awake()
    {
        totalSpawnRate = spawnRate;
    }

    void Update()
    {
        UpdateSpawnRate();
        if (spawnRate <= 0)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemyPosition = ((Vector2)transform.position).GetRandomPositionOnCircle(spawnCircleRadius);
        var enemyInstance = Instantiate(npcsSpawned[Random.Range(0, npcsSpawned.Length)], enemyPosition, Quaternion.identity) as Npc;
        GameObjectFactory.ChildCloneToContainer(enemyInstance.gameObject);
        spawnRate = totalSpawnRate;
    }

    private void UpdateSpawnRate()
    {
        spawnRate -= Time.deltaTime;
        spawnRate = Mathf.Max(0, spawnRate);
    }
}
