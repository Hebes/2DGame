using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoDestroySingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<T>();
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
            _instance = this as T;
        else
            GameObject.Destroy(_instance.gameObject);
    }
    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}
