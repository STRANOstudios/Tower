using UnityEngine;

public class MoveableObejct : MonoBehaviour, IPickable
{
    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}
