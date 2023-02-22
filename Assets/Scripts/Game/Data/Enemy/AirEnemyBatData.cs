using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newAirEnemyBatData", menuName = "数据/敌人/AirEnemyBatData")]
public class AirEnemyBatData : AirEnemyBaseData
{
    [FoldoutGroup("@FlyIdleStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public int idleChangeXDirNum=4;//满足改变x方向的条件
    [FoldoutGroup("@FlyIdleStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public int idleStartXVelocity=4;//开始的时候x方向的速度
}
