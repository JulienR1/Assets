using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Enemy Stats", menuName = "LD44/Enemy Stats")]
public class EnemyStats : EntityStats
{
    public float fleeRangeMultiplier;


    public float centerDodgeAngle = 90;
    public float offsetDodgeAngle = 20;
}
