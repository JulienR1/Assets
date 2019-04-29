using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    private const float PATH_REFRESH_RATE = 0.25f;
    private float nextPathRefreshTime;

    public Pathfinding pathfinding;
    protected Entity target;
    protected List<Node> path;
    protected Vector3 targetPos;
    protected Vector3 dodgeDirection;

    protected EnemyStats stats;
    protected Enums.AttackState previousState;
    protected List<Enums.AttackState> priorities = new List<Enums.AttackState>();

    public abstract void SetAttackPriority(int health, Weapon currentWeapon);
    public abstract Enums.AttackState ProcessPriorities(Cooldowns cooldowns, Weapon weapon);
    public abstract void FindDisplacementTarget(Enums.AttackState attackState);
    public abstract void Flee();
    public abstract void Dodge();

    public void SetInitialValues(Entity target, EntityStats stats)
    {
        this.target = target;
        this.stats = (EnemyStats)stats;
    }

    public void SetPreviousAttackState(Enums.AttackState previousState)
    {
        this.previousState = previousState;
    }

    public bool FindPath()
    {
        if (Time.time > nextPathRefreshTime)
        {
            Debug.LogWarning("UPDATE");
   //         path = pathfinding.FindPath(transform.position, targetPos);
            nextPathRefreshTime = Time.time + PATH_REFRESH_RATE;
        }
        return path != null;
    }

    public Vector3 FollowPath()
    {
        Debug.LogWarning("UPDATE");
        return Vector3.zero;
    }

    public Vector3 GetDodgeDirection()
    {
        return dodgeDirection;
    }
}
