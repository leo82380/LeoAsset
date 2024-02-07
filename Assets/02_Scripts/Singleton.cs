using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // 인스턴스가 없는 경우
            if (instance == null)
            {
                // 씬에서 찾아서 인스턴스화
                instance = FindObjectOfType<T>();
                // 인스턴스가 없는 경우
                if (instance == null)
                {
                    // 새로운 게임오브젝트를 만들어서 인스턴스화
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // 인스턴스가 없는 경우
        if (instance == null)
        {
            // 인스턴스를 자신으로 설정
            instance = this as T;
            // 씬이 변경되어도 삭제하지 않음
            DontDestroyOnLoad(gameObject);
        }
        // 인스턴스가 있는 경우
        else
        {
            // 자신을 삭제
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        // 인스턴스가 자신인 경우
        if (instance == this)
        {
            // 인스턴스를 null로 설정
            instance = null;
        }
    }
}