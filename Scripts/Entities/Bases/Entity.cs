using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public abstract class Entity : MonoBehaviour, IDamageable
{
    public EntityStats stats;
    protected EntityController controller;
    protected Enums.AttackState attackState;
    private int health;

    public List<Weapon> weapons;
    protected Cooldowns cooldowns;

    protected Vector3 moveDirection;
    private float dashRefreshTime;

    protected virtual void Start()
    {
        controller = this.GetComponent<EntityController>();
        this.health = stats.maxHealth;
    }

    protected virtual void Update()
    {
        Move();
        Animate();
        LookInFront();
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

    public int GetHealth()
    {
        return health;
    }

    public Enums.AttackState GetAttackState()
    {
        return attackState;
    }

    public void Die()
    {
        print("Killed " + gameObject.name);
        Destroy(this.gameObject);
    }

    protected virtual void Animate()
    {

    }

    protected abstract void LookInFront();

}
