/****************************************************
    文件：Boss_MeleeAttack_Minotaur.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 12:12:25
	功能：Boss牛头人
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss_MeleeAttack_Minotaur : BossBase<BossMinotaurData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.INAIR,new BossMinotaurInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.IDLE,new BossMinotaurIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED,new BossMinotaurDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.READY,new BossMinotaurReadyState(Fsm,Consts.CHARACTER_ANM_READY,this,_data));
        Fsm.AddState(E_CharacterState.RIGIDITY,new BossMinotaurRigidityState(Fsm,Consts.CHARACTER_ANM_RIGIDITY,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new BossMinotaurHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new BossMinotaurDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK,new BossMinotaurMeleeAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACKTWO,new BossMinotaurMeleeAttackTwoState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACK,new BossMinotaurRangeAttackOneState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACKTWO,new BossMinotaurRangeAttackTwoState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.BACK,new BossMinotaurBackState(Fsm,Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.EnterFirstState();
    }
}