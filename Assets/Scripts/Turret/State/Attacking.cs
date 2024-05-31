using UnityEngine;

internal class Attacking : State
{
    readonly float rotationSpeed = 20.0f;
    float fireRatio = 0.1f;

    float timeSinceLastFire = 0f;
    float fireRatioBackup;
    private readonly ParticleSystem[] particleSystems;

    public Attacking(GameObject _turret, Transform _barrel) : base(_turret, _barrel)
    {
        name = STATE.TRACKING;

        particleSystems = _turret.GetComponentsInChildren<ParticleSystem>();
    }

    private void Awake()
    {
        fireRatioBackup = fireRatio;
    }

    public override void Enter()
    {
        for(int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
        base.Enter();
    }

    public override void Update()
    {
        if (GetClosestEnemyInArea(attackDistance))
        {
            Vector3 direction = GetClosestEnemyInArea(attackDistance).transform.position - turret.transform.position;
            Quaternion barrelRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, barrelRotation, Time.deltaTime * rotationSpeed);

            float elevationAngle = Mathf.Atan2(direction.y, direction.magnitude) * Mathf.Rad2Deg;

            barrel.localRotation = Quaternion.Euler(-elevationAngle, 0f, 0f);

            if (Time.time - timeSinceLastFire > 1 / fireRatio)
            {
                timeSinceLastFire = Time.time;
                for (int i = 0; i < particleSystems.Length; i++)
                {
                    particleSystems[i].Emit(1);
                }
            }
        }
        else
        {
            nextState = new Idle(turret, barrel);
            stage = EVENT.EXIT;
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(turret.transform.position, 2f, layerMask: 8);

        foreach (Collider col in hitColliders)
        {
            Boost boost = col.transform.GetComponent<Boost>();
            if(boost.Type != Boost.BoostType.Speed) continue;
            fireRatio = fireRatioBackup / boost.BoostValue;
        }
    }

    public override void Exit()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
        base.Exit();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(turret.transform.position, 2f);
    }
}