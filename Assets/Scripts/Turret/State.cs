using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE,
        TRACKING,
        ATTACKING,
        DRAGGING
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject turret;
    protected Transform barrel;
    protected State nextState;

    protected float _visionDistance = 20.0f;
    protected float _attackDistance = 10.0f;
    protected float _rotationSpeed = 2.0f;
    protected float _fireRatio = 0.1f;

    /// <summary>
    /// Initializes a new instance of the <see cref="State"/> class.
    /// </summary>
    /// <param name="_turret"></param>
    /// <param name="_barrel"></param>
    public State(GameObject _turret, Transform _barrel)
    {
        turret = _turret;
        barrel = _barrel;

        stage = EVENT.ENTER;
    }

    /// <summary>
    /// Called when the state is entered.
    /// </summary>
    public virtual void Enter() { stage = EVENT.UPDATE; }
    /// <summary>
    /// Called when the state is updated.
    /// </summary>
    public virtual void Update() { stage = EVENT.UPDATE; }
    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public virtual void Exit() { stage = EVENT.EXIT; }

    /// <summary>
    /// Process the state.
    /// </summary>
    /// <returns></returns>
    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    /// <summary>
    /// Get the closest enemy in area.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public GameObject GetClosestEnemyInArea(float radius)
    {
        var position = turret.transform.position;

        Collider[] colliders = Physics.OverlapSphere(position, radius);

        List<GameObject> enemiesInRange = new();

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemiesInRange.Add(collider.gameObject);
            }
        }

        if (enemiesInRange.Count == 0)
        {
            return null;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public float FireRatio
    {
        get { return _fireRatio; }
        set { _fireRatio = value; }
    }

    public float RotationSpeed
    {
        get { return _rotationSpeed; }
        set { _rotationSpeed = value; }
    }

    public float AttackDistance
    {
        get { return _attackDistance; }
        set { _attackDistance = value; }
    }

    public float VisionDistance
    {
        get { return _visionDistance; }
        set { _visionDistance = value; }
    }
}
