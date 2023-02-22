using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimbState : HeroManBaseState
{
    protected Vector2 _workspace;
    protected bool _isHanding;
    protected Vector3 _detectedPos = Vector3.zero;
    protected Vector3 _endPos;
    protected Vector3 _startPos;
    protected float _playerHeightHalf;
    protected bool _climbUp;//是否决定向上攀爬

    protected float _xInput;
    protected float _yInput;
    protected bool _jumpInput;
    protected bool _isCeiling;
    public LedgeClimbState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        //计算得到正常情况下人物的图片的高度
        _playerHeightHalf = _ower.GetComponent<SpriteRenderer>().bounds.extents.y;
        AddTargetState(() => _isHanding && _xInput == -Move.FacingDir && _jumpInput, E_CharacterState.WALLJUMP);
        //AddTargetState(() => AnimFinish && _isCeiling, E_CharacterState.CROUCHIDLE);
        AddTargetState(() => AnimFinish && _climbUp, E_CharacterState.LEDGEJUMP);
        AddTargetState(() => !AnimFinish && !_climbUp, E_CharacterState.INAIR);
    }
    public override void Check()
    {
        base.Check();
        _isCeiling = ColliderCheck.Ceiling;
    }
    public override void Enter()
    {
        base.Enter();
        //计算得到贴墙的位置
        Move.SetVelocityZero();
        _ower.SetPosition(_detectedPos);
        _startPos = CalculateRightPos();
        _ower.SetPosition(_startPos);
        //计算得到endPos
        _endPos = _startPos;
        _endPos.y += _playerHeightHalf;
        _endPos.x += _data._endOffset.x * Move.FacingDir;
        _endPos.y += _data._endOffset.y;
        _isHanding = false;
        _climbUp = true;//默认最好是可以攀爬的
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInput = _ower.InputHandle.XInput;
        _yInput = _ower.InputHandle.YInput;
        _jumpInput = _ower.InputHandle.JumpInput;
        CheckIfInHanding();
        CheckRightPos();
    }
    private Vector2 CalculateRightPos()
    {
        Vector3 pos = ColliderCheck._wallFrontCheckTf.position;
        Vector3 dir = _ower.transform.right;
        float dis = ColliderCheck._wallAndLedgeCheckDis;
        var layer = ColliderCheck._groundLayer;
        var xHit = Physics2D.Raycast(pos, dir, dis, layer);
        float xPos = _ower.transform.position.x + xHit.distance * Move.FacingDir;
        pos = ColliderCheck._ledgeHorizontalCheckTf.position;
        pos.x += xPos;
        dir = -_ower.transform.up;
        dis = ColliderCheck._ledgeHorizontalCheckTf.position.y - ColliderCheck._wallFrontCheckTf.position.y;
        var yhit = Physics2D.Raycast(pos, dir, dis, layer);
        float yPos = pos.y - yhit.distance - _playerHeightHalf;
        xPos += _data._startOffset.x * Move.FacingDir;
        yPos += _data._startOffset.y;
        _workspace.Set(xPos, yPos);
        return _workspace;
    }
    private void CheckIfInHanding()
    {
        if (_isHanding)
        {
            if (_xInput == Move.FacingDir)
            {
                _isHanding = false;
                _climbUp = true;
                _anim.Play(Consts.CHARACTER_ANM_LEDGE_CLIIMB);
            }
            else if (_yInput == -1)
            {
                _isHanding = false;
                _climbUp = false;
            }
        }
    }
    private void CheckRightPos()
    {
        //需要保持主角位置暂时不变
        if (!AnimFinish)
        {
            Move.SetVelocityZero();
            _ower.SetPosition(_startPos);
        }
        else
            _ower.SetPosition(_endPos);
    }
    public override void AnimatorEnterTrigger()
    {
        base.AnimatorEnterTrigger();
        _isHanding = true;
        _anim.Play(Consts.CHARACTER_ANM_LEDGE_HOLD);
    }
    public void SetDetectedPos(Vector2 pos) => _detectedPos = pos;
}
