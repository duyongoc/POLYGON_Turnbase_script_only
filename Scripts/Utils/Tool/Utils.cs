using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{


    public static bool IsEditor()
    {
        return Application.isEditor;
    }


    public static bool IsAndroidOrIOS()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }


    public static bool IsAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }


    public static bool IsIOS()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }


    public static Vector3 UnscaleEventDelta(CanvasScaler scaler, Vector3 vec)
    {
        Vector2 referenceResolution = scaler.referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;
        float ratio = Mathf.Lerp(widthRatio, heightRatio, scaler.matchWidthOrHeight);

        return vec / ratio;
    }



    public static float GetPhysicDamge(DataActorStat attacker, DataActorStat victim, float percentDamage = 0)
    {
        var damage = attacker.p_attack - victim.p_defense;
        damage = (damage * percentDamage) / 100;

        // apply default damage if damage is nagative
        if (damage <= 0)
            damage = CONST.DEFAULT_DAMAGE;

        // print($"damage: {damage} | attacker.p_attack: {attacker.p_attack} | victim.p_defense: {victim.p_defense}");
        return damage;
    }


    public static float GetPhysicDamgeByPercent(DataActorStat attacker, DataActorStat victim, float percentDamage = 100)
    {
        var damage = attacker.p_attack - victim.p_defense;
        damage = (damage * percentDamage) / 100;

        // apply default damage if damage is nagative
        if (damage <= 0)
            damage = CONST.DEFAULT_DAMAGE;

        // print($"damage: {damage} | percentDamage: {percentDamage} | atk.p_attack: {attacker.p_attack} | vic.p_defense: {victim.p_defense}");
        return damage;
    }


    public static float GetMagicDamgeByPercent(DataActorStat attacker, DataActorStat victim, float percentDamage = 100)
    {
        var damage = attacker.m_attack - victim.m_defense;
        damage = (damage * percentDamage) / 100;

        // print($"damage: {damage} | percentDamage: {percentDamage} | atk.m_attack: {attacker.m_attack} | vic.m_defense: {victim.m_defense}");
        return damage;
    }



    public static Color GetColorByStringHex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out var c);
        return c;
    }


    public static void DOFade(CanvasGroup group, float value, float time, Action callback = null)
    {
        group.DOFade(value, time).OnComplete(() => callback?.Invoke());
    }


    public static void Delay(float delayTime, System.Action callback)
    {
        DOVirtual.DelayedCall(delayTime, () => { callback.CheckInvoke(); });
    }


    public static string FormatTimeFromSecond(float time)
    {
        int minute = (int)time / 60;
        int second = (int)time - minute * 60;

        // return string time with format : mm:ss
        return $"{minute.ToString("##.")}:{second.ToString("##.")}";
    }


    public static string Format_DayMMSS_FromTotalSeconds(double seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        return time.ToString(@"mm\:ss");
    }


    public static string Format_DayHHMMSS_FromTotalSeconds(double seconds)
    {
        // TimeSpan timeIn24h = TimeSpan.FromSeconds(seconds);
        TimeSpan totalTime = TimeSpan.FromMilliseconds(seconds * 1000);
        return string.Format("{0:D2} days {1:D2} :{2:D2}:{3:D2}", (int)totalTime.TotalHours / 24, (int)totalTime.TotalHours % 24, totalTime.Minutes, totalTime.Seconds);
    }


    public static bool CheckPointerOverGameObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;

        return false;
    }


    public static double GetSecondFromTimeStamp(double timeStamp)
    {
        var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        var totalSeconds = timeStamp - currentTime;
        return totalSeconds;
    }


    public static bool IsValidDataActorKey(DataActor actor)
    {
        if (actor == null)
            return false;

        if (string.IsNullOrEmpty(actor.key))
            return false;

        return true;
    }


    public static string GetDataActorKey(DataActor actor)
    {
        if (actor == null)
            return null;

        if (string.IsNullOrEmpty(actor.key))
            return null;

        return actor.key;
    }



    public static void ShowWindownExplorer(string buildPath)
    {
        print("ShowWindownExplorer: " + buildPath);
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = string.Format("/C start {0}", buildPath);
        process.StartInfo = startInfo;
        process.Start();
    }



#if !UNITY_EDITOR && UNITY_ANDROID
    public static int GetVersionCode()
    {
        // string packageName = context.Call<string>("getPackageName");
        AndroidJavaClass contextCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", Application.identifier, 0);
        return packageInfo.Get<int>("versionCode");
    }
#endif




    //---------------------------------------------
    //------------- Assert ------------------------


    // Thown an exception if condition = false
    public static void CheckNull<T>(T param)
    {
        if (param == null) UnityEngine.Debug.Log($"{param.GetType()} is null. Please re-cheack"); ;
    }

    public static bool Check<T>(T param)
    {
        return param != null;
    }

    [Conditional("ASSERT")]
    public static void Assert<T>(T param)
    {
        if (param == null) throw new UnityException();
    }

    [Conditional("ASSERT")]
    public static void Assert(bool condition)
    {
        if (!condition) throw new UnityException();
    }

    /// Thown an exception if condition = false, show message on console's log
    [Conditional("ASSERT")]
    public static void Assert(bool condition, string message)
    {
        if (!condition) throw new UnityException(message);
    }

    /// Thown an exception if condition = false, show message on console's log
    [Conditional("ASSERT")]
    public static void Assert(bool condition, string format, params object[] args)
    {
        if (!condition) throw new UnityException(string.Format(format, args));
    }


    public static void LOG(string content)
    {
        UnityEngine.Debug.Log("[UNITY] " + content);
    }

}
