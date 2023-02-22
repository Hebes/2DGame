using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public struct AttackDetails
{
    //攻击敌人的时候的属性
    [FoldoutGroup("基本组")]
    [EnumToggleButtons, HideLabel,OnValueChanged("OnRangeAttackType")]
    public E_Attack attackType;
    [BoxGroup("基本组/攻击基本属性"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f), Space(40), HideIf("@attackType==E_Attack.NONE")]
    public float movemenVelocity;//攻击时移动的速度
    [BoxGroup("基本组/攻击基本属性"), LabelWidth(120), GUIColor(0.1f, 1f, 0.1f), Space(40)]
    public float damageAmount;//攻击造成的伤害值 


    [BoxGroup("基本组/攻击基本预制体"), LabelWidth(100),PreviewField(100),GUIColor(0.1f,1,0.1f)]
    public GameObject effect;//攻击时的特效
    [BoxGroup("基本组/攻击基本预制体"), LabelWidth(170), PreviewField(100), GUIColor(0.1f, 1, 0.1f), HideIf("@attackType!=E_Attack.RANGE&&attackType!=E_Attack.RANGE_AIR")]
    public GameObject rangeAttackBullet;//远程攻击时需要产生的预制体






    [BoxGroup("基本组/特效属性相关"), GUIColor(0.1f, 1f, 0.1f),LabelWidth(200),Space(20)]
    public float bulletTime;//子弹时间
    [BoxGroup("基本组/特效属性相关"), GUIColor(0f, 1f, 0.8f), LabelWidth(200), Space(20)]
    public CameraShakeArgs cameraShakeArgs;//相机振动参数
    private void OnRangeAttackType(E_Attack attackType)
    {
        if(attackType == E_Attack.RANGE || attackType == E_Attack.RANGE_AIR)
        damageAmount = 0;
    }
}
