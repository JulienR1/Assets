using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : EntityAI
{
    public override Enums.AttackState SetAttackPriority(EntityStats stats)
    {
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState, Entity target)
    {
        if (attackState == Enums.AttackState.CHASE)
            this.targetPos = target.transform.position;
    }

    public override void Attack(Entity target)
    {
        
    }

    public override void Flee(Entity target)
    {
        
    }

    public override void Dodge(Entity target)
    {
        
    }

}
