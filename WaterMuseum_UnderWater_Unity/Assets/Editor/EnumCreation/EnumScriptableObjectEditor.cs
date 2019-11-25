using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnumScriptableObject))]
public class EnumScriptableObjectEditor : Editor
{
    private EnumScriptableObject editingObject;

    private void OnEnable()
    {
        editingObject = (EnumScriptableObject)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
            EnumWriter.WriteToEnum(editingObject.name, editingObject.enumNames);
    }
}