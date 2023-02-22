using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSingleTon<T> where T:class,new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var t = new T();
                if (t is MonoBehaviour)
                {
                    Debug.LogError($"Mono类请使用MonoSingleTon");
                    return null;
                }
                _instance = t;
            }
            return _instance;
        }
    }

}
