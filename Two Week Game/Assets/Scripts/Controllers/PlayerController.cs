using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerMoverTopDown))]
[RequireComponent(typeof(RangedWeapon))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Builder))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Force at which player is knocked back when taking damage")]
    [Range(0, 100)]
    public float knockBackForce = 5.0f;

    private PlayerMoverTopDown characterMoverTopDown;
    private RangedWeapon rangedWeapon;
    private Health health;
    private Builder builder;

    void Awake()
    {
        characterMoverTopDown = GetComponent<PlayerMoverTopDown>();
        rangedWeapon = GetComponent<RangedWeapon>();
        health = GetComponent<Health>();
        builder = GetComponent<Builder>();
        characterMoverTopDown.onControllerCollidedEvent += onControllerCollider;
        characterMoverTopDown.onTriggerEnterEvent += onTriggerEnterEvent;
        characterMoverTopDown.onTriggerStayEvent += onTriggerStayEvent;
        characterMoverTopDown.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Update()
    {
        characterMoverTopDown.Move(Input.GetAxis(InputNames.Horizontal), Input.GetAxis(InputNames.Vertical));
        if (Input.GetButton(InputNames.Fire))
        {
            rangedWeapon.Fire();
        }
        if (Input.GetButtonDown(InputNames.Build))
        {
            builder.Build();
        }
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        //Debug.Log("onControllerCollider: " + hit.transform.gameObject.name);
    }


    void onTriggerEnterEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerEnterEvent: " + collider2D.gameObject.name);
    }

    void onTriggerStayEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerStayEvent: " + collider2D.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerExitEvent: " + collider2D.gameObject.name);
    }

    void OnEnable()
    {
        health.DamageDealer += TookDamage;
    }

    void OnDisable()
    {
        health.DamageDealer -= TookDamage;
    }

    private void TookDamage(Damage damage)
    {
        var direction = (transform.position - damage.transform.position).normalized * knockBackForce;
        characterMoverTopDown.AddForce(direction);
    }
}