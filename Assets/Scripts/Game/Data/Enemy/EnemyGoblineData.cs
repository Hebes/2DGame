using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newEnemyGoblineData", menuName = "数据/敌人/EnemyGoblineData")]
public class EnemyGoblineData : EnemyBaseData
{
    [FoldoutGroup("@MoveStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float maxMoveTime = 1.5f;//最大移动时间
    [FoldoutGroup("@MoveStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f),Range(0,100)]
    public int moveOverToIdleProbability=60;
}
