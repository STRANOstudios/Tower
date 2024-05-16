using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent, RequireComponent(typeof(Slider))]
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] Slider HealthBar;

    private void Awake()
    {
        HealthBar.maxValue = maxHealth;
        HealthBar.value = maxHealth;
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
        maxHealth -= damage;
        HealthBar.value = maxHealth;

        Debug.Log(HealthBar.value);

        if(maxHealth <= 0) this.gameObject.SetActive(false);
    }
}
