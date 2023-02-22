using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RgMoveComponent : ComponentBase
{
    public int FacingDir { get; private set; }//当前面朝向
    public Rigidbody2D RB { get; private set; }
    protected Vector2 _workSpace;
    public bool CanSetVelocity { get; set; }//是否可以设置当前速度
    public bool CanFlip { get; set; }//是否可以翻转
    protected float _startGravityScale;//初始重力大小
    public override void Init()
    {
        base.Init();
        RB =_owerTf.GetComponent<Rigidbody2D>();
        if(RB==null)
            Debug.LogError($"当前物体:{transform.parent.parent.name}对应的刚体组件不存在");
        CanSetVelocity = true;
        CanFlip = true;
        FacingDir = 1;
        _startGravityScale = RB.gravityScale;
    }
    public void SetXVelocity(float xValue)
    {
        _workSpace.Set(xValue, RB.velocity.y);
        CheckIfCanSet();
    }
    public void SetXVelocityInFacing(float xValue, bool isFacingDir)
    {
        if (isFacingDir)
            _workSpace.Set(xValue * FacingDir, RB.velocity.y);
        else
            _workSpace.Set(xValue * -FacingDir, RB.velocity.y);
        CheckIfCanSet();
    }
    public void SetYVelocity(float yValue)
    {
        _workSpace.Set(RB.velocity.x, yValue);
        CheckIfCanSet();
    }
    public void CheckIfCanSet()
    {
        if (CanSetVelocity)
            RB.velocity = _workSpace;
    }

    public void SetVelocityButAbsY(float strength, Vector2 dir)
    {
        dir.Normalize();
        _workSpace.Set(strength*dir.x, strength*Mathf.Abs(dir.y==0f?0f:Mathf.Sign(dir.y)));
        CheckIfCanSet();
    }
    public void SetVelocity(float strength, Vector2 dir)
    {
        dir.Normalize();
        _workSpace.Set(strength * dir.x, strength * dir.y);
        CheckIfCanSet();
    }
    public void SetVelocityInDir(float strength, Vector2 dir, bool facingDir)
    {
        dir.Normalize();
        if (facingDir)
            _workSpace.Set(strength * dir.x * FacingDir, strength * dir.y);
        else
            _workSpace.Set(strength * dir.x * -FacingDir, strength * dir.y);
        CheckIfCanSet();
    }
    public void SetVelocityZero() => RB.velocity = Vector2.zero;
    public Vector2 GetCurVelocity => RB.velocity;

    public void CheckCanFlip(int xInput)
    {
        if (xInput!=0&&xInput != FacingDir && CanFlip)
            Flip();
    }
    public virtual void Flip()
    {
        FacingDir *= -1;
        _owerTf.localEulerAngles = new Vector3(0,FacingDir==1?0:180f,0);
    }
    //设置重力Scale
    public void SetGravityScale(float value = 0f)
        => RB.gravityScale = value;
    //还原重力Scale
    public void ResetGravityScale()
        => RB.gravityScale = _startGravityScale;
}
