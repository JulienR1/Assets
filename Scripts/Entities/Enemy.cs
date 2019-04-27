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
        transform.LookAt(ai.GetNextPosition());
    }
}
