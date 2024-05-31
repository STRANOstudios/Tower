using UnityEngine;

internal class Dragging : State
{
    public Dragging(GameObject _turret, Transform barrel) : base(_turret, barrel)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}