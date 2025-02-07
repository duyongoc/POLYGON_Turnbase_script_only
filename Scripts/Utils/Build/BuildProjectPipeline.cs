#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
// using Unity.EditorCoroutines.Editor;
using System.Collections;

public static class BuildProjectPipeline
{

    // private static string folderBuild = $"{Application.dataPath}/../Build";
    // private static string buildPath = $"{folderBuild}/{"Build"}";

    // private static string buildName;
    // private static string buildHosting;
    // private static string buildVersion;
    // private static bool isAppBundle = false;
    // private static int playerPrefsbuildNumber;


    // private static string outputLog = $"{Application.dataPath}/../Autobuild/output.log";
    // private static string showOutputLog = $"{Application.dataPath}/../Autobuild/show_output.log";


    // [UnityEditor.Callbacks.DidReloadScripts]
    // private static void OnScriptsReloaded()
    // {
    // }


    // [InitializeOnLoadMethod]
    // private static void OnInitialized()
    // {
    //     Debug.Log("OnInitialized " + isBuilding);
    //     if (isBuilding)
    //     {
    //         Debug.Log("Waiting for define symbols compiler.. dont touch!");
    //         BuildProject(BuildTargetGroup.WebGL, BuildTarget.WebGL);
    //         Debug.Log("Build done");
    //     }

    //     // var buildVersion = PlayerSettings.bundleVersion;
    //     // var buildHosting = CONSTS.HOST_ENDPOINT.Replace("https://", "").Replace("/", "").Replace("-game-api.w3w.app", "");
    //     // var buildEditor = $"Server: {buildHosting.ToUpper()}_{buildVersion}";
    //     // BuildProject(BuildTargetGroup.WebGL, BuildTarget.WebGL);
    // }


    // [MenuItem("Build/Build Web/Server DEV", false, 100)]
    // public static void BuildWeb_DEV()
    // {
    //     ChangeSetting_DEV();
    //     BuildProject("DEV", BuildTargetGroup.WebGL, BuildTarget.WebGL);
    // }


    // [MenuItem("Build/Build Web/Server TEST", false, 100)]
    // public static void BuildWeb_TEST()
    // {
    //     ChangeSetting_TEST();
    //     BuildProject("TEST", BuildTargetGroup.WebGL, BuildTarget.WebGL);
    // }


    // [MenuItem("Build/Build Web/Server LIVE", false, 100)]
    // public static void BuildWeb_LIVE()
    // {
    //     ChangeSetting_LIVE();
    //     BuildProject("LIVE", BuildTargetGroup.WebGL, BuildTarget.WebGL);
    // }




    // [MenuItem("Build/Build Android APK/Server DEV", false, 700)]
    // public static void BuildAndroidApk_DEV()
    // {
    //     isAppBundle = false;
    //     ChangeSetting_DEV();
    //     BuildProject("DEV", BuildTargetGroup.Android, BuildTarget.Android);
    // }


    // [MenuItem("Build/Build Android APK/Server TEST", false, 700)]
    // public static void BuildAndroidApk_TEST()
    // {
    //     isAppBundle = false;
    //     ChangeSetting_TEST();
    //     BuildProject("TEST", BuildTargetGroup.Android, BuildTarget.Android);
    // }


    // [MenuItem("Build/Build Android APK/Server LIVE", false, 700)]
    // public static void BuildAndroidApk_LIVE()
    // {
    //     isAppBundle = false;
    //     ChangeSetting_LIVE();
    //     BuildProject("LIVE", BuildTargetGroup.Android, BuildTarget.Android);
    // }



    // [MenuItem("Build/Build Android AAB/Server DEV", false, 700)]
    // public static void BuildAndroidAab_DEV()
    // {
    //     isAppBundle = true;
    //     ChangeSetting_DEV();
    //     BuildProject("DEV", BuildTargetGroup.Android, BuildTarget.Android);
    // }


    // [MenuItem("Build/Build Android AAB/Server TEST", false, 700)]
    // public static void BuildAndroidAab_TEST()
    // {
    //     isAppBundle = true;
    //     ChangeSetting_TEST();
    //     BuildProject("TEST", BuildTargetGroup.Android, BuildTarget.Android);
    // }


    // [MenuItem("Build/Build Android AAB/Server LIVE", false, 700)]
    // public static void BuildAndroidAab_LIVE()
    // {
    //     isAppBundle = true;
    //     ChangeSetting_LIVE();
    //     BuildProject("LIVE", BuildTargetGroup.Android, BuildTarget.Android);
    // }



