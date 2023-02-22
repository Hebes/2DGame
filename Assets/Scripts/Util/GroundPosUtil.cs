/****************************************************
    文件：GroundPosUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/7 15:50:4
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GroundPosUtil : NormalSingleTon<GroundPosUtil>
{
    private const string MapPath = "Map/";
    private const string PlatformPath = MapPath + "Background";
    private static Transform _playerTf;
    private static void Init()
    {
        if (_playerTf == null)
        {
            var playerGo = GameObject.FindGameObjectWithTag(Tags.Player);
            if(playerGo!=null)
            _playerTf =playerGo.transform;
        }
    }
    static GroundPosUtil()
    {
        Init();
    }
    //得到目标位置下面的平台位置
    public static Vector3 GetTargetGroundPos(Vector2 targetPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.down, 10, LayerMask.GetMask(Tags.Ground));
        if (hit.collider != null && hit.collider.CompareTag(Tags.Ground))
            return hit.point;
        return Vector3.zero;
    }
    //得到主角下方对应的格子坐标
    public static Vector3 GetPlayrerGroundPos()
    {
        Init();
        if (_playerTf != null)
            return GetTargetGroundPos(_playerTf.position);
        return Vector3.zero;
    }
}