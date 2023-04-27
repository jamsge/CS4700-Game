using UnityEditor;

[CustomEditor(typeof(WeaponManager))]
public class WeaponStatsEditor : Editor
{
    public enum DisplayCategory
    {
        Base, Upgraded
    }

    public DisplayCategory categoryToDisplay;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerData"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("player"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrower"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxe"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGun"));

        EditorGUILayout.Space();

        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display Stats", categoryToDisplay);

        EditorGUILayout.Space();

        switch (categoryToDisplay)
        {
            case DisplayCategory.Base:
                DisplayBaseInfo();
                break;
            case DisplayCategory.Upgraded:
                DisplayUpgradedInfo();
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DisplayBaseInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerCooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerAmmoUsage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackRadius"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunStunDuration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunCooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunAmmoUsage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonKnockbackStrength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonCooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonAmmoUsage"));
    }

    void DisplayUpgradedInfo()
    {
        //tba
    }
    
}