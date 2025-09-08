using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "JsonManagerSO", menuName = "SO/Json/JsonManagerSO")]
public class JsonManagerSO : ScriptableObject
{
    string path;

    protected virtual void OnEnable()
    {
        path = Application.persistentDataPath + "/";
    }

    public void SaveJson<T>(T obj, PrefsKeyType keyType)
    {
        var json = JsonUtility.ToJson(obj);
        File.WriteAllText(path + keyType.ToString(), json);
    }

    public T LoadJson<T>(PrefsKeyType keyType, T defaultValue = default)
    {
        var defaultJson = JsonUtility.ToJson(defaultValue);
        string json;
        try
        {
            json = File.ReadAllText(path + keyType.ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return defaultValue;
            throw;
        }
        var fromJson = JsonUtility.FromJson<T>(json);
        return fromJson ?? defaultValue;
    }
}

public enum PrefsKeyType
{
    Volume,
    SaveFile
}