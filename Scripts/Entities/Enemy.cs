using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EntityAI ai;
    private Entity target;

    protected override void Start()
    {
        base.Start();
        ai = this.GetComponent<EntityAI>();
        if (ai == null)
            Debug.LogError("No EntityAI component attached to " + gameObject.name);
        target = FindObjectOfType<Player>();
        ai.SetInitialValues(target, stats);
    }

    protected override void Update()
    {
        UpdateAI();
        base.Update();
    }

    private void UpdateAI()
    {
        ai.SetPreviousAttackState(this.attackState);
        ai.SetAttackPriority(this.GetHealth(), this.weapons[0]);
        this.attackState = ai.ProcessPriorities(this.cooldowns);
        ai.FindDisplacementTarget(this.attackState);

        if (this.attackState == Enums.AttackState.DODGE)
        {
            this.moveDirection = ai.GetDodgeDirection();
            Dash();
        }
        else
        {
            ai.FindPath();
            this.moveDirection = ai.FollowPath();
        }
    }

    protected override void LookInFront()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "Player":
                    target = hit.transform.GetComponent<Player>();
                    break;
            }
        }
    }

    private void LookAtTarget()
    {
        transform.LookAt(ai.GetNextNode().position);
    }
}
