using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Inventory))]
public class Builder : MonoBehaviour
{
    [Tooltip("Building to be spawned")]
    public Building building;

    [Tooltip("Offset from Builder of where Building will be Instantiated")]
    [Range(0, 25)]
    public float buildingOffset = 5.0f;

    private Character character;
    private Inventory inventory;

    void Awake()
    {
        character = GetComponent<Character>();
        inventory = GetComponent<Inventory>();
    }

    /// <summary>
    /// Builds the building
    /// </summary>
    public void Build()
    {
        if (inventory.gold < building.cost)
        {
            return;
        }
        inventory.gold -= building.cost;
        var buildingInstance = Instantiate(building, transform.position + transform.right * buildingOffset, Quaternion.identity) as Building;
        GameObjectFactory.ChildCloneToContainer(buildingInstance.gameObject);
        if (buildingInstance)
        {
            var buildingCharacter = buildingInstance.GetComponent<Character>();
            buildingCharacter.Copy(character);
        }
    }
}
