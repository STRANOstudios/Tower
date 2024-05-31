using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DockableObjectsManager : MonoBehaviour
{
    [Header("Dock Settings")]
    [SerializeField, Range(0, 10)] private int maxObjects = 3;

    private readonly Stack<GameObject> objectStack = new();

    private bool hasSpeedBoost = false;
    private bool hasDamageBoost = false;

    /// <summary>
    /// Called when an object is docked
    /// </summary>
    /// <param name="gameObject"></param>
    public bool OnDocking(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Boost>(out var boostComponent))
        {
            switch (boostComponent.Type)
            {
                case Boost.BoostType.Speed:
                    if (hasSpeedBoost) return false;
                    hasSpeedBoost = true;
                    break;
                case Boost.BoostType.Damage:
                    if (hasDamageBoost) return false;
                    hasDamageBoost = true;
                    break;
            }
            maxObjects++;
        }
        else
        {
            if (objectStack.Count >= maxObjects) return false;
        }

        objectStack.Push(gameObject);
        gameObject.transform.SetParent(transform);
        UpdateStackPositions();

        return true;
    }

    /// <summary>
    /// Called when an object is undocked
    /// </summary>
    /// <param name="gameObject"></param>
    public void OnUndocking(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Boost>(out var boostComponent))
        {
            switch (boostComponent.Type)
            {
                case Boost.BoostType.Speed:
                    hasSpeedBoost = false;
                    break;
                case Boost.BoostType.Damage:
                    hasDamageBoost = false;
                    break;
            }
            maxObjects--;
        }

        if (objectStack.Count > 0 && objectStack.Peek() == gameObject)
        {
            objectStack.Pop();
            UpdateStackPositions();
        }
    }


    private void SetBoost(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<TurretController>(out var controller))
        {
            if (hasSpeedBoost) controller.FireRatio = controller.GetFireRatio / 2;
            else controller.FireRatio = controller.GetFireRatio;

            if (hasDamageBoost) controller.Damage = controller.GetDamage * 2;
            else controller.Damage = controller.GetDamage;
        }
    }

    private void UpdateStackPositions()
    {
        float totalHeight = 0f;

        foreach (var obj in objectStack)
        {
            if (obj.TryGetComponent<Collider>(out var collider))
            {
                totalHeight += collider.bounds.size.y + .3f;
            }
        }

        foreach (var obj in objectStack)
        {
            Vector3 newPosition = new (0f, totalHeight, 0f);
            obj.transform.localPosition = newPosition;
            totalHeight -= GetObjectHeight(obj);
            SetBoost(obj);
        }
    }

    private float GetObjectHeight(GameObject obj)
    {
        if (obj.TryGetComponent<Collider>(out var collider))
        {
            return collider.bounds.size.y;
        }
        return 0f;
    }

    /// <summary>
    /// Returns true if the object has a speed boost
    /// </summary>
    public bool HasSpeedBoost => hasSpeedBoost;
    /// <summary>
    /// Returns true if the object has a damage boost
    /// </summary>
    public bool HasDamageBoost => hasDamageBoost;
}
