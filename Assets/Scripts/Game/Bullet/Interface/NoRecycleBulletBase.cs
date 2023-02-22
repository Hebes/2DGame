/****************************************************
    文件：NoRecycleBullet.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/7 14:43:35
	功能: 不回收子弹基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoRecycleBulletBase<T> : BulletBase<T> where T : BulletBaseData
{
    protected override void StopBullet()
    {
        _isOver = true;
        _move.ResetGravityScale();//还原重力的大小的影响
        _move.SetXVelocity(0);
        AddTorque(_data.torqueValue);
        Core.Get<BulletCombatComponent>().SetActive(false);
        DestroyInConstTime();
    }
}