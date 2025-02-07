#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Build.Reporting;
using UnityEditor.Build;

public class PlatformBuilder
{


    // [private]
    private static string buildName;
    private static string buildPath;
    private static string buildFolder;
    private static bool isAndroidAAB = false;



    [MenuItem("Build/Build iOS", false, 100)]
    public static void Build_Ios()
    {
        BuildProject("", false, BuildTargetGroup.iOS, BuildTarget.iOS);
    }


    [MenuItem("Build/Build WebGL", false, 200)]
    public static void Build_Web()
    {
        BuildProject("", false, BuildTargetGroup.WebGL, BuildTarget.WebGL);
    }

    [MenuItem("Build/Build WebGL And Run", false, 200)]
    public static void Build_WebAndRun()
    {
        BuildProject("", true, BuildTargetGroup.WebGL, BuildTarget.WebGL);
    }


    [MenuItem("Build/Build Window", false, 300)]
    public static void Build_Window()
    {
        BuildProject("", false, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
    }


    [MenuItem("Build/Build Android APK", false, 400)]
    public static void Build_AndroidAPK()
    {
        isAndroidAAB = false;
        BuildProject("", false, BuildTargetGroup.Android, BuildTarget.Android);
    }


    [MenuItem("Build/Build Android AAB", false, 401)]
    public static void Build_AndroidABB()
    {
        isAndroidAAB = true;
        BuildProject("", false, BuildTargetGroup.Android, BuildTarget.Android);
    }


    [MenuItem("Build/Build All", false, 500)]
    public static void Build_All()
    {
        Build_Ios();
        Build_Web();
        Build_Window();
        Build_AndroidAPK();
        Build_AndroidABB();
    }


    // [MenuItem("Build/WebGL and APK", false, 500)]
    // public static void Build_WebAndAndroid()
    // {
    //     Build_Web();
    //     Build_AndroidAPK();
    // }



    [MenuItem("Build/UpdateKeyStore")]
    static void UpdateKeyStore()
    {
        PlayerSettings.Android.useCustomKeystore = true;
        string fullPath = Application.dataPath + "/android.keystore";
        try
        {
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            PlayerSettings.Android.keystoreName = dir.FullName.Replace("\\", "/");
        }
        catch (Exception ex)
        {
            Debug.LogError("Key store not found at" + fullPath + " \n" + ex.StackTrace);
        }

        //com.game.duyongoc
        // PlayerSettings.Android.keystorePass = "123456@a";
        // PlayerSettings.Android.keyaliasPass = "123456@a";
        // PlayerSettings.Android.keyaliasName = "game";
    }


    private static void BuildProject(string nameBuild, bool autoRunPlayer, BuildTargetGroup targetGroup, BuildTarget buildTarget)
    {
        PlayerSettings.companyName = "duyongoc";
        PlayerSettings.productName = $"Polygon_Turnbase";
        PlayerSettings.bundleVersion = $"{Application.version}";
        PlayerSettings.SplashScreen.show = true;
        PlayerSettings.SplashScreen.showUnityLogo = false;


        // setting for building target group
        SettingForBuildIos(targetGroup, nameBuild);
        SettingForBuildWebGL(targetGroup, nameBuild);
        SettingForBuildWindow(targetGroup, nameBuild);
        SettingForBuildAndroid(targetGroup, nameBuild);

        // building project and show report
        EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, buildTarget);
        var buildOption = autoRunPlayer ? BuildOptions.AutoRunPlayer : BuildOptions.None;
        var report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, buildTarget, buildOption);


