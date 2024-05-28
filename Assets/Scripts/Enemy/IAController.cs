using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetDestination();
    }

    private void FixedUpdate()
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
        ObjectPoolerManager.ReturnObjectToPool(gameObject);
    }
}
