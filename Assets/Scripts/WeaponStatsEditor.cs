using UnityEditor;
using UnityEngine;

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

        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponReloadTime"));

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

        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerSoundEffect"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeSoundEffect"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunSoundEffect"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonSoundEffect"));

        serializedObject.ApplyModifiedProperties();

    }

    void DisplayBaseInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerDamage"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerRange"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerCooldown"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerAmmoUsage"), new GUIContent("Ammo Usage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackSpeed"), new GUIContent("Attack Speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeDamage"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackRadius"), new GUIContent("Attack Radius"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunDamage"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunRange"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunStunDuration"), new GUIContent("Stun Duration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunCooldown"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunAmmoUsage"), new GUIContent("Ammo Usage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonDamage"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonRange"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonKnockbackStrength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonCooldown"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonAmmoUsage"), new GUIContent("Ammo Usage"));
    }

    void DisplayUpgradedInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerDamageU"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerRangeU"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerCooldownU"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flamethrowerAmmoUsageU"), new GUIContent("Ammo Usage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackSpeedU"), new GUIContent("Attack Speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeDamageU"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAxeAttackRadiusU"), new GUIContent("Attack Radius"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunDamageU"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunRangeU"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunStunDurationU"), new GUIContent("Stun Duration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunCooldownU"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taserGunAmmoUsageU"), new GUIContent("Ammo Usage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonDamageU"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonRangeU"), new GUIContent("Range"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonKnockbackStrengthU"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonCooldownU"), new GUIContent("Cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waterCannonAmmoUsageU"), new GUIContent("Ammo Usage"));
    }
    
}