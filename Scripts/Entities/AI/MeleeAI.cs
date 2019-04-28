using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : EntityAI
{
    public override List<Enums.AttackState> SetAttackPriority(EntityStats stats, int health, Weapon currentWeapon)
    {
        List<Enums.AttackState> priority = new List<Enums.AttackState>();
        if (previousState == Enums.AttackState.CHASE || previousState == Enums.AttackState.IDLE)
        {
            if (health / stats.maxHealth < stats.healthPercentToAttack)
                priority.Add(Enums.AttackState.FLEE);
            if (target.GetAttackState() == Enums.AttackState.ATTACK)
                priority.Add(Enums.AttackState.DODGE);
            if (IsInRange(target, currentWeapon))
                priority.Add(Enums.AttackState.ATTACK);
        }
        priority.Add(Enums.AttackState.CHASE);
        return priority;
    }

    public override Enums.AttackState ProcessPriorities(Cooldowns cooldowns)
    {
        Debug.LogWarning("RESET ATTACK STATE");
        foreach(Enums.AttackState state in this.priorities)
        {
            switch (state)
            {
                case Enums.AttackState.FLEE:
                    if (state == previousState || previousState != Enums.AttackState.ATTACK && previousState != Enums.AttackState.DODGE)
                        return state;
                    break;
                case Enums.AttackState.DODGE:
                    if (state == previousState || previousState != Enums.AttackState.ATTACK && Time.time >= cooldowns.dodgeCooldownTime)
                        return state;
                    break;
                case Enums.AttackState.ATTACK:
                    if (state == previousState || Time.time >= cooldowns.attackCooldownTime)
                        return state;
                    break;
                case Enums.AttackState.CHASE:
                    return state;                    
            }
        }
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState)
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
