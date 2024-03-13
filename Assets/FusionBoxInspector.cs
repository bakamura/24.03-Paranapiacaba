#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FusionBox))]
public class FusionBoxInspector : Editor
{
    SerializedProperty matrixDimensions, distanceBetweenFuses, fuseObjectsParent, fuseUIParent, changeFuseInput, activateFuseInput, fuseLayer;

    public override void OnInspectorGUI()
    {       
        EditorGUILayout.PropertyField(matrixDimensions, new GUIContent("Fuse Box Dimensions"));
        EditorGUILayout.PropertyField(distanceBetweenFuses, new GUIContent("Distance Between Fuses"));
        EditorGUILayout.PropertyField(fuseObjectsParent, new GUIContent("Fuse Box Transform"));
        EditorGUILayout.PropertyField(fuseUIParent, new GUIContent("Fuse Box UI Transform"));
        EditorGUILayout.PropertyField(changeFuseInput, new GUIContent("Select Fuse Input Reference"));
        EditorGUILayout.PropertyField(activateFuseInput, new GUIContent("Activate Fuse Input Reference"));
        EditorGUILayout.PropertyField(fuseLayer, new GUIContent("Fuse Collision Layer"));

        FusionBox instance = (FusionBox)target;
        if (GUILayout.Button("Rename Fuses"))
        {
            instance.RenameObjects();            
        }

        if (GUILayout.Button("Reposition Fuses"))
        {
            instance.RepositionFuses();
        }

        if(GUILayout.Button("Update Fuses Active State"))
        {
            instance.UpdateFusesActiveState();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        matrixDimensions = serializedObject.FindProperty("_matrixDimensions");
        distanceBetweenFuses = serializedObject.FindProperty("_distanceBetweenFuses");
        fuseObjectsParent = serializedObject.FindProperty("_fuseObjectsParent");
        fuseUIParent = serializedObject.FindProperty("_fuseUIParent");
        changeFuseInput = serializedObject.FindProperty("_changeFuseInput");
        activateFuseInput = serializedObject.FindProperty("_activateFuseInput");
        fuseLayer = serializedObject.FindProperty("_fuseLayer");
    }
}
#endif