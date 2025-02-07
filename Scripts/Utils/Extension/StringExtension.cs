using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringValueAttribute : Attribute
{
    public string StringValue { get; protected set; }

    public StringValueAttribute(string value)
    {
        this.StringValue = value;
    }
}


public static class StringExtension
{

    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    public static bool ValidateEmail(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }


    public static T ExtensionTest<T>(this T t, Action cb) where T : Component
    {
        if (t == null)
        {
            return t;
        }
        return t;
    }


    public static String ConvertToString(this Enum value)
    {
        return Enum.GetName(value.GetType(), value);
    }


    public static String GetCharacterName(this String value)
    {
        var index = value.IndexOf('_');
        return value.Substring(0, index);
    }


    public static EnumType ConverToEnum<EnumType>(this String enumValue)
    {
        return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
    }


    public static string FormatToAddress(this String value, int count)
    {
        if (string.IsNullOrEmpty(value) || value.Length < count)
            return string.Empty;

        var firstPart = value.Substring(0, count);
        var lastPart = value.Substring(value.Length - count);
        return $"{firstPart}...{lastPart}";
    }


    public static string ToDisplayK(this String stringValue)
    {
        var index = 3;
        var value = stringValue;

        // return if value is null
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        // not format when the value less than 999
        if (value.Length <= 3)
        {
            return value;
        }

        // check if the value is float
        for (int i = 0; i < stringValue.Length; i++)
        {
            if (stringValue[i].Equals('.'))
            {
                Debug.Log($"value: {value} - i: {i}");
                // var indexDot = i + 1;
                value = value.Remove(i);
                Debug.Log($"=>>>>>>value: {value}");
            }
        }

        // check length to format
        if (value.Length < 5)
        {
            Debug.Log($"ToDisplayK value: {value} | return value: {value}");
            return stringValue;
        }

        var valueDisplay = value.Remove(value.Length - index);
        Debug.Log($"ToDisplayK value: {value} | valueDisplay: {valueDisplay}");
        return $"{valueDisplay}K";
    }

}
