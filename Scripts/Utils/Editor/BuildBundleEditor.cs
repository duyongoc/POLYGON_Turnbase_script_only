using System.Collections;
using System.Collections.Generic;
// using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public static class BuildEditor
{


    // [MenuItem("Build/Asset Clear Cache", false, 200)]
    // private static void EditorAssetClearCache()
    // {
    // }
    
    
    // [MenuItem("Build/Asset Bundle/Build Asset Bundle [All]", false, 200)]
    // private static void EditorBuildAssetBundleAll()
    // {
    //     EditorBuildAssetBundleWebGL();
    //     EditorBuildAssetBundleEditor();
    // }


    // [MenuItem("Build/Asset Bundle/Build Asset Bundle [Web]", false, 200)]
    // private static void EditorBuildAssetBundleWebGL()
    // {
    //     Debug.Log("BEGIN Build AssetBundle");
    //     var outputFolder = $"{Application.dataPath}/AssetBundle/Web";
    //     BuildAssetBundle(BuildTarget.WebGL, outputFolder, "_web");
    //     Debug.Log($"END Build AssetBundle with output folder {outputFolder}");
    // }


    // [MenuItem("Build/Asset Bundle/Build Asset Bundle [Editor]", false, 200)]
    // private static void EditorBuildAssetBundleEditor()
    // {
    //     Debug.Log("BEGIN Build AssetBundle");
    //     var outputFolder = $"{Application.dataPath}/AssetBundle/Editor";
    //     BuildAssetBundle(BuildTarget.StandaloneWindows, outputFolder, "_editor");
    //     Debug.Log($"END Build AssetBundle with output folder {outputFolder}");
    // }


    public static void BuildAssetBundle(BuildTarget buildTarget, string outputFolder, string extension)
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // build and remove unuse file
        BuildPipeline.BuildAssetBundles(outputFolder, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
        DeleteFileManifest(outputFolder);

        // add extension _editor or _web
        AddFileNameExtension(outputFolder, extension);
        AssetDatabase.Refresh();
    }


    private static void DeleteFileManifest(string outputPath)
    {
        var listFile = new DirectoryInfo(outputPath).GetFiles();
        foreach (var fileInfo in listFile)
        {
            if (fileInfo.Extension == ".manifest")
            {
                File.Delete(fileInfo.FullName);
                File.Delete(fileInfo.FullName + ".meta");
            }
        }

        var dirName = Path.GetFileName(outputPath);
        var fileName = Path.Combine(outputPath, dirName);
        File.Delete(fileName);
        File.Delete(fileName + ".meta");
    }


    private static void AddFileNameExtension(string outputPath, string extension)
    {
        // delete old files
        var listFileOld = new DirectoryInfo(outputPath).GetFiles();
        foreach (var fileInfo in listFileOld)
        {
            if (fileInfo.FullName.Contains(extension))
            {
                File.Delete(fileInfo.FullName);
            }
        }

        // rename new files
        var listFileNew = new DirectoryInfo(outputPath).GetFiles();
        foreach (var fileInfo in listFileNew)
        {
            if (fileInfo.Extension != ".meta")
            {
                Debug.Log("fileInfo " + fileInfo.FullName + " " + extension);
                fileInfo.Rename($"{fileInfo.FullName}{extension}");
            }
        }
    }




    // private static AssetBundleBuild[] BuildAssetBundleManifest(out DownloadManifest assetBundleManifest)
    // {
    //     var allAssetBundles = AssetDatabase.GetAllAssetBundleNames();
    //     foreach (var bundle in allAssetBundles)
    //     {
    //         Debug.Log($"hash of {bundle}");
    //     }
    // }

}
