using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (!GameStateModel.Instance.CurScene.ToString().Contains("Level"))
            {
                Debug.LogError($"非游戏场景不能使用对象池");
                return null;
            }
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PoolManager>();
            if (_instance == null)
            {
                var go = new GameObject(typeof(PoolManager).Name);
                _instance = go.AddComponent<PoolManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            GameObject.Destroy(_instance.gameObject);
    }
    private void OnDestroy()
    {
        _instance = null;
    }
    private Dictionary<string, GameObjectPool> _poolsDic = new Dictionary<string, GameObjectPool>();
    //通过路径从对象池中得到物体
    public GameObject GetFromPool(string path)
    {
        if (!_poolsDic.ContainsKey(path))
        {
            var configData = PoolConfig.GetConfigData(path);
            var maxNum = configData.maxNum;
            var preLoadNum = configData.preLoadNum;
            var delayTime = configData.delayDestroyTime;
            var isAutoDestroy = configData.autoDestroy;
            var obj = ResMgr.Instance.LoadPrefab(path);
            _poolsDic[path] = new GameObjectPool(obj, transform, maxNum, preLoadNum, delayTime, isAutoDestroy);
        }
        return _poolsDic[path].GetFromPool();
    }
    public GameObject GetFromPool(GameObject obj)
    {
        var name = obj.name;
        if (!_poolsDic.ContainsKey(name))
        {
            var configData = PoolConfig.GetConfigData(name);
            var maxNum = configData.maxNum;
            var preLoadNum = configData.preLoadNum;
            var delayTime = configData.delayDestroyTime;
            var isAutoDestroy = configData.autoDestroy;
            _poolsDic[name] = new GameObjectPool(obj, transform, maxNum, preLoadNum, delayTime, isAutoDestroy);
        }
        return _poolsDic[name].GetFromPool();
    }
    public void PushPool(GameObject obj)
        => GetGameObjectPool(obj).PushPool(obj);
    //销毁该预制体对应的对象池
    public void DestroyPool(GameObject obj)
    {
        var pool = GetGameObjectPool(obj);
        var name = GetPrefabName(obj);
        _poolsDic.Remove(name);
    }
    private GameObjectPool GetGameObjectPool(GameObject obj)
    {
        var name = GetPrefabName(obj);
        _poolsDic.TryGetValue(name, out GameObjectPool pool);
        if (pool == null)
        {
            foreach (var item in _poolsDic.Keys)
                if (item.EndsWith(name))
                    pool = _poolsDic[item];
        }
        if (pool == null)
            Debug.LogError($"名字为:{name}的GameObject不存在对应缓存池");
        return pool;
    }
    private string GetPrefabName(GameObject obj)
        => obj.name.Replace("(Clone)", "");
}
