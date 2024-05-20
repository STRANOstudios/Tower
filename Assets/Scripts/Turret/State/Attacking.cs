using UnityEngine;

internal class Attacking : State
{
    readonly float rotationSpeed = 20.0f;
    readonly float fireRatio = 0.1f;

    ParticleSystem particleSystem;

    float timeSinceLastFire = 0f;

    public Attacking(GameObject _turret, Transform _barrel) : base(_turret, _barrel)
    {
        name = STATE.TRACKING;

        particleSystem = _turret.GetComponentInChildren<ParticleSystem>();
    }

    public override void Enter()
    {
        particleSystem.Play();
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

            if (particleSystem && Time.time - timeSinceLastFire > 1 / fireRatio)
            {
                timeSinceLastFire = Time.time;
                particleSystem.Emit(1);
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
        particleSystem.Stop();
        base.Exit();
    }
}