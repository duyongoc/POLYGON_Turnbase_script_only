using System;
using UnityEngine;


// this event id using for global event 
public enum EventID
{
    None = 0,

    // lobby event
    ShowCharacter,

    // battle event
    Game_Finished,
    Game_Victory,
    Game_Defeat,

}



public static class EventExtension
{

    public static void CheckInvoke(this Action action)
    {
        action?.Invoke();
    }

    public static void CheckInvoke<T>(this Action<T> action, T param)
    {
        action?.Invoke(param);
    }



    public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance?.RegisterListener(eventID, callback);
    }

    public static void RemoveListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance?.RemoveListener(eventID, callback);
    }

    public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
    {
        EventDispatcher.Instance?.PostEvent(eventID, param);
    }

    public static void PostEvent(this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.Instance?.PostEvent(eventID, null);
    }

}
