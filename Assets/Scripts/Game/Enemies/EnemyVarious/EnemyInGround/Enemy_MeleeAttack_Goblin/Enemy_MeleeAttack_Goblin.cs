using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack_Goblin : EnemyBase<EnemyGoblineData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.IDLE,new EnemyGoblinIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.MOVE,new EnemyGoblinMoveState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemyGoblinHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyGoblinDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.DODGE,new EnemyGoblineDodgeState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemyGoblinInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.SCARED,new EnemyGoblinScaredState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.EnterFirstState();
    }
}
