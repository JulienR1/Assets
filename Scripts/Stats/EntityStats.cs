using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Entity Stats", menuName = "LD44/Entity Stats")]
public class EntityStats : ScriptableObject
{
    public int maxHealth;
    [Range(0,1)]
    public float healthPercentToAttack;

    public float moveSpeed;
    public float acceleration;

    public float dashDistance;
    public float dashTime;
    public float dashCooldown;

    public float fleeRangeMultiplier;

    public float centerDodgeAngle = 90;
    public float offsetDodgeAngle = 20;
}
