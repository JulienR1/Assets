using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAI : EntityAI
{
    public override void SetAttackPriority(int health, Weapon currentWeapon)
    {
        priorities.Add(Enums.AttackState.IDLE);
    }

    public override Enums.AttackState ProcessPriorities(Cooldowns cooldowns)
    {
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState)
    {
        
    }

    public override void Attack()
    {

    }

    public override void Flee()
    {

    }

    public override void Dodge()
    {

    }
}
