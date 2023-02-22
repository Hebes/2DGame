using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RangeAttack_FireWorm : EnemyBase<EnemyFireWormData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE, new EnemyFireWormMoveState(Fsm, Consts.CHARACTER_ANM_MOVE, this, _data));
        Fsm.AddState(E_CharacterState.IDLE, new EnemyFireWormIdleState(Fsm, Consts.CHARACTER_ANM_IDLE, this, _data));
        Fsm.AddState(E_CharacterState.DETECTED, new EnemyFireWormDetectedState(Fsm, Consts.CHARACTER_ANM_DETECTED, this, _data));
        Fsm.AddState(E_CharacterState.RANGEATTACK, new EnemyFireWormRangeAttackState(Fsm, Consts.CHARACTER_ANIM_RANGEATTACK, this, _data));
        Fsm.AddState(E_CharacterState.INAIR, new EnemyFireWormInAirState(Fsm, Consts.CHARACTER_ANM_INAIR, this, _data));
        Fsm.AddState(E_CharacterState.HIT,new EnemyFireWormHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyFireWormDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.EnterFirstState();
    }
}
