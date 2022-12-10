using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(AbstractAction))]
public abstract class AbstractActionEditor : Editor
{
    new SerializedProperty name;
    SerializedProperty description;
    SerializedProperty category;
    SerializedProperty requiredKnowledge;
    SerializedProperty effects;

    protected AbstractAction action;

    private Type[] effectTypes;

    int effectIndex = 0;

    protected virtual void OnEnable()
    {
        name = serializedObject.FindProperty("name");
        description = serializedObject.FindProperty("description");
        category = serializedObject.FindProperty("category");
        requiredKnowledge = serializedObject.FindProperty("requiredKnowledge");
        effects = serializedObject.FindProperty("effects");
        action = (AbstractAction)target;
        effectTypes = GetImplementations<ActionEffect>();
    }

    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        serializedObject.Update();
        EditorGUILayout.PropertyField(name);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(category);
        EditorGUILayout.PropertyField(requiredKnowledge);
        EditorGUILayout.PropertyField(effects);
        EditorGUILayout.BeginHorizontal();

        effectIndex = EditorGUILayout.Popup(effectIndex, effectTypes.Select(et => et.ToString()).ToArray());

        if (GUILayout.Button("Add Effect"))
        {
            AddEffect();
        }
        if (GUILayout.Button("Refresh Effects"))
        {
            effectTypes = GetImplementations<ActionEffect>();
        }
        EditorGUILayout.EndHorizontal();
    }

    void AddEffect()
    {
        // action.effects.Add((ActionEffect)Activator.CreateInstance(effectTypes[effectIndex]));
        effects.InsertArrayElementAtIndex(effects.arraySize);
        serializedObject.ApplyModifiedProperties();
        // explicitly set that new item
        action.effects[action.effects.Length-1] = (ActionEffect)Activator.CreateInstance(effectTypes[effectIndex]);
    }

    private static Type[] GetImplementations<T>()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        var interfaceType = typeof(T);
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }
}
