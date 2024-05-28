using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent, RequireComponent(typeof(Slider))]
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
        healthBar = GetComponent<Slider>(); // Find the Slider component
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
            if (other.TryGetComponent<Damage>(out var damageComponent))
            {
                TakeDamage(damageComponent.GetDamage);
            }
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
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void ReturnToPool()
    {
        ObjectPoolerManager.ReturnObjectToPool(gameObject);
    }
}
