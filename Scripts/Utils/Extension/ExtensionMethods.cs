using UnityEngine;


public static class ExtensionMethods
{

    public static GameObject SpawnToGarbage(this GameObject prefab, Vector3 position, Quaternion quaternion)
    {
        return PoolGarbage.Instance.Spawn(prefab, position, quaternion);
    }

    public static GameObject SpawnToGarbage(this GameObject prefab, Vector3 position, Quaternion quaternion, Transform transform)
    {
        return PoolGarbage.Instance.Spawn(prefab, position, quaternion, transform);
    }

    public static GameObject SpawnToGarbage(this GameObject prefab)
    {
        return PoolGarbage.Instance.Spawn(prefab);
    }

    public static GameObject Spawn(this GameObject prefab, Transform parent)
    {
        return PoolGarbage.Instance.Spawn(prefab, parent.transform);
    }

    public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 scale, Vector3 position, Vector3 rotation)
    {
        var slash = PoolGarbage.Instance.Spawn(prefab, parent.transform);
        slash.transform.localScale = scale;
        slash.transform.localPosition = position;
        slash.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        return slash;
    }

    public static GameObject Set(this GameObject prefab, Vector3 scale, Vector3 position, Vector3 rotation)
    {
        prefab.transform.localScale = scale;
        prefab.transform.localPosition = position;
        prefab.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        return prefab;
    }

    public static Vector3 GarbagePosition()
    {
        return PoolGarbage.Instance.GetPosition();
    }

}