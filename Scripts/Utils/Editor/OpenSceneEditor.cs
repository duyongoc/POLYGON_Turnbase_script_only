using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class OpenSceneEditor : EditorWindow
{
    #region Tool
    private static string scenePath = "Assets/_Game/Scenes/{0}.unity";


    public enum ESceneName
    {
        Menu,
        Battle,
    }


    [MenuItem("OpenScene/RunGame &0", false, 100)]
    public static void StartGame()
    {
        if (EditorApplication.isPlaying)
            return;

        if (!string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Menu));
        EditorApplication.isPlaying = true;
    }


    [MenuItem("OpenScene/Menu &1", false, 900)]
    public static void OpenSceneMenu()
    {
        if (string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
        {
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }
        EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Menu), OpenSceneMode.Single);
    }


    [MenuItem("OpenScene/Battle &2", false, 901)]
    public static void OpenSceneLevel()
    {
        if (string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
        {
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }
        EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Battle), OpenSceneMode.Single);
    }



    // [MenuItem("OpenScene/Photo &3", false, 950)]
    // public static void OpenScenePhoto()
    // {
    //     if (string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
    //     {
    //         EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    //     }
    //     EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Photo), OpenSceneMode.Single);
    // }


    // [MenuItem("OpenScene/Map 2 &4", false, 951)]
    // public static void OpenSceneMap2()
    // {
    //     if (string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
    //     {
    //         EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    //     }
    //     EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Map_2), OpenSceneMode.Single);
    // }
   

    // [MenuItem("OpenScene/Balance &4", false, 1000)]
    // public static void OpenBattleToolScene()
    // {
    //     if (string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
    //     {
    //         EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    //     }
    //     EditorSceneManager.OpenScene(string.Format(scenePath, ESceneName.Balance), OpenSceneMode.Single);
    // }


    //Todo: Add more open scene to this class
    #endregion
}