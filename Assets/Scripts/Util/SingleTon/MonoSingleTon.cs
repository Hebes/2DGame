using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            GameObject.DontDestroyOnLoad(_instance.gameObject);
        }
        else
            GameObject.Destroy(_instance.gameObject);

    }
}
