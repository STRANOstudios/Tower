using UnityEngine;

[DisallowMultipleComponent]
public class Damage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField, Min(1f)] private float damage = 1;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField, Min(1f)] private float radius = 2f;

    private float damageBackup;
    private Vector3 spherePosition;

    private void Awake()
    {
        damageBackup = damage;
    }

    private void FixedUpdate()
    {
        UpdateSpherePosition();
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, radius, targetLayer);

        foreach (Collider col in hitColliders)
        {
            Boost boost = col.GetComponent<Boost>();
            if (boost != null && boost.Type == Boost.BoostType.Damage)
            {
                damage = boost.BoostValue * damageBackup;
                break;
            }
        }
    }

    private void UpdateSpherePosition()
    {
        Vector3 objectPosition = transform.position;
        spherePosition = objectPosition + transform.forward * -1f;
    }

    public float GetDamage => damage;

    private void OnDrawGizmosSelected()
    {
        UpdateSpherePosition();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, radius);
    }
}
