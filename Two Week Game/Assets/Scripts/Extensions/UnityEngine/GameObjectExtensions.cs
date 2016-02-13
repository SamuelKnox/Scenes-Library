using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// If GameObject has specified Component, it returns it.  If GameObject does not have specified Component, it adds it and then returns it.
    /// </summary>
    public static ComponentType GetOrAddComponent<ComponentType>(this GameObject gameObject) where ComponentType : Component
    {
        ComponentType component = gameObject.GetComponent<ComponentType>();
        if (component == null)
        {
            component = gameObject.AddComponent(typeof(ComponentType)) as ComponentType;
        }
        return component;
    }

    /// <summary>
    /// Searches ancestors upward until it finds the first instance of the desired component and returns it.  If no ancestor has the specified component, null will be returned.  If the component itself has the desired component, then it will be returned..
    /// </summary>
    public static ComponentType GetAncestorComponent<ComponentType>(this GameObject gameObject) where ComponentType : Component
    {
        var currentAncestor = gameObject.transform;
        while (currentAncestor)
        {
            var ancestorComponent = currentAncestor.GetComponent<ComponentType>();
            if (ancestorComponent)
            {
                return ancestorComponent;
            }
            currentAncestor = currentAncestor.parent;
        }
        return null;
    }
}