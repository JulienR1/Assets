using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    protected Queue<Vector3> path;

    public abstract bool FindPath(Entity target);
    public abstract bool FollowPath();
    public abstract void Attack(Entity target);
    public abstract void Flee(Entity target);
    public abstract void Dodge(Entity target);

    public bool IsInRange(Entity target, Weapon weapon)
    {
        return (Vector3.Distance(target.transform.position, transform.position) <= weapon.stats.attackRange);
    }

    public Vector3 GetNextPosition()
    {
        return path.Peek();
    }

}
