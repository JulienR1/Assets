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
            if (health / stats.maxHealth < stats.healthPercentToAttack)
                priorities.Add(Enums.AttackState.FLEE);
            if (target.GetAttackState() == Enums.AttackState.ATTACK)
                priorities.Add(Enums.AttackState.DODGE);
            if (IsInRange(target, currentWeapon))
                priorities.Add(Enums.AttackState.ATTACK);
        }
        priorities.Add(Enums.AttackState.CHASE);
    }

    public override Enums.AttackState ProcessPriorities(Cooldowns cooldowns)
    {
        Debug.LogWarning("RESET ATTACK STATE");
        foreach (Enums.AttackState state in this.priorities)
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
        switch (attackState)
        {
            case Enums.AttackState.FLEE:
                Flee();
                break;
            case Enums.AttackState.DODGE:
                Dodge();
                break;
            case Enums.AttackState.ATTACK:
                Attack();
                break;
            case Enums.AttackState.CHASE:
                this.targetPos = this.target.transform.position;
                break;
            case Enums.AttackState.IDLE:
                this.targetPos = this.transform.position;
                break;
        }
    }

    public override void Attack()
    {
        this.targetPos = this.transform.position;
        this.LookAtTarget();
    }

    public override void Flee()
    {

    }

    public override void Dodge()
    {
        Vector3 targetDirection = (this.targetPos - this.transform.position).normalized;
        float offsetAngle = Random.Range(-this.stats.offsetDodgeAngle, this.stats.offsetDodgeAngle);
        float dodgeAngle = (this.stats.centerDodgeAngle + offsetAngle) * Mathf.Deg2Rad;

        float dodgeX = targetDirection.x * Mathf.Cos(dodgeAngle) - targetDirection.z * Mathf.Sin(dodgeAngle);
        float dodgeZ = targetDirection.x * Mathf.Sin(dodgeAngle) + targetDirection.z * Mathf.Cos(dodgeAngle);
        this.dodgeDirection = new Vector3(dodgeX, 0, dodgeZ);
    }

}
