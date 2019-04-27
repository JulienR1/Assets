using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EntityController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 moveVelocity;

    private bool isDashing = false;
    private float dashEndTime;
    private Vector3 dashVelocity;

    private void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveVelocity)
    {
        this.moveVelocity = moveVelocity;

        if (isDashing)
        {
            this.moveVelocity = dashVelocity;
            if (Time.time > dashEndTime)
                isDashing = false;
        }
    }

    public bool Dash(Vector3 dashVelocity, float dashTime)
    { 
        if (isDashing)
            return false;
        if (Time.time <= dashEndTime)
            return false;
        this.dashVelocity = dashVelocity;
        dashEndTime = Time.time + dashTime;
        isDashing = true;
        return true;
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + moveVelocity * Time.fixedDeltaTime);
    }
}
