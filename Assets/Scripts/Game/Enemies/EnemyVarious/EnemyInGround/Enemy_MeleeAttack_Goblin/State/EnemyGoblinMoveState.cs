using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGoblinMoveState : EnemyBaseMoveState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{

    protected bool _canEnterDodgeState;
    protected bool _isOverToIdleState;
    public EnemyGoblinMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        SetIsOverToIdleState(true);
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        //���������ǰ״̬����������
        AddTargetState(() => (_isWallFront || !_isLedgeVerticalFront || CheckIsMoveOver())&&!_isOverToIdleState&&_canEnterDodgeState,E_CharacterState.DODGE,()=>fsm.GetState<EnemyGoblineDodgeState>().SetIsEnterAfterInAirScaredState(false));
        AddTargetState(() => (_isWallFront || !_isLedgeVerticalFront || CheckIsMoveOver()), E_CharacterState.IDLE);
    }
    public override void Enter()
    {
        base.Enter();
        SetIsOverToIdleState(Random.Range(0, 100) < _data.moveOverToIdleProbability);
    }
    public override void Check()
    {
        base.Check();
        _canEnterDodgeState = ColliderCheck.RangeLedgeVerticalBack;
    }
    public override void Exit()
    {
        base.Exit();
        SetIsOverToIdleState(true);
    }
    //����Ƿ��ƶ�����
    private bool CheckIsMoveOver()
        => Time.time >= _enterTime + _data.maxMoveTime;

    public void SetIsOverToIdleState(bool value)
        => _isOverToIdleState = value;
}
