using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();
        LevelController controller = ((LevelController)target);
        if (GUILayout.Button("Add Spawn Element"))
        {
            controller.SpawnList.Add(null);
        }
        if (GUILayout.Button("Sort SpawnList"))
        {
            controller.SortSpawnList();
        }
        if (GUILayout.Button("Show all Gizmos"))
        {
            controller.DrawAllGizmos();
        }
        if (GUILayout.Button("Hide all Gizmos"))
        {
            controller.HideAllGizmos();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
