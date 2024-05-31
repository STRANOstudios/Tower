using UnityEngine;

[DisallowMultipleComponent]
public class Damage : MonoBehaviour
{
    private float damage = 1;

    public float GetDamage => damage;
}
