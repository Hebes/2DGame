using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManShieldState : EnemyBaseShieldState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    private string _audioClipName = "enemy_shieldMan_dash_back";
    public EnemyShieldManShieldState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isOnGround, E_CharacterState.INAIR);
        AddTargetState(()=>_isShieldOver,E_CharacterState.MELEEATTACK);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
