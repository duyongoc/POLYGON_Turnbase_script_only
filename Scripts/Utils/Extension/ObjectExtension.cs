using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class ObjectExtension
{

    public static T UnBoxing<T>(this object obj, T param)
    {
        return (T)obj;
    }

    public static List<T> GetPage<T>(this List<T> list, int pageNumber, int pageSize = 10)
    {
        return list.Skip(pageSize * pageNumber).Take(pageSize).ToList();
    }

    public static IEnumerable<T> AddIfNotExists<T>(this IEnumerable<T> list, T value)
    {
        if (!list.Contains(value))
        {
            return list.Append(value);
        }
        return list;
    }

    public static System.Threading.CancellationToken GetDefaultCancelledToken(this MonoBehaviour monoBehaviour)
    {
        return monoBehaviour.GetCancellationTokenOnDestroy();
    }

}
