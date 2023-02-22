using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBossMinotaurData", menuName = "数据/敌人/BossMinotaurData")]
public class BossMinotaurData : EnemyBaseData
{
    protected const string RangeAttackOneStateProperty = "RangeAttackOne状态数据相关";

    [FoldoutGroup("@InAirStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public CameraShakeArgs inAirOverShakeArgs;


    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public CameraShakeArgs rangeAttackShakeArgs;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public int rangeAttackOneBulletNum = 10;//每次生成的子弹的数量
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float rangeAttackOneOffselAngel = 5f;//每次生成的子弹偏移角度

    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public int rangeAttackOneCombatIndex = 1;

    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public int rangeAttackTwoCombatIndex = 2;



    [FoldoutGroup("@MeleeAttackStateProperty/MeleeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float meleeAttackTwoVelocity = 20;//此时的速度
    [FoldoutGroup("@MeleeAttackStateProperty/MeleeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public int meleeAttackTwoCombatIndex = 3;//该攻击对应的攻击索引
    [FoldoutGroup("@MeleeAttackStateProperty/MeleeAttackTwo"), LabelWidth(300), GUIColor(0.1f, 1f, 0.1f),Range(0,100)]
    public int meleeAttackTwoToAttackOneProbability = 60;
    [FoldoutGroup("@MeleeAttackStateProperty/MeleeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public CameraShakeArgs meleeAttackTwoShakeArgs;


    [FoldoutGroup("@HitStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 1f), Range(0, 100)]
    public int hitToReadyProbability=50;
}

