using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicGridLayoutGroup))]
public class DynamicGridLayoutGroupEditor : Editor {

    DynamicGridLayoutGroup obj;

    protected virtual void OnEnable() {
        obj = ((DynamicGridLayoutGroup)target);
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        /*
        serializedObject.Update();
        EditorGUILayout.PropertyField(_padding, true);
        EditorGUILayout.PropertyField(_spacing, true);
        EditorGUILayout.PropertyField(_startCorner, true);
        EditorGUILayout.PropertyField(_startAxis, true);
        EditorGUILayout.PropertyField(_adjustSize, true);
        EditorGUILayout.PropertyField(_useRatio, true);



        if (obj.useRatio == true) {
            EditorGUILayout.PropertyField(_ratioSize, true);
        } else {

        }



        serializedObject.ApplyModifiedProperties();
        */
    }
}
