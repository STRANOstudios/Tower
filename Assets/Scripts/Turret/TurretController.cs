using UnityEngine;

[DisallowMultipleComponent]
public class TurretController : MonoBehaviour
{
    State currentState;
    [SerializeField] Transform barrel;

    public void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState = currentState.Process();
        }
    }

    public void OnDragging()
    {
        currentState = new Dragging(gameObject, barrel);
    }

    public void ResetState()
    {
        currentState = new Idle(gameObject, barrel);
    }
}
