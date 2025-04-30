using System;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(WrapJson(json));
        return wrapper.Items;
    }

    private static string WrapJson(string rawJson)
    {
        return "{\"Items\":" + rawJson + "}";
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}