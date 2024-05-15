using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE,
        TRACKING,
        ATTACKING
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

    protected float visionDistance = 20.0f;
    protected float attackDistance = 10.0f;

    public State(GameObject _turret, Transform barrel)
    {
        turret = _turret;
        this.barrel = barrel;

        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

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
}
