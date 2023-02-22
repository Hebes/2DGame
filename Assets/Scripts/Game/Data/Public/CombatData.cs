using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[InlineEditor]
[CreateAssetMenu(fileName = "newCombatData", menuName = "数据/英雄或者敌人/CombatData")]
public class CombatData : ScriptableObject
{
    [PreviewField(100, ObjectFieldAlignment.Left), ShowInInspector, Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [TableList(ShowIndexLabels = true,AlwaysExpanded =true)]
    [SerializeField]
    public AttackDetails[] attackDetails;
    private Dictionary<E_Attack, List<AttackDetails>> _attackDetailsDic = new Dictionary<E_Attack, List<AttackDetails>>();
    //将攻击数据转化为字典类型的数据
    public void Init()
    {
        _attackDetailsDic.Clear();//防止重新加载场景的时候 之前场景的数据没有被清空
        foreach (var detail in attackDetails)
        {
            var type = detail.attackType;
            if (!_attackDetailsDic.ContainsKey(type))
                _attackDetailsDic[type] = new List<AttackDetails>();
            _attackDetailsDic[type].Add(detail);
        }
        if (_attackDetailsDic.Count == 0)
            Debug.LogError($"当前Combat数据中未包含任何内容");
    }
    public AttackDetails GetAttackDeails(E_Attack type, int index)
    {
        if (_attackDetailsDic.ContainsKey(type) && _attackDetailsDic[type].Count > index)
            return _attackDetailsDic[type][index];
        Debug.LogError($"你想要的攻击类型{type}不存在");
        Debug.LogError($"最大索引为:{_attackDetailsDic[type].Count - 1},索引错误{index}");
        return default(AttackDetails);
    }
    public int GetMaxCombatIndex(E_Attack type) => _attackDetailsDic[type].Count;
    public HashSet<E_Attack> GetCombatDataAllType()
    {
        var attackTypeHash = new HashSet<E_Attack>();
        foreach (var type in _attackDetailsDic.Keys)
            attackTypeHash.Add(type);
        return attackTypeHash;
    }
    public E_Attack GetFirstAttackType()
        => attackDetails[0].attackType;
}
