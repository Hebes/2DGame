/****************************************************
    文件：AirBossDemonData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 15:54:22
	功能：Demon敌人数据相关
*****************************************************/
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newAirBossDemenData", menuName = "数据/敌人/AirBossDemenData")]
public class AirBossDemonData : AirEnemyBaseData
{
    [FoldoutGroup("@MeleeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public float meleeAttackOneVelocity=20f;
    [FoldoutGroup("@MeleeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int meleeAttackOneCombatIndex = 1;




    [FoldoutGroup("@MeleeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public float meleeAttackTwoVelocity = 20f;
    [FoldoutGroup("@MeleeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int meleeAttackTwoCombatIndex = 2;




    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public float rangeAttackOneVelocity=20f;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public float rangeAttackOneStopDis = 1f;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackOne"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackOneCombatIndex=1;




    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackTwoBulletNum=10;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackTwoContinueNum=4;//最大连续进入该远程攻击次数
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackTwoCombatIndex = 2;



    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackThree"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackThreeBulletNum = 10;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackThree"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackThreeContinueNum = 4;//最大连续进入该远程攻击次数
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackThree"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackThreeCombatIndex = 3;



    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackFour"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackFourBulletNum = 8;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackFour"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackFourContinueNum = 2;
    [FoldoutGroup("@RangeAttackStateProperty/RangeAttackFour"), LabelWidth(200), GUIColor(0.1f, 1f, 1f)]
    public int rangeAttackFourCombatIndex = 4;





}