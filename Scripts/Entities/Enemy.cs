using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EntityAI ai;
    private Entity target;
    private Enums.AttackState attackState;

    private void UpdateAI()
    {

    }

    private void LookAtTarget()
    {
        transform.LookAt(ai.GetNextPosition());
    }
}
