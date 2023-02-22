using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AirBlendAttack_FlyEye : EnemyBase<AirEnemyFlyEyeData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.IDLE, new AirEnemyFlyEyeIdleState(Fsm, Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED, new AirEnemyFlyEyeDetectedState(Fsm, Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACK, new AirEnemyFlyEyeRangeAttackState(Fsm, Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK, new AirEnemyFlyEyeMeleeAttackState(Fsm, Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.DASH, new AirEnemyFlyEyeDashState(Fsm, Consts.CHARACTER_ANM_DASH,this,_data));
        Fsm.AddState(E_CharacterState.WAIT, new AirEnemyFlyEyeWaitState(Fsm, Consts.CHARACTER_ANM_WAIT,this,_data));
        Fsm.AddState(E_CharacterState.BACK, new AirEnemyFlyEyeBackState(Fsm, Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.AddState(E_CharacterState.HIT, new AirEnemyFlyEyeHitState(Fsm, Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD, new AirEnemyFlyEyeDeadState(Fsm, Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE, new AirEnemyFlyEyeChargeState(Fsm, Consts.CHARACTER_ANM_CHARGE,this,_data));
        Fsm.EnterFirstState();
    }
}
