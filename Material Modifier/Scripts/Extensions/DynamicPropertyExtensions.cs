using System;
using UnityEngine;

public static class DynamicPropertyExtensions
{
    public static void ParseBool(this DynamicProperties properties, string propName, out bool result, bool defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseBool(propName, ref result);
    }

    public static void ParseColorHex(this DynamicProperties properties, string propName, out Color result, Color defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseColorHex(propName, ref result);
    }

    public static void ParseEnum<T>(this DynamicProperties properties, string propName, out T result, T defaultValue) where T : struct, IConvertible
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseEnum(propName, ref result);
    }

    public static void ParseFloat(this DynamicProperties properties, string propName, out float result, float defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseFloat(propName, ref result);
    }

    public static void ParseInt(this DynamicProperties properties, string propName, out int result, int defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseInt(propName, ref result);
    }

    public static void ParseString(this DynamicProperties properties, string propName, out string result, string defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseString(propName, ref result);
    }

    public static void ParseVec(this DynamicProperties properties, string propName, out Vector3 result, Vector3 defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseVec(propName, ref result);
    }

    public static void ParseVec(this DynamicProperties properties, string propName, out Vector3i result, Vector3i defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseVec(propName, ref result);
    }

    public static void ParseVec(this DynamicProperties properties, string propName, out Vector2 result, Vector2 defaultValue)
    {
        result = default;
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        properties.ParseVec(propName, ref result);
    }

    public static void ParseVec(this DynamicProperties properties, string propName, out Vector2i result, Vector2i defaultValue)
    {
        if (!properties.Values.ContainsKey(propName))
        {
            result = defaultValue;
            return;
        }
        result = StringParsers.ParseVector2i(properties.GetString(propName));
    }
}