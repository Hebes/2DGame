using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class AirEnemyBaseData : EnemyBaseData
{
    protected const string FlyIdleStateProperty = "飞行怪物Idle状态数据相关";
    protected const string FlyChargeStateProperty = "飞行怪物Charge状态数据相关";
    protected const string FlyWaitStateProperty = "飞行怪物Wait状态数据相关";
    protected const string FlyBackStateProperty = "飞行怪物Back状态数据相关";
    protected const string FlyDeadStateProperty = "飞行怪物Dead状态数据相关";
    [FoldoutGroup("@FlyIdleStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f), Space(10)]
    public float changeIdleDirTime=0.5f;
    [FoldoutGroup("@FlyIdleStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float idleVelocity=2f;
    [FoldoutGroup("@FlyIdleStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float minChangeDirTime = 1f;//最小改变反向的时间



    [FoldoutGroup("@FlyChargeStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float maxTouchingWallTime=1f;//最大触墙时间
    [FoldoutGroup("@FlyChargeStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float chargeStopDis=1f;//离主角最近的停下来的距离




    [FoldoutGroup("@FlyWaitStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float maxWaitTime=0.5f;
    [FoldoutGroup("@FlyWaitStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float waitStateCoolTime=2f;




    [FoldoutGroup("@FlyBackStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public Vector2 backDirMult;//后退方向的混合参数
    [FoldoutGroup("@FlyDeadStateProperty"), LabelWidth(150), GUIColor(0f, 1f, 1f)]
    public float deadGravitySacle=6;
}
