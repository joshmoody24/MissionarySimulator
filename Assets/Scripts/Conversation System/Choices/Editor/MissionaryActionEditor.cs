using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*

[CustomEditor(typeof(MissionaryAction))]
public class MissionaryActionEditor : AbstractActionEditor
{

    SerializedProperty specialPointsCost;

    protected override void OnEnable()
    {
        base.OnEnable();
        specialPointsCost = serializedObject.FindProperty("specialPointsCost");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Missionary Specific", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(specialPointsCost);
        serializedObject.ApplyModifiedProperties();
    }
}
*/