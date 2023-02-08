using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditorInternal;
using NUnit.Framework;

[CustomEditor(typeof(Choice))]
public class ChoiceEditor : Editor
{
    new SerializedProperty name;
    SerializedProperty description;
    SerializedProperty resultText;
    SerializedProperty requiredKnol;
    SerializedProperty fromTags;
    SerializedProperty toTags;
    SerializedProperty targetTraits;
    SerializedProperty statRanges;
    SerializedProperty includedEnvironments;
    SerializedProperty excludedEnvironments;
    SerializedProperty consequences;
    Type[] consequenceTypes;

    protected Choice choice;

    int consequenceIndex = 0;

    protected virtual void OnEnable()
    {
        name = serializedObject.FindProperty("name");
        description = serializedObject.FindProperty("description");
        resultText = serializedObject.FindProperty("resultText");
        requiredKnol = serializedObject.FindProperty("requiredKnol");
        fromTags = serializedObject.FindProperty("fromTags");
        toTags = serializedObject.FindProperty("toTags");
        targetTraits = serializedObject.FindProperty("targetTraits");
        statRanges = serializedObject.FindProperty("statRanges");
        includedEnvironments = serializedObject.FindProperty("includedEnvironments");
        excludedEnvironments = serializedObject.FindProperty("excludedEnvironments");
        consequences = serializedObject.FindProperty("consequences");
        consequenceTypes = GetImplementations<Consequence>();
        choice = (Choice)target;
    }

    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        serializedObject.Update();
        EditorGUILayout.PropertyField(name);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(resultText);
        EditorGUILayout.PropertyField(requiredKnol);
        EditorGUILayout.PropertyField(fromTags);
        EditorGUILayout.PropertyField(toTags);
        EditorGUILayout.PropertyField(targetTraits);
        EditorGUILayout.PropertyField(statRanges);
        EditorGUILayout.PropertyField(includedEnvironments);
        EditorGUILayout.PropertyField(excludedEnvironments);
        EditorGUILayout.PropertyField(consequences);
        EditorGUILayout.BeginHorizontal();

        consequenceIndex = EditorGUILayout.Popup(consequenceIndex, consequenceTypes.Select(et => et.ToString()).ToArray());

        if (GUILayout.Button("Add Effect"))
        {
            AddEffect();
        }
        if (GUILayout.Button("Refresh Effects"))
        {
            consequenceTypes = GetImplementations<Consequence>();
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }

    void AddEffect()
    {
        // action.effects.Add((ActionEffect)Activator.CreateInstance(effectTypes[effectIndex]));
        consequences.InsertArrayElementAtIndex(consequences.arraySize);
        serializedObject.ApplyModifiedProperties();
        // explicitly set that new item
        choice.consequences[choice.consequences.Length-1] = (Consequence)Activator.CreateInstance(consequenceTypes[consequenceIndex]);
    }

    private static Type[] GetImplementations<T>()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        var interfaceType = typeof(T);
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }
}
