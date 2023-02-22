/****************************************************
    文件：EnemyHuntreeData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/29 21:13:4
	功能：女猎人数据
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newEnemyHuntressData", menuName = "数据/敌人/EnemyHuntressData")]
public class EnemyHuntressData:EnemyBaseData
{
    [FoldoutGroup("@MeleeAttackStateProperty/MeleeAttackTwo"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public int meleeAttackTwoCombatIndex=3;
}