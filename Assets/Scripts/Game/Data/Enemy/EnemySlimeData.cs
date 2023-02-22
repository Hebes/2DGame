using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "newSlimeData", menuName = "数据/敌人/EnemySlimeData")]
public class EnemySlimeData : EnemyBaseData
{
    [FoldoutGroup("@MoveStateProperty")]
    public float maxMoveTime=3f;//最大移动时间
    [FoldoutGroup("SlimeCopySelf"),Required,PreviewField]
    public GameObject slimePrefab;
    [FoldoutGroup("SlimeCopySelf")]
    public Vector2 slimeCreateOffset;//复制偏移
    [FoldoutGroup("SlimeCopySelf"),Range(0,100)]
    public int slimeCopyProbability=25;
}
