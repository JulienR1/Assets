using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;

    public void Attack(IDamageable target)
    {
        print("REDO logique");
        target.TakeDamage(stats.attackDamage);
    }
}
