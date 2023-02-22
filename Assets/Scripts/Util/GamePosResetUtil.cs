/****************************************************
    文件：GamePosResetUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/6 21:13:53
	功能： 主角位置重置工具
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
public class GamePosResetUtil : MonoDestroySingleTon<GamePosResetUtil>, ITaskInit
{
    private HashSet<Vector3> _resetPos;
    private Dictionary<int, Vector3> _archivePointDic;
    public void Init(Action callback)
    {
        AddResetPos();
        AddArchivePos();
        callback?.Invoke();
    }
    //添加重置位置
    private void AddResetPos()
    {
        _resetPos = new HashSet<Vector3>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tags.PlayerResetPos);
        foreach (var obj in objs)
            _resetPos.Add(obj.transform.position);
    }
    //添加存档点位置
    private void AddArchivePos()
    {
        _archivePointDic = new Dictionary<int, Vector3>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tags.ArchivePoint);
        foreach (var obj in objs)
        {
            var point = obj.GetComponent<ArchiviePoint>();
            var id = point.GetPlaceId();
            var pos = point.GetResetPos();
            _archivePointDic[id] = pos;
        }
    }


    public Vector3 GetResetPos(Vector3 playerPos)
    {
        if (_resetPos == null)
            Debug.LogError($"未对主角复活点进行初始化");
        else
        {
            Vector3 resetPos = Vector3.zero;
            float minDis = 0;
            float tempDis;
            Vector3 tempPos;
            foreach (var pos in _resetPos)
            {
                tempPos = pos;
                tempDis = Vector3.Distance(tempPos, playerPos);
                if (tempDis < minDis || minDis == 0)
                {
                    resetPos = pos;
                    minDis = tempDis;
                }
            }
            return resetPos;
        }
        return default;
    }
    public Vector3 GetArchivePos(int id)
    {
        if (_archivePointDic.TryGetValue(id, out Vector3 pos))
            return pos;
        Debug.LogError($"没有该id:{id}对应的存档点");
        return default;
    }
}