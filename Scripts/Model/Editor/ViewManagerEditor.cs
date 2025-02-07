using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(ViewManager))]
public class ViewManagerEditor : GameCustomEditor
{

    
    protected bool showView = false;
    protected bool showViewPopup = true;
    protected bool showMoreDebug = true;
    protected ViewManager controller;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        controller = target as ViewManager;

        // DrawCustomView();
        // DrawCustomPopup();
        DrawCustomDebug();

    }

    private void DrawCustomDebug()
    {
        GUILayout.Space(20);
        showMoreDebug = EditorGUILayout.Foldout(showMoreDebug, "Show Debug Seting", cheatStyle);

        if (showMoreDebug)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hide Popup", GUILayout.Width(240), GUILayout.Height(32)))
            {
                controller.HideAllView();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Popup", GUILayout.Width(240), GUILayout.Height(32)))
            {
                controller.LoadViewEditor();
            }
            GUILayout.EndHorizontal();
        }
    }

}
#endif