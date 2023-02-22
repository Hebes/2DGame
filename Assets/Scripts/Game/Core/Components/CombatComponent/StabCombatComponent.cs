/****************************************************
    文件：StabCombatComponent.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/6 18:46:29
	功能：地刺攻击脚本
*****************************************************/
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class StabCombatComponent : ComponentBase, ICombat
{
    [BoxGroup("战斗组件设置"), SerializeField, EnumToggleButtons]
    private E_Group[] _hostileGroup;
    [BoxGroup("战斗组件设置"), SerializeField,LabelWidth(150f)]
    private float _damageValue;//造成的伤害值


    private bool _isFirst = true;
    private HashSet<E_Group> _hostileGroupHash;
    public override void Init()
    {
        base.Init();
        InitComponent();
    }
    private void InitComponent()
    {
        _hostileGroupHash = new HashSet<E_Group>();
        foreach (var group in _hostileGroup)
            _hostileGroupHash.Add(group);
    }
    public HashSet<E_Group> GetHostileGroup()
        => _hostileGroupHash;
    public int GetOwerFacingDir()
        => 0;
    public void SetHostileGroupHash(HashSet<E_Group> hostileGroup)
       => _hostileGroupHash = hostileGroup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var behavior = collision.GetComponent<IBehavior>();
        if (behavior != null && GetHostileGroup().Contains(behavior.GetGroup()))
        {
            if (_isFirst&&collision.CompareTag(Tags.Player))
            {
                SubEventMgr.ExecuteEvent(E_EventName.STAB_CHANGESPROTE);
                _isFirst = false;
            }
            behavior.Damage(_owerTf.transform.position, _damageValue, E_Attack.TRAP);
        }
    }
}