using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newAirEnemyFlyEyeData", menuName = "数据/敌人/AirEnemyFlyEyeData")]
public class AirEnemyFlyEyeData : AirEnemyBaseData
{
    [FoldoutGroup("$OtherSetting"), Range(0f, 10f), LabelWidth(200), GUIColor(0f, 1f, 1f)]
    public float changeAttackOffsetTime = 0.5f;//攻击间隔时间
}
