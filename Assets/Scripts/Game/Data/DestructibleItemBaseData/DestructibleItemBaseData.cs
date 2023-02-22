using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newDestructibleItemData", menuName = "数据/可破坏地形物体"),InlineEditor]
public class DestructibleItemBaseData : ScriptableObject
{
    [PreviewField(100, ObjectFieldAlignment.Left), ShowInInspector, Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [TabGroup("可破坏性物体的基本属性"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float shakeForce=0.2f;//产生抖动的力
    [TabGroup("可破坏性物体的基本属性"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float shakeTime=1f;//产生抖动的时间
    [TabGroup("可破坏性物体的基本属性"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public int vibrato=20;//振动次数
    [TabGroup("可破坏性物体的基本属性"), LabelWidth(150), GUIColor(0f, 1f, 1f),Range(0,180)]
    public float randomness=90;//抖动随机值
}
