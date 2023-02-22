using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack_Skull : EnemyBase<EnemySkullData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE,new EnemySkullMoveState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.IDLE,new EnemySkullIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED,new EnemySkullDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE,new EnemySkullChargeState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.DODGE,new EnemySkullDodgeState(Fsm,Consts.CHARACTER_ANM_JUMP,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemySkullInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.LAND,new EnemySkullLandState(Fsm,Consts.CHARACTER_ANM_LAND,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK,new EnemySkullMeleeAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.MELEEAIRATTACK,new EnemySkullMeleeAirAttackState(Fsm,Consts.CHARACTER_ANM_AIRMELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemySkullHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemySkullDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.BACK,new EnemySkullBackState(Fsm,Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.AddState(E_CharacterState.DASH,new EnemySkullDashState(Fsm,Consts.CHARACTER_ANM_DASH,this,_data));
        Fsm.EnterFirstState();
    }
}
