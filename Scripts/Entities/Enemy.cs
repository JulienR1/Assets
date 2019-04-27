using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EntityAI ai;
    private Entity target;
    private Enums.AttackState attackState;

    protected override void Start()
    {
        base.Start();
        ai = this.GetComponent<EntityAI>();
        if (ai == null)
            Debug.LogError("No EntityAI component attached to " + gameObject.name);
    }

    private void UpdateAI()
    {

    }

    protected override void LookInFront()
    {
        Debug.Log("LOOK in fRONT");
    }

    private void LookAtTarget()
    {
        transform.LookAt(ai.GetNextPosition());
    }
}