    // [MenuItem("Build/Build Android DEV apk and aab", false, 1000)]
    // public static void BuildAndroid_DEV_ApkAndAab()
    // {
    //     BuildAndroidApk_DEV();
    //     BuildAndroidAab_DEV();
    // }


    // [MenuItem("Build/Build Android TEST apk and aab", false, 1000)]
    // public static void BuildAndroid_TEST_ApkAndAab()
    // {
    //     BuildAndroidApk_TEST();
    //     BuildAndroidAab_TEST();
    // }


    // [MenuItem("Build/Build Android LIVE apk and aab", false, 1000)]
    // public static void BuildAndroid_LIVE_ApkAndAab()
    // {
    //     BuildAndroidApk_LIVE();
    //     BuildAndroidAab_LIVE();
    // }



    // ///===



    // public static void ChangeSetting_DEV()
    // {
    //     // var gameMgr = GameObject.FindObjectOfType<GameManager>();
    //     // gameMgr.showCheat = true;
    //     // gameMgr.RUN_WEB_NFT = true;
    //     // gameMgr.RunOnUnityEditor = false;
    //     // gameMgr.SetViewGame(EViewGame.Login);
    //     // buildVersion = gameMgr.buildVersion;

    //     // switch to dev
    //     SwitchToDefineSymbol_DEV();
    //     AssetDatabase.Refresh();

    //     SavePrefab();
    //     SaveScene();
    // }


    // public static void ChangeSetting_TEST()
    // {
    //     // var gameMgr = GameObject.FindObjectOfType<GameManager>();
    //     // gameMgr.showCheat = true;
    //     // gameMgr.RUN_WEB_NFT = true;
    //     // gameMgr.RunOnUnityEditor = false;
    //     // gameMgr.SetViewGame(EViewGame.Login);
    //     // buildVersion = gameMgr.buildVersion;

    //     // switch to test
    //     SwitchToDefineSymbol_TEST();
    //     AssetDatabase.Refresh();

    //     SavePrefab();
    //     SaveScene();
    // }


    // public static void ChangeSetting_LIVE()
    // {
    //     // var gameMgr = GameObject.FindObjectOfType<GameManager>();
    //     // gameMgr.showCheat = false;
    //     // gameMgr.RUN_WEB_NFT = true;
    //     // gameMgr.RunOnUnityEditor = false;
    //     // gameMgr.SetViewGame(EViewGame.Login);
    //     // buildVersion = gameMgr.buildVersion;

    //     // switch to live
    //     SwitchToDefineSymbol_LIVE();
    //     AssetDatabase.Refresh();

    //     SavePrefab();
    //     SaveScene();
    // }


    // [MenuItem("Build/Switch to DEV", false, 0)]
    // public static void SwitchToDefineSymbol_DEV()
    // {
    //     RemoveAllDefine();
    //     AddScriptingDefine("DEV");
    // }


    // [MenuItem("Build/Switch to TEST", false, 0)]
    // public static void SwitchToDefineSymbol_TEST()
    // {
    //     RemoveAllDefine();
    //     AddScriptingDefine("TEST");
    // }


    // [MenuItem("Build/Switch to LIVE", false, 0)]
    // public static void SwitchToDefineSymbol_LIVE()
    // {
    //     RemoveAllDefine();
    //     AddScriptingDefine("LIVE");
    // }


    // public static void RemoveAllDefine()
    // {
    //     RemoveScriptingDefine("DEV");
    //     RemoveScriptingDefine("TEST");
    //     RemoveScriptingDefine("LIVE");
    // }



    // private static void SavePrefab()
    // {
    //     // var gameMgr = GameObject.FindObjectOfType<GameManager>();
    //     // GameObject root = PrefabUtility.GetOutermostPrefabInstanceRoot(gameMgr.gameObject);
    //     // PrefabUtility.ApplyPrefabInstance(root, InteractionMode.UserAction);
    // }


    // private static void SaveScene()
    // {
    //     var scene = SceneManager.GetActiveScene();
    //     EditorSceneManager.MarkSceneDirty(scene);
    //     EditorSceneManager.SaveScene(scene);
    //     AssetDatabase.SaveAssets();
    // }


    // public static void AddScriptingDefine(string define)
    // {
    //     var target = EditorUserBuildSettings.activeBuildTarget;
    //     var group = BuildPipeline.GetBuildTargetGroup(target);
    //     var current = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
    //     if (current.Contains(define))
    //         return;

