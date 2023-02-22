using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossDemonDeadState : AirBossBaseDeadState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_dead";
    public AirBossDemonDeadState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void DeadAction()
    {
        GameStateModel.Instance.CurGameState = E_GameState.Win;
        EventMgr.Instance.ExecuteEvent(E_EventName.BOSS_DEAD, 1);
        base.DeadAction();
    }
}
