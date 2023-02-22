/****************************************************
    文件：ScreenPosUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 13:49:2
	功能：游戏屏幕坐标转世界坐标工具
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ScreenPosUtil
{
    public static Vector3 GetScreenMaxPos()
    {
        var pos = Camera.main.ViewportToWorldPoint(Vector2.one);
        pos.z = 0;
        return pos;
    }
    public static Vector3 GetScreenMinPos()
    {
        var pos = Camera.main.ViewportToWorldPoint(Vector2.zero);
        pos.z = 0;
        return pos;
    }
    public static Vector3 GetScreenCenterPos()
    {
        var maxPos = GetScreenMaxPos();
        var minPos = GetScreenMinPos();
        return (minPos+maxPos)/2;
    }
}