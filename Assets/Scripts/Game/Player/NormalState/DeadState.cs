using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : HeroManBaseState
{
    protected bool _isDead;
    private string _animClipName = "hero_damage";
    public DeadState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isDead = false;
        AudioMgr.Instance.PlayOnce(_animClipName);
        Move.SetXVelocity(0);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        if (AnimFinish&&!_isDead)
        {
            _isDead = true;
            DeadAction();
        }
    }
    protected void DeadAction()
    {
        GameStateModel.Instance.CurGameState = E_GameState.Lose;
        //每次死亡丢失一般的金币
        GameDataModel.Instance.AddOrReduceCoin(-GameDataModel.Instance.GetCurCoin()/2);
        GameObject.Destroy(_ower.gameObject);
    }
}
