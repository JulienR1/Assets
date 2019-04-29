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
        if (this.attackState == Enums.AttackState.DODGE)
            if (!controller.GetIsDashing())
                this.attackState = Enums.AttackState.IDLE;    

        UpdateAI();
        LookAtTarget();
        base.Update();
    }

    private void UpdateAI()
    {
        ai.SetPreviousAttackState(this.attackState);
        ai.SetAttackPriority(this.GetHealth(), this.weapons[0]);
        this.attackState = ai.ProcessPriorities(this.cooldowns, this.weapons[0]);
        ai.FindDisplacementTarget(this.attackState);

        if (this.attackState == Enums.AttackState.DODGE)
        {
            this.moveDirection = ai.GetDodgeDirection();
            Dash();
        }
        else if (attackState == Enums.AttackState.ATTACK || attackState == Enums.AttackState.IDLE)
        {
            if (attackState == Enums.AttackState.ATTACK)
            {
                Debug.LogWarning("Change to equipped weapon");
                this.Attack(target, weapons[0]);
            }
            this.moveDirection = Vector3.zero;
        }
        else
        {
            ai.FindPath();
            this.moveDirection = ai.FollowPath();
        }
        print(this.attackState);
    }

    protected override Transform LookInFront()
    {
        if (target == null) {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "Player":
                        target = hit.transform.GetComponent<Player>();
                        return target.transform;
                }
            }
        }
        return null;
    }

    protected void LookAtTarget()
    {
        Vector3 pos;
        Debug.LogWarning("UPDATE");
        /*if (ai.GetNextNode() != null)
            pos = ai.GetNextNode().position;
        else*/
            pos = target.transform.position;
        transform.LookAt(pos);
        Vector3 rotation = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles = rotation;
    }
}
