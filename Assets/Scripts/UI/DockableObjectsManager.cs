using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DockableObjectsManager : MonoBehaviour
{
    [Header("Dock Settings")]
    [SerializeField, Range(0, 10)] private int maxObjects = 3;

    private readonly Stack<GameObject> objectStack = new Stack<GameObject>();

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

    private void UpdateStackPositions()
    {
        float totalHeight = 0.1f;

        foreach (var obj in objectStack)
        {
            if (obj.TryGetComponent<Collider>(out var collider))
            {
                totalHeight += collider.bounds.size.y;
            }
        }

        foreach (var obj in objectStack)
        {
            Vector3 newPosition = new Vector3(0f, totalHeight, 0f);
            obj.transform.localPosition = newPosition;
            totalHeight -= GetObjectHeight(obj);
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
}
