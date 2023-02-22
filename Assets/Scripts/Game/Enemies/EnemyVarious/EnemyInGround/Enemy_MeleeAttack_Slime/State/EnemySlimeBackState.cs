using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySlimeBackState : EnemyBaseBackState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    private string _audioClipName = "enemy_slime_back";
    public EnemySlimeBackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isBackOver,E_CharacterState.MOVE);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    //重写后退Update方法
    protected override void BackActionUpdate()
    {
        if (Time.time >= _lastFlipTime + _data.detectedMinFlipTime)
        {
            Move.LookAtPlayerBetween();
            _lastFlipTime = Time.time;
        }
        CheckIfBackOver();
    }
    protected override void CheckIfBackOver()
    {
        if (_isBackOver)
            return;
        Move.SetXVelocityInFacing(_data.backMovementVelocity, true);
        if (Time.time >= _enterTime + _data.backTime)
            _isBackOver = true;
    }
}