    //     string result = current + ";" + define;
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(group, result);
    // }


    // public static void RemoveScriptingDefine(string define)
    // {
    //     var target = EditorUserBuildSettings.activeBuildTarget;
    //     var group = BuildPipeline.GetBuildTargetGroup(target);
    //     var current = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
    //     if (!current.Contains(define))
    //         return;

    //     string result = current.Replace(define, String.Empty);
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(group, result);
    // }


    // private static void BuildProject(string nameBuild, BuildTargetGroup targetGroup, BuildTarget buildTarget)
    // {
    //     PlayerSettings.companyName = "Bacoor";
    //     PlayerSettings.bundleVersion = $"{buildVersion}";
    //     PlayerSettings.SplashScreen.show = true;
    //     PlayerSettings.SplashScreen.showUnityLogo = false;



    //     buildName = $"{nameBuild}_{buildVersion}";
    //     // Debug.Log($"Building project: {buildHosting}_{buildVersion} | targetGroup: {targetGroup} | buildTarget: {buildTarget}");

    //     // specific setting
    //     SettingForBuildWebGL(targetGroup);
    //     SettingForBuildAndroid(targetGroup);
    //     buildPath = $"{Application.dataPath}/../Build/{buildName}";
    //     Debug.Log($"Building project buildPath: {buildPath}");

    //     // building project and show report
    //     EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, buildTarget);
    //     BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, buildTarget, BuildOptions.None);
    //     CheckReportLOG(report);
    // }


    // private static void SettingForBuildWebGL(BuildTargetGroup targetGroup)
    // {
    //     if (targetGroup == BuildTargetGroup.WebGL)
    //     {
    //         PlayerSettings.productName = $"";
    //     }
    // }


    // private static void SettingForBuildAndroid(BuildTargetGroup targetGroup)
    // {
    //     if (targetGroup == BuildTargetGroup.Android)
    //     {
    //         if (isAppBundle)
    //         {
    //             buildName += "_android.aab";
    //             EditorUserBuildSettings.buildAppBundle = true;
    //             PlayerSettings.Android.useAPKExpansionFiles = true;
    //         }
    //         else
    //         {
    //             buildName += "_android.apk";
    //             EditorUserBuildSettings.buildAppBundle = false;
    //             PlayerSettings.Android.useAPKExpansionFiles = false;
    //         }

    //         PlayerSettings.productName = $"";
    //         Screen.orientation = ScreenOrientation.LandscapeLeft;
    //     }
    // }


    // private static void CheckReportLOG(BuildReport report)
    // {
    //     if (report.summary.result == BuildResult.Succeeded)
    //     {
    //         DoAfterBuild();
    //         AssetDatabase.Refresh();
    //         var error = report.summary.totalErrors;
    //         var warning = report.summary.totalWarnings;
    //         var size = report.summary.totalSize / Mathf.Pow(1024, 2);
    //         var time = report.summary.totalTime.ToString(@"hh\:mm\:ss");
    //         Debug.Log($"Build [succeeded]  |  Size: {size} MB  |  Time: {time}  |  {warning} warnings  |  {error} errors  \nBuild path: {buildPath}");
    //     }
    //     else
    //     {
    //         Debug.LogError("\nBuild [Failed] \nBuild path: {path}");
    //     }
    // }


    // private static void DoAfterBuild()
    // {
    //     try
    //     {
    //         ShowWindownExplorer(buildPath);
    //         run_copy_python(buildPath, playerPrefsbuildNumber.ToString());
    //         PlayerPrefs.SetInt("playerPrefsbuildNumber", playerPrefsbuildNumber);
    //     }
    //     catch
    //     {
    //     }
    // }


    // private static void ShowWindownExplorer(string buildPath)
    // {
    //     System.Diagnostics.Process process = new System.Diagnostics.Process();
    //     System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
    //     startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    //     startInfo.FileName = "cmd.exe";
    //     startInfo.Arguments = string.Format("/C start {0}", buildPath);
    //     process.StartInfo = startInfo;
    //     process.Start();
    // }



    // [ContextMenu("run python")]
    // private static void run_copy_python(string build_path, string version)
    // {
    //     // var build_path = " F:/work/tool/Assets/../Build/Build-test-50";
    //     // var pythonArgs = string.Format("{0} {1} {2} {3}", python_path, build_path, unity_build_path, version);