        // open build folder 
        ShowWindowExplorer(buildFolder);
        DeleteBurstDebugInformationFolder(report);
    }



    private static void SettingForBuildIos(BuildTargetGroup targetGroup, string nameBuild)
    {
        if (targetGroup == BuildTargetGroup.iOS)
        {
            buildName = $"{PlayerSettings.productName}_{nameBuild}{Application.version}";
            buildPath = $"{Application.dataPath}/../build/iOS_{buildName}";
            buildFolder = $"{Application.dataPath}/../build";

            string iosTempFileContent = String.Format("OUTPUT_IPA_NAME={0}", buildPath);
            File.WriteAllText(Application.dataPath + "/../ios_tmp.properties", iosTempFileContent);
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, "com.game.duyongoc");
        }
    }


    private static void SettingForBuildWebGL(BuildTargetGroup targetGroup, string nameBuild)
    {
        if (targetGroup == BuildTargetGroup.WebGL)
        {
            // var versionCode = PlayerSettings.Android.bundleVersionCode.ToString();
            // buildName = $"{PlayerSettings.productName}_{nameBuild}_{version}_{versionCode}";

            var version = PlayerSettings.bundleVersion.Replace(".", "");
            buildName = $"{PlayerSettings.productName}_{nameBuild}{version}";
            buildPath = $"{Application.dataPath}/../build/Web_{buildName}";
            buildFolder = buildPath;

            PlayerSettings.WebGL.decompressionFallback = true;
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
            // PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        }
    }

    private static void SettingForBuildWindow(BuildTargetGroup targetGroup, string nameBuild)
    {
        if (targetGroup == BuildTargetGroup.Standalone)
        {
            // var versionCode = PlayerSettings.Android.bundleVersionCode.ToString();
            // buildName = $"{PlayerSettings.productName}_{nameBuild}_{version}_{versionCode}";

            var version = PlayerSettings.bundleVersion.Replace(".", "");
            buildName = $"{PlayerSettings.productName}_{nameBuild}{version}";
            buildPath = $"{Application.dataPath}/../build/Win_{buildName}/{PlayerSettings.productName}";
            buildFolder = buildPath;
        }
    }


    private static void SettingForBuildAndroid(BuildTargetGroup targetGroup, string nameBuild)
    {
        if (targetGroup == BuildTargetGroup.Android)
        {
            // var versionCode = PlayerSettings.Android.bundleVersionCode.ToString();
            // buildName = $"{PlayerSettings.productName}_{nameBuild}_{version}_{versionCode}";

            var version = PlayerSettings.bundleVersion.Replace(".", "");
            buildName = $"{PlayerSettings.productName}_{nameBuild}{version}";
            buildPath = $"{Application.dataPath}/../build/{buildName}";
            buildFolder = $"{Application.dataPath}/../build/";

            if (isAndroidAAB)
            {
                buildPath += ".aab";
                EditorUserBuildSettings.buildAppBundle = true;
                // PlayerSettings.Android.splitApplicationBinary = true;
            }
            else
            {
                buildPath += ".apk";
                EditorUserBuildSettings.buildAppBundle = false;
                // PlayerSettings.Android.splitApplicationBinary = false;
            }

            // UpdateKeyStore();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.game.duyongoc");
        }
    }


    private static void ShowWindowExplorer(string buildFolder)
    {
        Debug.Log("buildFolder: " + buildFolder);
        var process = new System.Diagnostics.Process();
        var startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = string.Format("/C start {0}", buildFolder);
        process.StartInfo = startInfo;
        process.Start();
    }


    private static void DeleteBurstDebugInformationFolder([NotNull] BuildReport buildReport)
    {
        string outputPath = buildReport.summary.outputPath;

        try
        {
            var applicationName = Path.GetFileNameWithoutExtension(outputPath);
            var outputFolder = Path.GetDirectoryName(outputPath);
            UnityEngine.Assertions.Assert.IsNotNull(outputFolder);
            outputFolder = Path.GetFullPath(outputFolder);


            // delete mapping txt 
            var mappingPath = Path.Combine(outputFolder, $"{applicationName}_mapping.txt");
            if (File.Exists(mappingPath))
            {
                Debug.Log($">Deleting mapping.txt at path  {mappingPath}");
                File.Delete(mappingPath);
            }

            // delete burst folder
            var burstDebugPath = Path.Combine(outputFolder, $"{PlayerSettings.productName}_BurstDebugInformation_DoNotShip");
            // Debug.Log("burstDebugInformationDirectoryPath: " + burstDebugPath);
            if (Directory.Exists(burstDebugPath))
            {
                Debug.Log($">Deleting Burst debug information folder at path {burstDebugPath}");
                Directory.Delete(burstDebugPath, true);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"An unexpected exception occurred while performing build cleanup: {e}");
        }
    }



    // private static void BuildIos()
    // {
    //     PlayerSettings.companyName = "";
    //     PlayerSettings.bundleVersion = $"{Application.version}";
    //     PlayerSettings.SplashScreen.show = true;
    //     PlayerSettings.SplashScreen.showUnityLogo = false;
    //     PlayerSettings.productName = $"";

    //     RemoveScriptingDefine(BuildTargetGroup.iOS, "ENABLE_CHEAT");
    //     string productName = PlayerSettings.productName + "_" + PlayerSettings.bundleVersion.Replace(".", "") + "-" + PlayerSettings.iOS.buildNumber;
    //     string outFolder = "./build/ios";

    //     string iosTempFileContent = String.Format("OUTPUT_IPA_NAME={0}", productName);
    //     File.WriteAllText(Application.dataPath + "/../ios_tmp.properties", iosTempFileContent);
    //     BuildPipeline.BuildPlayer(GetSceneList(), outFolder, BuildTarget.iOS, BuildOptions.None);
    // }


    // private static void BuildIosCheat()
    // {
    //     AddScriptingDefine(BuildTargetGroup.iOS, "ENABLE_CHEAT");
    //     string productName = PlayerSettings.productName + "_" + PlayerSettings.bundleVersion.Replace(".", "") + "-" + PlayerSettings.iOS.buildNumber + "_Cheat";
    //     string outFolder = "./build/ios";

    //     string iosTempFileContent = String.Format("OUTPUT_IPA_NAME={0}", productName);
    //     File.WriteAllText(Application.dataPath + "/../ios_tmp.properties", iosTempFileContent);
    //     BuildPipeline.BuildPlayer(GetSceneList(), outFolder, BuildTarget.iOS, BuildOptions.Development);
    // }


    public static void ArchiveIosDevelopmentBuild()
    {
        ChangeNameBuild("Cheat");
    }


    public static void ArchiveIosDistributionBuild()
    {
        ChangeNameBuild("Release");
    }


    public static void Copy_Podfile()
    {
        string filePath = Application.dataPath;
        string iosBuildFolder = "./_build/ios";
        File.Copy(filePath + "/Pod/Podfile", iosBuildFolder + "/Podfile");
    }


    public static void Run_PodScript()
    {
        var proc = new System.Diagnostics.ProcessStartInfo("open", Application.dataPath + "/Pod/pods.command");
        proc.RedirectStandardOutput = true;
        proc.UseShellExecute = false;
        proc.CreateNoWindow = true;
        proc.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        var process = System.Diagnostics.Process.Start(proc);

        Debug.Log("Run" + Application.dataPath + "/Pod/pods.command");
        while (!process.StandardOutput.EndOfStream)
        {
            Debug.Log("" + process.StandardOutput.ReadLine());
        }
    }


    public static void ChangeNameBuild(string fileSuffix)
    {
        var path = "./_build/ios/";
        var suffixTime = DateTime.Now.ToString();
        // "./_build/ios/build/Release-iphoneos/";
        suffixTime = suffixTime.Replace(" ", "_").Replace("/", "-");

        var pathBuild = path + "Unity-iPhone.xcarchive";
        var pathMove = path + "Unity-iPhone_" + fileSuffix + "_" + suffixTime + ".xcarchive";

        Debug.Log("pathBuild " + pathBuild);
        Debug.Log("pathMove " + pathMove);
        Directory.Move(pathBuild, pathMove);
    }


    public static void ArchiveIosBuild(string folder, string fileSuffix)
    {
        string path = Application.dataPath + "./_build/ios" + folder + "/build/Release-iphoneos/";
        Debug.Log("ArchiveIosBuild path: " + path);

        if (Directory.Exists(path))
        {
            path = Path.GetFullPath(path);
            if (File.Exists(path + "AAF.ipa"))
            {
                if (!Directory.Exists(path + "Archive/"))
                    Directory.CreateDirectory(path + "Archive/");
                string fileName = "AAF_" + PlayerSettings.bundleVersion.Replace(".", "")
                                + "-" + PlayerSettings.Android.bundleVersionCode.ToString()
                                + "_" + fileSuffix + ".ipa";

                Debug.Log("[ArchiveIosBuild] Copy ipa file to " + path + "Archive/" + fileName);
                File.Copy(path + "AAF.ipa", path + "Archive/" + fileName, true);
            }

            Debug.Log("path: " + path);

            var suffixTime = DateTime.Now;
            var pathMove = Path.Combine(path, "/Unity-iPhone" + suffixTime + ".xcarchive");
            File.Move(path + "/Unity-iPhone.xcarchive", pathMove);
            Debug.Log("ArchiveIosBuild pathMove: " + pathMove);
        }
    }


    private static void DeleteFolder(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }


    // private static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, string define)
    // {
    //     string current = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
    //     if (current.Contains(define))
    //         return;
    //     string result = current + ";" + define;
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, result);
    // }


    // private static void RemoveScriptingDefine(BuildTargetGroup buildTargetGroup, string define)
    // {
    //     string current = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
    //     if (!current.Contains(define))
    //         return;
    //     string result = current.Replace(define, String.Empty);
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, result);
    // }


    private static void AddScriptingDefine(UnityEditor.Build.NamedBuildTarget buildTargetGroup, string define)
    {
        string current = PlayerSettings.GetScriptingDefineSymbols(buildTargetGroup);
        if (current.Contains(define))
            return;
        string result = current + ";" + define;
        PlayerSettings.SetScriptingDefineSymbols(buildTargetGroup, result);
    }


    private static void RemoveScriptingDefine(UnityEditor.Build.NamedBuildTarget buildTargetGroup, string define)
    {
        string current = PlayerSettings.GetScriptingDefineSymbols(buildTargetGroup);
        if (!current.Contains(define))
            return;
        string result = current.Replace(define, String.Empty);
        PlayerSettings.SetScriptingDefineSymbols(buildTargetGroup, result);
    }


    private static string[] GetSceneList()
    {
        List<string> result = new List<string>();
        int count = EditorBuildSettings.scenes.Length;
        for (int i = 0; i < count; i++)
        {
            string path = EditorBuildSettings.scenes[i].path;
            if (EditorBuildSettings.scenes[i].enabled)
                result.Add(path);
        }
        return result.ToArray();
    }


}
#endif