using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponStats))]
public class WeaponStatsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        WeaponStats controller = target as WeaponStats;

        GUIStyle style = new GUIStyle();
        style.richText = true;

        controller.itemCost = Mathf.Clamp(EditorGUILayout.IntField("Item cost", controller.itemCost), 0, int.MaxValue);

        EditorGUILayout.Space();

        string attackTitle = (controller.attackType == Enums.AttackType.RANGE) ? "Min Damage" : "Damage";
        controller.attackDamage = Mathf.Clamp(EditorGUILayout.IntField(attackTitle, controller.attackDamage), 0, int.MaxValue);
        if (controller.attackType == Enums.AttackType.RANGE)
        {
            controller.attackMax = Mathf.Clamp(EditorGUILayout.IntField("Max Damage", controller.attackMax), controller.attackDamage, int.MaxValue);
        }

        EditorGUILayout.Space();

        string rangeTitle = (controller.attackType == Enums.AttackType.RANGE) ? "Min Range" : "Range";
        controller.attackRange = Mathf.Clamp(EditorGUILayout.FloatField(rangeTitle, controller.attackRange), 0, float.MaxValue);
        if (controller.attackType == Enums.AttackType.RANGE)
        {
            controller.rangeMax = Mathf.Clamp(EditorGUILayout.FloatField("Max Range", controller.rangeMax), controller.attackRange, float.MaxValue);
        }

        EditorGUILayout.Space();

        controller.attackTime = EditorGUILayout.FloatField("Attack Time", controller.attackTime);
        controller.attackCooldown = EditorGUILayout.FloatField("Attack Cooldown", controller.attackCooldown);
        controller.attackType = (Enums.AttackType)EditorGUILayout.EnumPopup("Attack Type", controller.attackType);
        if (controller.attackType == Enums.AttackType.MULTIPLE)
        {
            EditorGUI.indentLevel++;
            controller.rangeAngle = Mathf.Clamp(EditorGUILayout.FloatField("Range angle", controller.rangeAngle), 0, 360);
            EditorGUI.indentLevel--;
        }
    }
}