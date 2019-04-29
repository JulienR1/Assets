using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public abstract class Entity : MonoBehaviour, IDamageable
{
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;

    public EntityStats stats;
    protected EntityController controller;
    private int health;

    protected List<Weapon> weapons;
    protected Weapon currentWeapon;
    protected int weaponNumber = 0;

    protected Vector3 moveDirection;
    private float dashRefreshTime;

    protected virtual void Start()
    {
        currentWeapon = weapons[0];

        controller = this.GetComponent<EntityController>();
        this.health = stats.maxHealth;
    }

    public void SelectWeapon() {

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weaponNumber++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weaponNumber--;
        }

        currentWeapon = weapons[weaponNumber % weapons.Count];

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

    public virtual void Die()
    {
        print("Killed " + gameObject.name);
        FamePoints.KillFame(stats.deathFame);
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }

        Destroy(this.gameObject);
    }

    protected virtual void Animate()
    {

    }

    protected abstract void LookInFront();

}
