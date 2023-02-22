using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossDemonAppearState : AirBossBaseAppearState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private float demonWidth;
    private float demonHeight;
    private Vector3 maxPos;
    private Vector3 minPos;
    private string _audioClipName = "enemy_demon_appear";
    public AirBossDemonAppearState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.DETECTED);
        InitValue();
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    private void InitValue()
    {
         demonWidth = _ower.Render.bounds.size.x/2;
         demonHeight = _ower.Render.bounds.size.y/2;
    }
    protected override void AppearAction()
    {
        base.AppearAction();
        _ower.Render.enabled = true;
        Behavior.SetActive(true);
    }
    protected override Vector3 GetPos()
    {
        maxPos = ScreenPosUtil.GetScreenMaxPos() - new Vector3(demonWidth, demonHeight);
        //最小点 不需要 因为Demon的轴心点的缘故
        minPos = ScreenPosUtil.GetScreenMinPos();
        var xPos = Random.Range(minPos.x,maxPos.x);
        var yPos = Random.Range(minPos.y+(maxPos.y-minPos.y)/2,maxPos.y);
        return new Vector3(xPos,yPos,0);
    }
}
