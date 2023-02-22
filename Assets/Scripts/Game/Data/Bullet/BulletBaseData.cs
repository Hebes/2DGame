using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[InlineEditor]
public class BulletBaseData :ScriptableObject
{
    [PreviewField(100, ObjectFieldAlignment.Left), ShowInInspector, Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [TabGroup("子弹的基本属性"),LabelWidth(100),GUIColor(0.1f,1f,0.1f)]
    public float movementVelocity=20;
    [TabGroup("子弹的基本属性"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float damageValue=1;//造成的伤害
    [TabGroup("子弹的基本属性"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float maxFlyTime=3f;
    [TabGroup("子弹的基本属性"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float torqueValue = 20f;//旋转的扭矩力
    [TabGroup("子弹的基本属性"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float recycleTime = 1.5f;//回收时间
    [TabGroup("子弹的基本属性"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float hurtEffectShowTime=0.5f;
}
