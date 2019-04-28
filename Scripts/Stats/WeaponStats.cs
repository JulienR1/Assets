using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Weapon Stats", menuName = "LD44/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public int itemCost;

    public int attackDamage;
    public int attackMax;

    public float attackRange;
    public float rangeMax;

    public float attackTime;
    public float attackCooldown;

    public Enums.AttackType attackType;
    public float rangeAngle;
}
