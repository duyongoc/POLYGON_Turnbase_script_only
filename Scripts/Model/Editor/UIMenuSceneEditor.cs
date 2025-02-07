using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(UIMenuScene))]
public class UIGameSceneEditor : GameCustomEditor
{


    protected bool showView = false;
    protected bool showViewPopup = true;
    protected bool showMoreDebug = true;
    protected UIMenuScene controller;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        controller = target as UIMenuScene;

        // DrawCustomView();
        DrawCustomDebug();
    }


    private void DrawCustomView()
    {
        serializedObject.Update();
        showViewPopup = EditorGUILayout.Foldout(showViewPopup, "View Popup", foldoutStyle);
        // myController.stringName = EditorGUILayout.TextField("Name :", myController.stringName);

        if (showViewPopup)
        {
            // var privacy = serializedObject.FindProperty("viewPrivacy");
            // var login = serializedObject.FindProperty("viewPrivacy");
            // var privacy = serializedObject.FindProperty("viewLogin");
            // EditorGUILayout.PropertyField(privacy,  new GUIContent("Viwewe"));

            // privacy.serializedObject = EditorGUILayout.ObjectField("Privacy", myController.ViewPrivacy, typeof(ViewPrivacy), true);
            // myController.ViewPrivacy = (ViewPrivacy)EditorGUILayout.ObjectField("Privacy", myController.ViewPrivacy, typeof(ViewPrivacy), true);
            // myController.ViewLogin = (ViewLogin)EditorGUILayout.ObjectField("Login", myController.ViewLogin, typeof(ViewLogin), true);
            // myController.ViewLobby = (ViewLobby)EditorGUILayout.ObjectField("Lobby", myController.ViewLobby, typeof(ViewLobby), true);
            // myController.ViewProfile = (ViewProfile)EditorGUILayout.ObjectField("Profile", myController.ViewProfile, typeof(ViewProfile), true);
            // myController.ViewFormation = (ViewFormation)EditorGUILayout.ObjectField("Formation", myController.ViewFormation, typeof(ViewFormation), true);
            // myController.ViewEquipment = (ViewEquipment)EditorGUILayout.ObjectField("Equipment", myController.ViewEquipment, typeof(ViewEquipment), true);
            // myController.ViewCharacter = (ViewCharacter)EditorGUILayout.ObjectField("Character", myController.ViewCharacter, typeof(ViewCharacter), true);
            // myController.ViewBloodUnionGeneration = (ViewBloodUnionGeneration)EditorGUILayout.ObjectField("BloodUnionGeneration", myController.ViewBloodUnionGeneration, typeof(ViewBloodUnionGeneration), true);
            // myController.ViewSkillCombination = (ViewSkillSynthesis)EditorGUILayout.ObjectField("SkillCombination", myController.ViewSkillCombination, typeof(ViewSkillSynthesis), true);
        }

        serializedObject.ApplyModifiedProperties();
    }


    private void DrawCustomDebug()
    {
        GUILayout.Space(20);
        showMoreDebug = EditorGUILayout.Foldout(showMoreDebug, "Show Debug Seting", cheatStyle);

        if (showMoreDebug)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hide All View", GUILayout.Width(240), GUILayout.Height(32)))
            {
                controller.HideAllView();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Views Game", GUILayout.Width(240), GUILayout.Height(32)))
            {
                controller.LoadViewEditor();
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif