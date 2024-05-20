using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.SetDestination(GameObject.Find("Target").transform.position);
    }

    private void OnEnable()
    {
        agent.SetDestination(GameObject.Find("Target").transform.position);
    }
}
