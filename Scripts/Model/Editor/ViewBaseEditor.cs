using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(ViewBase), true)]
public class ViewBaseEditor : GameCustomEditor
{
    protected bool showMoreDebug = true;
    protected ViewBase myview;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        myview = target as ViewBase;

        DrawCustomDebug();
    }


    private void DrawCustomDebug()
    {
        GUILayout.Space(20);
        showMoreDebug = EditorGUILayout.Foldout(showMoreDebug, "Show Debug Seting", cheatStyle);

        if (showMoreDebug)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hide View"))
            {
                myview.CheatHideView();
            }

            if (GUILayout.Button("Show View"))
            {
                myview.CheatShowView();
            }
            GUILayout.EndHorizontal();
        }
    }

}
#endif