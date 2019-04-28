using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EntityAI ai;
    public Entity target;
    private Enums.AttackState attackState;

    protected override void Start()
    {
        base.Start();
        ai = this.GetComponent<EntityAI>();
        if (ai == null)
            Debug.LogError("No EntityAI component attached to " + gameObject.name);
    }

    protected override void Update()
    {
        base.Update();
        UpdateAI();
    }

    private void UpdateAI()
    {
        FindObjectOfType<Astar>().FindPath(transform.position, target.transform.position);
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
