using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAI : EntityAI
{
    public override List<Enums.AttackState> SetAttackPriority(EntityStats stats, int health, Weapon currentWeapon)
    {
        List<Enums.AttackState> priority = new List<Enums.AttackState>();
        return priority;
    }

    public override Enums.AttackState ProcessPriorities(Cooldowns cooldowns)
    {
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState)
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
