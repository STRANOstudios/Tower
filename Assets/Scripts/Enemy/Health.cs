using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent, RequireComponent(typeof(Slider))]
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] Slider HealthBar;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        HealthBar.maxValue = currentHealth;
        HealthBar.value = currentHealth;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        HealthBar.value = currentHealth;
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("bullet"))
        {
            TakeDamage(other.GetComponent<Damage>().GetDamage);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            ObjectPoolerManager.ReturnObjectsToPool(gameObject);
        }
    }
}
