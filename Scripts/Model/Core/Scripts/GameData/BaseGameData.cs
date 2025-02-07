using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameData : ScriptableObject
{

    public string title;
    [Multiline]
    public string description;

    public virtual string Id { get { return name; } }


    public string GetDesciption()
    {
        var value = description.Replace("{0}%", "10%")
                                .Replace("{0}", "1");
        return value;
    }

}
