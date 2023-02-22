using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowHitState : EnemyBaseHitState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGround, E_CharacterState.INAIR);
        AddTargetState(() => CheckCanDodge(), E_CharacterState.DODGE);
        AddTargetState(() => AnimFinish, E_CharacterState.BACK);
    }
   
    private bool CheckCanDodge()
    {
        //如果靠墙可以直接跳跃
        if (!AnimFinish)
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
        if ((_isWallFront||_isLedgeVerticalFront) && _isRangeLedgeVerticalBack)
            return true;
        //进到这里只有不翻转跳跃的情况
        return _isRangeLedgeVerticalBack;
    }
}
