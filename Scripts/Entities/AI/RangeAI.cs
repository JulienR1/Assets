using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAI : EntityAI
{
    public override Enums.AttackState SetAttackPriority(EntityStats stats)
    {
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState, Entity target)
    {

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
