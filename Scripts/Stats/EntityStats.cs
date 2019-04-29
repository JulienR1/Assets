using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Entity Stats", menuName = "LD44/Entity Stats")]
public class EntityStats : ScriptableObject
{
    public int maxHealth;    

    public float moveSpeed;
    public float acceleration;

    public float dashDistance;
    public float dashTime;
    public float dashCooldown;
}
