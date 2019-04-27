using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class Entity : MonoBehaviour, IDamageable
{
    public EntityStats stats;
    private EntityController controller;
    private int health;

    protected Weapon[] weapons;

    protected Vector3 moveDirection;
    private float dashRefreshTime;

    private void Start()
    {
        controller = this.GetComponent<EntityController>();
        this.health = stats.maxHealth;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {
        controller.Move(moveDirection*stats.moveSpeed);
    }

    protected void Dash()
    {
        if (Time.time > dashRefreshTime)
        {
            if (moveDirection == Vector3.zero)
                moveDirection = Vector3.forward;
            controller.Dash(moveDirection * stats.dashDistance / stats.dashTime, stats.dashTime);
            dashRefreshTime = Time.time + stats.dashTime + stats.dashCooldown;
        }
    }

    protected void Attack(IDamageable target, Weapon weapon)
    {
        weapon.Attack(target);
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
            Die();
    }

    public void Die()
    {
        print("Killed " + gameObject.name);
        Destroy(this.gameObject);
    }

}
