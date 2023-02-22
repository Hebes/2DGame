using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class ResLoader : ILoader
{
    public T LoadRes<T>(string path) where T : Object
    {
        var res=Resources.Load<T>(path);
        if (res == null)
        {
            Debug.LogError($"加载资源失败:失败路径为:{path}");
            return null;
        }
            return res;
    }
    public T[] LoadAllRes<T>(string path) where T : Object
    {
        var res = Resources.LoadAll<T>(path);
        if(res==null ||res.Length==0)
        {
            Debug.LogError($"加载资源失败:失败路径为:{path}");
            return null;
        }
        return res;
    }
    public GameObject LoadPrefab(string path)
        => LoadRes<GameObject>(path);
    public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
        => Object.Instantiate(LoadRes<GameObject>(path), parent);
    //异步资源加载的方法
    public void LoadResAysn<T>(string path, Action<T> callback) where T : Object
        => CoroutineMgr.Instance.ExcuteOne(RealLoadResAsyn(path, callback));
    private IEnumerator  RealLoadResAsyn<T>(string path, Action<T> callback) where T : Object
    {
        var request = Resources.LoadAsync<T>(path);
        yield return request;
        callback?.Invoke(request.asset as T);
    }
    public void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : Object
        => CoroutineMgr.Instance.ExcuteOne(RealLoadAllResAsyn(path, callback));
    private IEnumerator RealLoadAllResAsyn<T>(string path, Action<T[]> callback) where T : Object
    {
        var request = Resources.LoadAsync<T>(path);
        yield return request;
        callback?.Invoke(request.asset as T[]);
    }
}
