using UnityEngine;

internal class Tracking : State
{
    public Tracking(GameObject _turret, Transform _barrel) : base(_turret, _barrel)
    {
        name = STATE.TRACKING;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (!GetClosestEnemyInArea(_visionDistance))
        {
            nextState = new Idle(turret, barrel);
            stage = EVENT.EXIT;
        }
        else if (GetClosestEnemyInArea(_attackDistance))
        {
            nextState = new Attacking(turret, barrel);
            stage = EVENT.EXIT;
        }
        else if (GetClosestEnemyInArea(_visionDistance))
        {
            Vector3 direction = GetClosestEnemyInArea(_visionDistance).transform.position - turret.transform.position;
            direction.y = 0;

            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}