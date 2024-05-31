using UnityEngine;

[DisallowMultipleComponent]
public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform barrel;

    [Header("Settings")]
    [SerializeField] float attackDistance = 10f;
    [SerializeField] float visionDistance = 20f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float fireRatio = 1f;
    [SerializeField] float damage = 10f;

    private float fireRatioDefault;
    private float damageDefault;

    private State currentState;

    public void Start()
    {
        fireRatioDefault = fireRatio;
        damageDefault = damage;
        ResetState();
    }

    private void Update()
    {
        if (currentState != null) currentState = currentState.Process();

        currentState.FireRatio = fireRatio;
    }

    /// <summary>
    /// Set the state.
    /// </summary>
    public void OnDragging()
    {
        currentState = new Dragging(gameObject, barrel);
    }

    /// <summary>
    /// Reset the state.
    /// </summary>
    public void ResetState()
    {
        currentState = new Idle(gameObject, barrel);
    }

    /// <summary>
    /// Get the current state.
    /// </summary>
    public State GetState => currentState;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float FireRatio
    {
        get { return fireRatio; }
        set { fireRatio = value; }
    }
    public float GetDamage => damageDefault;
    public float GetFireRatio => fireRatioDefault;
}
