using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    private const float PATH_REFRESH_RATE = 0.25f;
    private float nextPathRefreshTime;

    public Astar pathfinding;
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
            path = pathfinding.FindPath(transform.position, targetPos);
            nextPathRefreshTime = Time.time + PATH_REFRESH_RATE;
        }
        return path != null;
    }

    public Vector3 FollowPath()
    {
        if (path == null)
            return Vector3.zero;
        if (path.Count == 0)
            return Vector3.zero;

        Vector3 moveDirection = GetNextNode().position - transform.position;
        moveDirection.Normalize();
        moveDirection.y = 0;

        return moveDirection;
    }

    public Node GetNextNode()
    {
        if (path != null)
            if (path.Count > 0)
                return path[0];
        return null;
    }

    public Vector3 GetDodgeDirection()
    {
        return dodgeDirection;
    }

    private void OnDrawGizmos()
    {
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (Node n in path)
            {
                Gizmos.DrawCube(n.position, Vector3.one);
            }
        }
    }
}
