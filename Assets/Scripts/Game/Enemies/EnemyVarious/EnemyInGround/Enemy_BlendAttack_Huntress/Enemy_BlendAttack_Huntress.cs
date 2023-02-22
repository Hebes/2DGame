/****************************************************
    文件：Enemy_BlendAttack_Huntress.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/30 14:10:28
	功能：混合的攻击类型的敌人
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_BlendAttack_Huntress : EnemyBase<EnemyHuntressData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE, new EnemyHuntressMoveState(Fsm, Consts.CHARACTER_ANM_MOVE, this, _data));
        Fsm.AddState(E_CharacterState.IDLE, new EnemyHuntressIdleState(Fsm, Consts.CHARACTER_ANM_IDLE, this, _data));
        Fsm.AddState(E_CharacterState.DETECTED, new EnemyHuntressDetectedState(Fsm, Consts.CHARACTER_ANM_IDLE, this, _data));
        Fsm.AddState(E_CharacterState.LOOKFOR, new EnemyHuntressLookForPlayerState(Fsm, Consts.CHARACTER_ANM_LOOKFOR, this, _data));
        Fsm.AddState(E_CharacterState.MELEEATTACK, new EnemyHuntressMeleeAttackState(Fsm, Consts.CHARACTER_ANM_MELEEATTACK, this, _data));
        Fsm.AddState(E_CharacterState.RANGEATTACK, new EnemyHuntressRangeAttackState(Fsm, Consts.CHARACTER_ANM_MELEEATTACK, this, _data));
        Fsm.AddState(E_CharacterState.BACK, new EnemyHuntressBackState(Fsm, Consts.CHARACTER_ANM_BACK, this, _data));
        Fsm.AddState(E_CharacterState.INAIR, new EnemyHuntressInAirState(Fsm, Consts.CHARACTER_ANM_INAIR, this, _data));
        Fsm.AddState(E_CharacterState.MELEEATTACKTWO, new EnemyHuntressMeleeAttackTwoState(Fsm, Consts.CHARACTER_ANM_MELEEATTACK, this, _data));
        Fsm.AddState(E_CharacterState.DODGE, new EnemyHuntressDodgeState(Fsm, Consts.CHARACTER_ANM_INAIR, this, _data));
        Fsm.AddState(E_CharacterState.HIT, new EnemyHuntressHitState(Fsm, Consts.CHARACTER_ANM_HIT, this, _data));
        Fsm.AddState(E_CharacterState.DEAD, new EnemyHuntressDeadState(Fsm, Consts.CHARACTER_ANM_DEAD, this, _data));
        Fsm.EnterFirstState();
    }
}