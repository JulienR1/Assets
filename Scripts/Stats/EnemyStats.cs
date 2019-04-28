using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Enemy Stats", menuName = "LD44/Enemy Stats")]
public class EnemyStats : EntityStats
{
    [Range(0, 1)]
    public float healthPercentToAttack;

    public float fleeRangeMultiplier;

    public float centerDodgeAngle = 90;
    public float offsetDodgeAngle = 20;
}
