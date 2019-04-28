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
        ai.SetTarget(target);
    }

    protected override void Update()
    {
        base.Update();
        UpdateAI();
    }

    private void UpdateAI()
    {
        ai.SetPreviousAttackState(this.attackState);
        ai.SetAttackPriority(stats, this.GetHealth(), this.weapons[0]);
        this.attackState = ai.ProcessPriorities(this.cooldowns);
        ai.FindDisplacementTarget(this.attackState);
        ai.FindPath();
        controller.Move(ai.FollowPath(stats.moveSpeed));
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
