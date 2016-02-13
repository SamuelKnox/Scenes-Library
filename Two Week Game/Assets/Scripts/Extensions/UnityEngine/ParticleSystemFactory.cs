using UnityEngine;

public class ParticleSystemFactory
{
    /// <summary>
    /// Plays a Particle System from its prefab at a specified position, and then automatically destroys it upon completion
    /// </summary>
    public static void PlayParticleSystem(ParticleSystem particleSystemPrefab, Vector2 position)
    {
        PlayParticleSystem(particleSystemPrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// Plays a Particle System from its prefab at a specified position and rotation, and then automatically destroys it upon completion
    /// </summary>
    public static void PlayParticleSystem(ParticleSystem particleSystemPrefab, Vector2 position, Quaternion rotation)
    {
        var particleSystem = Object.Instantiate(particleSystemPrefab, position, rotation) as ParticleSystem;
        GameObjectFactory.ChildCloneToContainer(particleSystem.gameObject);
        Object.Destroy(particleSystem.gameObject, particleSystem.duration);
    }
}