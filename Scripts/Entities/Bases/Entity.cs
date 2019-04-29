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
    protected Enums.AttackState attackState;
    private AttackProcess currentAttack;
    private int health;

    protected Cooldowns cooldowns;
    protected Weapon currentWeapon;
    protected int weaponNumber = 0;

    protected List<Weapon> weapons;
    protected Vector3 moveDirection;

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
        if (Time.time > cooldowns.dodgeCooldownTime)
        {
            if (moveDirection == Vector3.zero)
                moveDirection = Vector3.forward;
            controller.Dash(moveDirection * stats.dashDistance / stats.dashTime, stats.dashTime);
            cooldowns.dodgeCooldownTime = Time.time + stats.dashTime + stats.dashCooldown;
        }
    }

    protected void Attack(IDamageable target, Weapon weapon)
    {
        if (Time.time > cooldowns.attackCooldownTime && IsInRange(transform.position, (Entity)target, weapon))
        {
            attackState = Enums.AttackState.ATTACK;
            currentAttack = new AttackProcess(target, weapon);
            this.cooldowns.attackCooldownTime = Time.time + weapon.stats.attackCooldown + weapon.stats.attackTime;
            Debug.LogWarning("Animation of attack");
            // REDO AVEC ANIMATION
            Invoke("OnAttackAnimationEnd", weapon.stats.attackTime);
        }
        else
        {
            attackState = Enums.AttackState.IDLE;
        }
    }

    private void OnAttackAnimationEnd()
    {
        Transform inFrontTransform = LookInFront();
        if (inFrontTransform != null)
            if (inFrontTransform.tag == "Enemy" || inFrontTransform.tag == "Player")
                currentAttack.weapon.Attack(currentAttack.target);
        attackState = Enums.AttackState.IDLE;
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

    protected abstract Transform LookInFront();

    public static bool IsInRange(Vector3 selfPos, Entity target, Weapon weapon)
    {
        return (Vector3.Distance(target.transform.position, selfPos) <= weapon.stats.attackRange);
    }

    private struct AttackProcess
    {
        public IDamageable target;
        public Weapon weapon;

        public AttackProcess(IDamageable target, Weapon weapon)
        {
            this.target = target;
            this.weapon = weapon;
        }
    }

}
