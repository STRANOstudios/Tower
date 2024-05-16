using UnityEngine;

[DisallowMultipleComponent]
public class TurretController : MonoBehaviour
{
    State currentState;
    [SerializeField] Transform barrel;

    public void Start()
    {
        currentState = new Idle(gameObject, barrel);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState = currentState.Process();
        }
    }
}
