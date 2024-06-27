using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isDestroyed = false;

    public static T Instance
    {
        get
        {
            if (isDestroyed)
            {
                return null;
            }

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError($"{typeof(T)}");
                }
            }

            return instance;
        }
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}