using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private Slider healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        healthBar = GetComponentInChildren<Slider>(); // Find the Slider component
        healthBar.maxValue = maxHealth;
        UpdateHealthUI();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Called when a particle collides with this object
    /// </summary>
    /// <param name="other"></param>
    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("bullet"))
        {
            var turretController = other.GetComponentInParent<TurretController>();
            if (turretController != null) TakeDamage(turretController.Damage);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            ReturnToPool();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar)
        {            
            healthBar.value = currentHealth;
        }
    }

    private void ReturnToPool()
    {
        ObjectPoolerManager.ReturnObjectToPool(gameObject);
    }
}
