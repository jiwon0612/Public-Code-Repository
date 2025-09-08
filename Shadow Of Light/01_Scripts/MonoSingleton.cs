using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    private static bool IsDestroyed = false;

    public static T Instance
    {
        get
        {
            if (IsDestroyed)
            {
                instance = null;
            }
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} ½Ì±ÛÅæ ¸øÃ£À½");
                }
                else
                {
                    IsDestroyed = false;
                }
            }
            return instance;
        }
    }

    private void OnDisable()
    {
        IsDestroyed = true;
    }
}
