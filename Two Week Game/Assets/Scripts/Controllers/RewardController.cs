using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardController : MonoBehaviour
{
    private Npc npc;

    void OnEnable()
    {
        ScenesManager.Instance.TemporarySceneLoader += SceneLoaded;
    }

    void OnDisable()
    {
        ScenesManager.Instance.TemporarySceneLoader -= SceneLoaded;
    }

    void OnDestroy()
    {
        if (!npc)
        {
            return;
        }
        foreach (var npcComponent in npc.GetComponents<MonoBehaviour>())
        {
            npcComponent.enabled = true;
        }
        //var npcMoverTopDown = npc.GetComponent<NpcMoverTopDown>();
        //if (npcMoverTopDown)
        //{
        //    npcMoverTopDown.moveable = true;
        //}
        //var npcHealth = npc.GetComponent<Health>();
        //if (npcHealth)
        //{
        //    npcHealth.invincible = false;
        //}
    }

    private void SceneLoaded(Scene temporaryScene)
    {
        npc = FindObjectOfType<Npc>();
        foreach (var npcComponent in npc.GetComponents<MonoBehaviour>())
        {
            npcComponent.enabled = false;
        }
        var npcRenderer = npc.GetComponent<Renderer>();
        if (npcRenderer)
        {
            npcRenderer.enabled = true;
        }
        //var npcMoverTopDown = npc.GetComponent<NpcMoverTopDown>();
        //if (npcMoverTopDown)
        //{
        //    npcMoverTopDown.moveable = false;
        //}
        //var npcHealth = npc.GetComponent<Health>();
        //if (npcHealth)
        //{
        //    npcHealth.invincible = true;
        //}
    }
}