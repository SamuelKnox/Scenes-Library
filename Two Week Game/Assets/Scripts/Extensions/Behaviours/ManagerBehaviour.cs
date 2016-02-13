using UnityEngine;

/// <summary>
/// Extending this class creates a MonoBehaviour which may only have on instance and will not be destroyed between scenes.  When extending, the type of the inheriting class must be passed.
/// </summary>
public abstract class ManagerBehaviour<ManagerType> : MonoBehaviour where ManagerType : ManagerBehaviour<ManagerType>
{
    private const string ManagerName = "Manager";

    private static ManagerType instance;

    /// <summary>
    /// Gets the singleton instance of the Manager
    /// </summary>
    public static ManagerType Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ManagerType>();
                if (!instance)
                {
                    var instanceGameObject = GameObjectFactory.GetOrAddGameObject(ManagerName);
                    instance = instanceGameObject.AddComponent<ManagerType>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        DestroyDuplicateManagers();
        DontDestroyOnLoad(gameObject);
    }

    private void DestroyDuplicateManagers()
    {
        var managers = FindObjectsOfType<ManagerType>();
        foreach (var manager in managers)
        {
            if (!manager)
            {
                continue;
            }
            if (Instance != manager)
            {
                if (Instance.gameObject == manager.gameObject || manager.transform.childCount > 0)
                {
                    Destroy(manager);
                }
                else
                {
                    Destroy(manager.gameObject);
                }
            }
        }
    }
}