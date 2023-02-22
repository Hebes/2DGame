using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyShieldManChargeState : EnemyBaseChargeState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    private string _audioClipName = "enemy_shieldMan_charge";
    public EnemyShieldManChargeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => (_isInMeleeAttack || _bulletInMeleeAttackDis), E_CharacterState.MELEEATTACK);
        //如果发现主角跳起来了 如果有子弹在近战攻击范围内
        AddTargetState(() => _playerInAir, E_CharacterState.DODGE);
        AddTargetState(() => _isChargeOver && _isInMaxAgro, E_CharacterState.DETECTED);
        AddTargetState(() => _isChargeOver, E_CharacterState.LOOKFOR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void Check()
    {
        base.Check();
        _playerInAir = ColliderCheck.PlayerInAir;
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
}
