using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonEvent<T> : MonoBehaviour where T : Component
{


    #region FILEDS
    ///
    /// Instance
    ///
    private static T instance;
    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    #endregion


    ///
    /// Gets instance
    ///
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
            }

            return instance;
        }
    }


    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }


    /// <summary>
    /// Use this for initialization
    /// </summary>
    public virtual void Awake()
    {
        // print($"instance: {instance} | this: {this} | {instance != this}");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransfrom = child.transform;
        while (parentTransfrom.parent != null)
        {
            parentTransfrom = parentTransfrom.parent;
        }

        // Debug.Log(parentTransfrom.name);
        DontDestroyOnLoad(parentTransfrom.gameObject);
    }

}
