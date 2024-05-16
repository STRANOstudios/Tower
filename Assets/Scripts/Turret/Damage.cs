using UnityEngine;

[DisallowMultipleComponent]
public class Damage : MonoBehaviour
{
    [SerializeField, Min(1f)] private float damage = 1;

    public float GetDamage => damage;
}