    //     // noted: its depend on your location 
    //     var python_path = $"{Application.dataPath}/../Autobuild/copyBuildFolder.py";
    //     var unity_build_path = @"C:\work\nftgame-client\static\unitybuild\Build";
    //     var pythonArgs = string.Format("{0} {1} {2} {3}", python_path, Path.Combine(build_path, "Build"), unity_build_path, version);

    //     System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
    //     start.FileName = @"C:\Users\duyongoc\AppData\Local\Programs\Python\Python36\python.exe";
    //     start.Arguments = pythonArgs;
    //     start.UseShellExecute = false;
    //     start.RedirectStandardOutput = true;
    //     using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
    //     {
    //         using (StreamReader reader = process.StandardOutput)
    //         {
    //             string result = reader.ReadToEnd();
    //             Console.Write(result);
    //         }
    //     }
    // }



    // public static void BuildProject()
    // {
    //     SettingBuild(buildNumber: 1);
    //     BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.WebGL, BuildOptions.None);
    //     UpdateBuildReport(report);
    // }


    // public static void BuildProjectIncreaseVersion()
    // {
    //     int buildNumber = PlayerPrefs.GetInt("BUILDNUMBER", 0);
    //     SettingBuild(buildNumber);
    //     PlayerPrefs.SetInt("BUILDNUMBER", buildNumber);

    //     path += $"_{PlayerSettings.bundleVersion}";
    //     BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.WebGL, BuildOptions.None);
    //     UpdateBuildReport(report);
    // }



    // private static void SettingBuild(int buildNumber)
    // {
    //     buildNumber += 1;
    //     PlayerSettings.companyName = "";
    //     PlayerSettings.productName = "";
    //     PlayerSettings.bundleVersion = "0.0.1";
    //     PlayerSettings.bundleVersion += ("." + buildNumber);
    //     PlayerSettings.SplashScreen.show = false;
    // }


    // public static void UpdateBuildReport(BuildReport report)
    // {
    //     string logResult = string.Empty;
    //     switch (report.summary.result)
    //     {
    //         case BuildResult.Succeeded:
    //             OnResultBuildSuccess(report.summary, logResult);
    //             break;

    //         case BuildResult.Failed:
    //             OnResultBuildFailed(report.summary, logResult);
    //             break;
    //     }

    //     File.Copy(outputLog, showOutputLog, true);
    //     using (StreamWriter writer = new StreamWriter(showOutputLog, true))
    //     {
    //         writer.WriteLine(logResult);
    //     }
    // }


    // private static void OnResultBuildSuccess(BuildSummary summary, string logResult)
    // {
    //     var error = summary.totalErrors;
    //     var warning = summary.totalWarnings;
    //     var size = summary.totalSize / Mathf.Pow(1024, 2);
    //     var time = summary.totalTime.ToString(@"hh\:mm\:ss");
    //     Debug.Log($"Build [succeeded]  |  Size: {size} MB  |  Time: {time}  |  {warning} warnings  |  {error} errors  \nBuild path: {path}");
    // }


    // private static void OnResultBuildFailed(BuildSummary summary, string logResult)
    // {
    //     Debug.LogError("\nBuild [Failed] \nBuild path: {path}");
    // }


    // public static void CopyAndPushToGit()
    // {
    //     var pathFolder = new System.Uri($"{path}");
    //     var commitVersion = $"-update_build_web{PlayerSettings.bundleVersion}";
    //     string cmdText = $"{pathFolder.LocalPath} {commitVersion}";
    //     string cmdPath = $"{Application.dataPath}/../Autobuild/copy_and_push_into_git.cmd";

    //     System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(cmdPath, cmdText);
    //     System.Diagnostics.Process process = new System.Diagnostics.Process();
    //     startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    //     startInfo.RedirectStandardOutput = true;
    //     startInfo.UseShellExecute = false;
    //     process.StartInfo = startInfo;
    //     process.Start();

    //     Debug.Log($"CopyAndPushToGit:\n{process.StandardOutput.ReadToEnd()}");
    // }


    // public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, string define)
    // {
    //     string current = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
    //     if (current.Contains(define))
    //         return;

    //     string result = current + ";" + define;
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, result);
    //     // PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.WebGL, result);
    // }

    // public static void RemoveScriptingDefine(BuildTargetGroup buildTargetGroup, string define)
    // {
    //     string current = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
    //     if (!current.Contains(define))
    //         return;

    //     string result = current.Replace(define, String.Empty);
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, result);
    //     // PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.WebGL, result);
    // }

}
#endif

