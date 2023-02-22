using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResMgr : NormalSingleTon<ResMgr>,ILoader
{
    private ILoader _loader;
    public  ResMgr()
    {
        _loader = new ResLoader();
    }
    public T LoadRes<T>(string path) where T : UnityEngine.Object
        => _loader.LoadRes<T>(path);
    public T[] LoadAllRes<T>(string path) where T : UnityEngine.Object
        => _loader.LoadAllRes<T>(path);
    public void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : UnityEngine.Object
        => _loader.LoadAllResAysn(path, callback);
    public GameObject LoadPrefab(string path)
        => _loader.LoadPrefab(path);
    public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
        => _loader.LoadPrefabAndInstantiate(path, parent);
    public void LoadResAysn<T>(string path, Action<T> callback) where T : UnityEngine.Object
        => _loader.LoadResAysn(path, callback);
}
