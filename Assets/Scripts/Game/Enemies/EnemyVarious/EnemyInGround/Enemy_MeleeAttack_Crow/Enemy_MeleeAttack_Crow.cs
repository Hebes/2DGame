using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack_Crow : EnemyBase<EnemyCrowData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE, new EnemyCrowMoveState(Fsm, Consts.CHARACTER_ANM_MOVE, this, _data));
        Fsm.AddState(E_CharacterState.IDLE, new EnemyCrowIdleState(Fsm, Consts.CHARACTER_ANM_IDLE, this, _data));
        Fsm.AddState(E_CharacterState.DETECTED,new EnemyCrowDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.LOOKFOR,new EnemyCrowLookForPlayerState(Fsm,Consts.CHARACTER_ANM_LOOKFOR,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE,new EnemyCrowChargeState(Fsm,Consts.CHARACTER_ANM_CHARGE,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK,new EnemyCrowMeleeAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.DODGE,new EnemyCrowDodgeState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.LAND,new EnemyCrowLandState(Fsm,Consts.CHARACTER_ANM_LAND,this,_data));
        Fsm.AddState(E_CharacterState.BACK,new EnemyCrowBackState(Fsm,Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemyCrowHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyCrowDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemyCrowAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.EnterFirstState();
    }
}
