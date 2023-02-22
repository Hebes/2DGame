using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameObjectPool
{
    private bool _isAutoDestroy;//是否自动销毁
    private int _maxNum;//最大缓存数量
    private GameObject _curGameObject;
    private float _destroyOffsetTime;//开始销毁多余预制体的时间
    private GameObject _selfPoolGo;//预制体本身
    private DateTime _lastSpawnTime;
    private bool _isDestroying;//是否正在销毁
    private List<GameObject> _activeList = new List<GameObject>();
    private List<GameObject> _inActiveList = new List<GameObject>();
    public GameObjectPool(GameObject gameObject, Transform parent, int maxNum, int preLoadNum, float destoryOffsetTime, bool isAutoDestroy)
    {
        _curGameObject = gameObject;
        _maxNum = maxNum;
        _destroyOffsetTime = destoryOffsetTime;
        _selfPoolGo = new GameObject(gameObject.name);
        _selfPoolGo.transform.SetParent(parent);
        _isAutoDestroy = isAutoDestroy;
        if (_isAutoDestroy)
            AutoDestroy();
        PreLoadPrefab(preLoadNum);
    }
    //从对象池中取出该物体
    public void PreLoadPrefab(int num)
    {
        if (num > _maxNum)
            num = _maxNum;
        for (int i = 0; i < num; i++)
        {
             var tempGo =GetNewGameObject();
            tempGo.SetActive(false);
            _inActiveList.Add(tempGo);
        }
    }
    public GameObject GetFromPool()
    {
        if (_curGameObject == null)
            Debug.LogError($"当前对象池所管理的物理为空");
        GameObject tempGo;
        if (_inActiveList.Count > 0)
        {
            tempGo = _inActiveList[0];
            _inActiveList.Remove(tempGo);
        }
        else
            tempGo = GetNewGameObject();
        tempGo.SetActive(true);
        _activeList.Add(tempGo);
        _lastSpawnTime = DateTime.Now;
        return tempGo;
    }
    //将该物体压入对象池
    public void PushPool(GameObject gameObject)
    {
        if (_activeList.Contains(gameObject))
        {
            _activeList.Remove(gameObject);
            _inActiveList.Add(gameObject);
            gameObject.transform.SetParent(_selfPoolGo.transform);
            gameObject.SetActive(false);
        }
        else
            Debug.LogError($"该物体:{gameObject.name}不属于该对象池{_selfPoolGo.name}");
    }
    //自动销毁
    private async void AutoDestroy()
    {
        while (_selfPoolGo != null)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            float spendTime = (DateTime.Now-_lastSpawnTime).Seconds;
            if (spendTime > _destroyOffsetTime && _inActiveList.Count > _maxNum && !_isDestroying)
            {
                _isDestroying = true;
                StartDestroy();
            }
        }
    }
    private async void StartDestroy()
    {
        GameObject temp = null;
        while (_inActiveList.Count > _maxNum)
        {
            await Task.Delay(100);
            temp = _inActiveList[0];
            _inActiveList.Remove(temp);
            GameObject.Destroy(temp);
        }
        _isDestroying = false;
    }
    //得到当前缓存池中缓存的数量
    public int GetCurPoolNum()
        => _inActiveList.Count;
    private GameObject GetNewGameObject()
    {
        var tempGo = GameObject.Instantiate(_curGameObject);
        tempGo.transform.SetParent(_selfPoolGo.transform);
        return tempGo;
    }
    //销毁当前对象池
    public void DestroyPool()
    {
        _curGameObject = null;
        _activeList.Clear();
        _inActiveList.Clear();
        GameObject.Destroy(_selfPoolGo);
    }
}
