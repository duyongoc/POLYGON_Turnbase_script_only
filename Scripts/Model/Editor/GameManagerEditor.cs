using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(GameManager))]
public class GameManagerEditor : GameCustomEditor
{


    protected GameManager controller;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        controller = target as GameManager;

        DrawCustomDebug();
    }


    private void DrawCustomDebug()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Run Game Online", GUILayout.Width(240), GUILayout.Height(32)))
        {
            controller.RunGameLocal = false;
            SavePrefab();
            SaveScene();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Run Game Local", GUILayout.Width(240), GUILayout.Height(32)))
        {
            controller.RunGameLocal = true;
            SavePrefab();
            SaveScene();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Build Folder", GUILayout.Width(240), GUILayout.Height(32)))
        {
            Utils.ShowWindownExplorer($"{Application.dataPath}/../Build");
        }
        GUILayout.EndHorizontal();
    }


    private void SavePrefab()
    {
        GameObject root = PrefabUtility.GetOutermostPrefabInstanceRoot(Selection.activeGameObject);
        PrefabUtility.ApplyPrefabInstance(root, InteractionMode.UserAction);
    }


    private void SaveScene()
    {
        var scene = SceneManager.GetActiveScene();
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
    }

}
#endif