using UnityEngine;

public class GameObjectFactory
{
    private const string CloneSuffix = "(Clone)";

    /// <summary>
    /// Returns a newly created GameObject at the bottom of a tree, or returns it if it already exists.
    /// </summary>
    public static GameObject GetOrAddGameObject(params string[] gameObjectNames)
    {
        var parent = GameObject.Find(gameObjectNames[0]);
        if (!parent)
        {
            parent = new GameObject(gameObjectNames[0]);
        }
        if (gameObjectNames.Length > 1)
        {
            for (int i = 1; i < gameObjectNames.Length; i++)
            {
                GameObject child = null;
                var childTransform = parent.transform.Find(gameObjectNames[i]);
                if (childTransform)
                {
                    child = childTransform.gameObject;
                }
                else
                {
                    child = new GameObject(gameObjectNames[i]);
                    child.transform.parent = parent.transform;
                }
                parent = child;
            }
        }
        return parent;
    }

    /// <summary>
    /// Creates a GameObject by the same name of the clone and sets the clone as a child to that GameObject.  The container GameObject will automatically be destroyed once it has no remaining clones.  Parent will be defaulted if none is provided.
    /// </summary>
    public static GameObject ChildCloneToContainer(GameObject clone, GameObject owner = null)
    {
        var containerName = clone.name.TrimEnd(CloneSuffix);
        GameObject container;
        if (owner)
        {
            var containerTransform = owner.transform.Find(containerName);
            if (containerTransform)
            {
                container = containerTransform.gameObject;
            }
            else
            {
                container = new GameObject(containerName);
                container.transform.parent = owner.transform;
                container.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            container = GetOrAddGameObject(containerName);
        }
        clone.transform.parent = container.transform;
        container.gameObject.GetOrAddComponent<DestroyedWhenChildless>();
        return container;
    }
}