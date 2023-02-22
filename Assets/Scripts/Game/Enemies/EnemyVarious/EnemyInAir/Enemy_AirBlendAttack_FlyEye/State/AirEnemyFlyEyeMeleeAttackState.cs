using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeMeleeAttackState : AirEnemyBaseMeleeAttackState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    private string _auidoClioName = "enemy_flyeye_meleeAttack";
    public AirEnemyFlyEyeMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        //如果动画播放完毕
        AddTargetState(()=>_isAbilityDown,E_CharacterState.BACK);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
