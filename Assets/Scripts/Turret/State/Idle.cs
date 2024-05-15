using UnityEngine;

public class Idle : State
{
    public Idle(GameObject _turret, Transform _barrel) : base(_turret, _barrel)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if(GetClosestEnemyInArea(visionDistance) != null)
        {
            nextState = new Tracking(turret, barrel);
            stage = EVENT.EXIT;
        }
        else if(GetClosestEnemyInArea(attackDistance) != null)
        {
            nextState = new Attacking(turret, barrel);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
