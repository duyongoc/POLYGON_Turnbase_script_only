using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(PopupBase), true)]
public class PopupBaseEditor : GameCustomEditor
{

    protected bool showMoreDebug = true;
    protected PopupBase myPopup;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        myPopup = target as PopupBase;

        DrawCustomDebug();
    }


    private void DrawCustomDebug()
    {
        GUILayout.Space(20);
        showMoreDebug = EditorGUILayout.Foldout(showMoreDebug, "Show Debug Seting", cheatStyle);

        if (showMoreDebug)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hide Popup"))
            {
                myPopup.CheatHidePopup();
            }

            if (GUILayout.Button("Show Popup"))
            {
                myPopup.CheatShowPopup();
            }
            GUILayout.EndHorizontal();
        }
    }

}
#endif