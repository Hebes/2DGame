using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCrowMeleeAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    private string _audioClipName = "enemy_crow_meleeAttack";
    public EnemyCrowMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(CheckCanDodge, E_CharacterState.DODGE);
        AddTargetState(() => _isAbilityDown, E_CharacterState.BACK);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        //如果攻击的之后发现主角在身后的位置
        if (!_isSetDodgeDir && _isPlayerInBack)
        {
            _fsm.GetState<EnemyCrowDodgeState>().SetFlip(true);
            _isSetDodgeDir = true;
        }
    }
    private bool CheckCanDodge()
    {
        //如果靠墙可以直接跳跃
        if (!_isAbilityDown)
            return false;
        _isRangeLedgeVerticalFront = ColliderCheck.RangeLedgeVerticalFront;
        _isRangeLedgeVerticalBack = ColliderCheck.RangeLedgeVerticalBack;
        //如果背靠墙或者背靠悬崖  且 不会跳进坑里面
        if ((_isWallBack || _isLedgeVerticalBack) && _isRangeLedgeVerticalFront)
        {
            //翻转是为了正对主角
            _fsm.GetState<EnemyCrowDodgeState>().SetFlip(true);
            return true;
        }
        //如果正对墙或者正靠悬崖  且 不会跳进坑里面
        if ((_isWallFront || _isLedgeVerticalFront) && _isRangeLedgeVerticalBack)
            return true;
        //进到这里只有不翻转跳跃的情况
        return _isRangeLedgeVerticalBack;
    }
}
