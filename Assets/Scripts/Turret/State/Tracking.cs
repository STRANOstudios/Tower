using UnityEngine;

internal class Tracking : State
{
    readonly float rotationSpeed = 2.0f;

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
        if (!GetClosestEnemyInArea(visionDistance))
        {
            nextState = new Idle(turret, barrel);
            stage = EVENT.EXIT;
        }
        else if (GetClosestEnemyInArea(attackDistance))
        {
            nextState = new Attacking(turret, barrel);
            stage = EVENT.EXIT;
        }
        else if (GetClosestEnemyInArea(visionDistance))
        {
            Vector3 direction = GetClosestEnemyInArea(visionDistance).transform.position - turret.transform.position;
            direction.y = 0;

            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}