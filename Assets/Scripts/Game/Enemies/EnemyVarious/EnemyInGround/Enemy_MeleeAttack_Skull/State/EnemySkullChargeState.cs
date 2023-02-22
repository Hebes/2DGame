using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullChargeState : EnemyBaseChargeState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    private string _audioClipName = "enemy_skull_charge";
    public EnemySkullChargeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>(_isInMeleeAttack || _bulletInMeleeAttackDis)/*&&fsm.GetState<EnemySkullMeleeAttackState>().CheckCanMeleeAttack()*/, E_CharacterState.MELEEATTACK);
        //如果发现主角跳起来了 如果有子弹在近战攻击范围内
        AddTargetState(()=>_playerInAir,E_CharacterState.DODGE);
        AddTargetState(() => _isChargeOver && _isInMaxAgro, E_CharacterState.DETECTED);
        AddTargetState(()=>_isChargeOver,E_CharacterState.MOVE);
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
