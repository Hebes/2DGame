/****************************************************
    文件：BombCombatComponent.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/6 23:0:50
	功能：炸弹类型基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombCombatComponent : ComponentBase, ICombat
{
    protected Collider2D _collider;
    protected float _damageValue;
    public override void Init()
    {
        base.Init();
        _collider = GetComponent<Collider2D>();
    }
    protected HashSet<E_Group> _hostileGroupHash;
    public HashSet<E_Group> GetHostileGroup()
        => _hostileGroupHash;
    public void SetHostileGroupHash(HashSet<E_Group> hostileGroup)
        => _hostileGroupHash = hostileGroup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageValue == 0)
        {
            Debug.LogError($"炸弹战斗组件未初始化,其所有者名称为:{_owerTf.name}");
            return;
        }
        var behavior = collision.GetComponent<IBehavior>();
        if (behavior != null && GetHostileGroup().Contains(behavior.GetGroup()))
            behavior?.Damage(_owerTf.position, _damageValue);
    }
    public void SetActive(bool value)
        => gameObject.SetActive(value);
    public void SetDamage(float damageValue)
        => _damageValue = damageValue;
    public int GetOwerFacingDir()
        => _owerTf.localEulerAngles == Vector3.up * 180f ? -1 : 1;
}