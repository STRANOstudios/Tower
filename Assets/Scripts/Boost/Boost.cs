using UnityEngine;

public class Boost : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private BoostType type;
    [SerializeField, Range(0f, 5f)] private float boostValue = 2f;

    public enum BoostType
    {
        Speed,
        Damage
    }

    public BoostType Type => type;
    public float BoostValue => boostValue;
}
