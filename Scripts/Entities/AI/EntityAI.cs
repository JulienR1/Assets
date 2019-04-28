using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    public Astar pathfinding;
    protected Entity target;
    protected List<Node> path;
    protected Vector3 targetPos;

    protected Enums.AttackState previousState;
    protected List<Enums.AttackState> priorities;

    public abstract List<Enums.AttackState> SetAttackPriority(EntityStats stats, int health, Weapon currentWeapon);
    public abstract Enums.AttackState ProcessPriorities(Cooldowns cooldowns);
    public abstract void FindDisplacementTarget(Enums.AttackState attackState);
    public abstract void Attack(Entity target);
    public abstract void Flee(Entity target);
    public abstract void Dodge(Entity target);

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    public void SetPreviousAttackState(Enums.AttackState previousState)
    {
        this.previousState = previousState;
    }

    public bool FindPath()
    {
        path = pathfinding.FindPath(transform.position, targetPos);
        return path != null;
    }

    public Vector3 FollowPath(float moveSpeed)
    {
        if (path == null)
            return Vector3.zero;
        if (path.Count == 0)
            return Vector3.zero;

        Vector3 moveDirection = path[0].position - transform.position;
        moveDirection.Normalize();
        moveDirection.y = 0;

        return moveDirection * moveSpeed;
    }

    public bool IsInRange(Entity target, Weapon weapon)
    {
        return (Vector3.Distance(target.transform.position, transform.position) <= weapon.stats.attackRange);
    }

    public Node GetNextNode()
    {
        return path[0];
    }

}
