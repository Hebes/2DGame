using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManDodgeState : EnemyBaseDodgeState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    private string _audioClipName = "enemy_shieldMan_jump";
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public EnemyShieldManDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.INAIR);
    }
}