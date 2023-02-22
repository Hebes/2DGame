using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
public interface ILoader//资源加载接口
{
    T LoadRes<T>(string path) where T : Object;//加载单个资源
    T[] LoadAllRes<T>(string path) where T : Object;//加载多个资源
    void LoadResAysn<T>(string path, Action<T> callback) where T : Object; 
    void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : Object;
    GameObject LoadPrefab(string path);
    GameObject LoadPrefabAndInstantiate(string path,Transform parent=null);
}
