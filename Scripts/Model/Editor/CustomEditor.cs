
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameCustomEditor : Editor
{
    protected static bool styleDefined = false;
    protected static GUIStyle headerStyle;
    protected static GUIStyle foldoutStyle;
    protected static GUIStyle cheatStyle;



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DefineStyle();
    }


    protected static void DefineStyle()
    {
        if (styleDefined) return;
        styleDefined = true;

        headerStyle = new GUIStyle("Label");
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.normal.textColor = Color.black;

        foldoutStyle = new GUIStyle("Foldout");
        foldoutStyle.fontStyle = FontStyle.Normal;
        foldoutStyle.normal.textColor = Color.white;

        cheatStyle = new GUIStyle("Foldout");
        cheatStyle.fontStyle = FontStyle.Bold;
        cheatStyle.normal.textColor = Color.red;
    }

}
#endif