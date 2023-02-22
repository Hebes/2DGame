using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManDashState : EnemyBaseDashState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    private string _audioClipName = "enemy_shieldMan_dash_back";
    public EnemyShieldManDashState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isPlayerInMeleeAttack, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _isDashOver || _isWallFront || !_isLedgeVerticalFront, E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
