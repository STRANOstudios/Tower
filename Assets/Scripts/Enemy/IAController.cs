using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 target;

    public delegate void TouchDown();
    public static event TouchDown InTheTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetDestination();
    }

    private void Update()
    {
        // Check if the agent has reached its destination
        if (agent.remainingDistance <= 1)
        {
            ReturnToPool();
        }
    }

    private void OnEnable()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        target = GameObject.Find("Target").transform.position;
        if (target != null)
        {
            agent.SetDestination(target);
        }
    }

    private void ReturnToPool()
    {
        InTheTarget?.Invoke();
        ObjectPoolerManager.ReturnObjectToPool(gameObject);
    }
}
