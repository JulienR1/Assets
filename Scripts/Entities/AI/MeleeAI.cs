using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : EntityAI
{
    public override void SetAttackPriority(int health, Weapon currentWeapon)
    {
        priorities.Clear();
        if (previousState == Enums.AttackState.CHASE || previousState == Enums.AttackState.IDLE)
        {
            if ((float)health / stats.maxHealth < stats.healthPercentToAttack)
                priorities.Add(Enums.AttackState.FLEE);
            if (target.GetAttackState() == Enums.AttackState.ATTACK)
                priorities.Add(Enums.AttackState.DODGE);
            if (Entity.IsInRange(transform.position, target, currentWeapon))
                priorities.Add(Enums.AttackState.ATTACK);
            priorities.Add(Enums.AttackState.CHASE);
        }
        else
        {
            priorities.Add(previousState);
        }
    }

    public override Enums.AttackState ProcessPriorities(Cooldowns cooldowns, Weapon weapon)
    {
        Debug.LogWarning("RESET ATTACK STATE");
        foreach (Enums.AttackState state in this.priorities)
        {
            switch (state)
            {
                case Enums.AttackState.FLEE:
                    if (previousState != Enums.AttackState.ATTACK && previousState != Enums.AttackState.DODGE)
                        return state;
                    break;
                case Enums.AttackState.DODGE:
                    if (previousState != Enums.AttackState.ATTACK && Time.time >= cooldowns.dodgeCooldownTime)
                        return state;
                    break;
                case Enums.AttackState.ATTACK:
                    if (Time.time >= cooldowns.attackCooldownTime)
                        return state;
                    break;
                case Enums.AttackState.CHASE:
                    if (!Entity.IsInRange(transform.position, target, weapon))
                        return state;
                    break;
            }
        }
        return Enums.AttackState.IDLE;
    }

    public override void FindDisplacementTarget(Enums.AttackState attackState)
    {
        switch (attackState)
        {
            case Enums.AttackState.FLEE:
                Flee();
                break;
            case Enums.AttackState.DODGE:
                Dodge();
                break;
            case Enums.AttackState.ATTACK:
                break;
            case Enums.AttackState.CHASE:
                this.targetPos = this.target.transform.position;
                break;
            case Enums.AttackState.IDLE:
                this.targetPos = this.transform.position;
                break;
        }
    }

    public override void Flee()
    {

    }

    public override void Dodge()
    {
        Vector3 targetDirection = (this.target.transform.position - this.transform.position).normalized;
        float offsetAngle = Random.Range(-this.stats.offsetDodgeAngle, this.stats.offsetDodgeAngle);
        float dodgeAngle = (this.stats.centerDodgeAngle + offsetAngle) * Mathf.Deg2Rad;

        float dodgeX = targetDirection.x * Mathf.Cos(dodgeAngle) - targetDirection.z * Mathf.Sin(dodgeAngle);
        float dodgeZ = targetDirection.x * Mathf.Sin(dodgeAngle) + targetDirection.z * Mathf.Cos(dodgeAngle);
        this.dodgeDirection = new Vector3(dodgeX, 0, dodgeZ);
    }

}
