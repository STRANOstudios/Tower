using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField, Min(1f)] private float maxHealth = 100f;
    private Slider healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<Slider>(); // Find the Slider component
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void OnEnable()
    {
        IAController.InTheTarget += TakeDamage;
    }

    private void OnDisable()
    {
        IAController.InTheTarget -= TakeDamage;
    }

    private void TakeDamage()
    {
        healthBar.value -= 10f;
        if (healthBar.value <= 0)
        {
            FindObjectOfType<MenuController>().ReturnButton();
        }
    }
}
