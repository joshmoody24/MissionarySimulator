using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(NonmemberAction))]
public class NonmemberActionEditor : AbstractActionEditor
{
    SerializedProperty minAttention;
    SerializedProperty maxAttention;

    NonmemberAction nmAction;

    protected override void OnEnable()
    {
        base.OnEnable();
        minAttention = serializedObject.FindProperty("minAttention");
        maxAttention = serializedObject.FindProperty("maxAttention");
        nmAction = (NonmemberAction)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Nonmember Specific", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(minAttention.floatValue.ToString("0.000"), GUILayout.Width(50));
        EditorGUILayout.MinMaxSlider(ref nmAction.minAttention, ref nmAction.maxAttention, 0f, 1f);
        EditorGUILayout.LabelField(maxAttention.floatValue.ToString("0.000"), GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
*/