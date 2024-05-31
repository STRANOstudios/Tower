using UnityEngine;

internal class Attacking : State
{
    float timeSinceLastFire = 0f;

    private readonly ParticleSystem[] particleSystems;

    public Attacking(GameObject _turret, Transform _barrel) : base(_turret, _barrel)
    {
        name = STATE.TRACKING;

        particleSystems = _turret.GetComponentsInChildren<ParticleSystem>();
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
        if (GetClosestEnemyInArea(_attackDistance))
        {
            Vector3 direction = GetClosestEnemyInArea(_attackDistance).transform.position - turret.transform.position;
            Quaternion barrelRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, barrelRotation, Time.deltaTime * _rotationSpeed);

            float elevationAngle = Mathf.Atan2(direction.y, direction.magnitude) * Mathf.Rad2Deg;

            barrel.localRotation = Quaternion.Euler(-elevationAngle, 0f, 0f);

            if (Time.time - timeSinceLastFire > _fireRatio)
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

    public override void Exit()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
        base.Exit();
    }
}