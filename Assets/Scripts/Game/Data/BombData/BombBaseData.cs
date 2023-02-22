/****************************************************
    文件：BombBaseData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/5 11:45:58
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[InlineEditor]
public abstract class BombBaseData : ScriptableObject
{
    [PreviewField(100, ObjectFieldAlignment.Left), Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [TabGroup("炸弹的基本属性"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float startForce = 20;//初始力
    [TabGroup("炸弹的基本属性"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float startTorqueForece=5;//初始扭矩力大小
    [TabGroup("炸弹的基本属性"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float damageValue = 1;//造成的伤害
    [TabGroup("炸弹的基本属性"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float recycleTime = 1.5f;//回收时间
}