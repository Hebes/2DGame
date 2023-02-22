using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[InlineEditor]
[CreateAssetMenu(fileName = "newHeroManData", menuName = "数据/英雄/HeroManData")]
public class HeroManData : ScriptableObject
{
    protected const string HeroBaseProperty = "Hero的基本属性";
    protected const string IdleStateProperty = "Idle状态数据相关";
    protected const string RangeAttackStateProperty = "RangeAttack状态数据相关";
    protected const string InAirStateProperty = "InAir状态数据相关";
    protected const string DashStateProperty = "Dash状态数据相关";
    protected const string JumpStateProperty = "Jump状态数据相关";
    protected const string CrouchStateProperty = "Crouch状态数据相关";
    protected const string RollStateProperty = "Roll状态数据相关";
    protected const string GroundSlideStateProperty = "GroundSlide状态数据相关";
    protected const string WallSlideStateProperty = "WallSlide状态数据相关";
    protected const string WallClimbStateProperty = "WallClimb状态数据相关";
    protected const string WallJumpStateProperty = "WawllJump状态数据相关";
    protected const string LedgeClimbStateProperty = "LedgeClimb状态数据相关";
    protected const string LedgeJumpStateProperty = "LedgeJump状态数据相关";
    protected const string OtherSetting = "其他设置";

    [PreviewField(100, ObjectFieldAlignment.Left), Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [FoldoutGroup("$HeroBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float moveVelocity = 10;
    [FoldoutGroup("$HeroBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float attractCoinDis=10;//可以吸收金币的范围
    [FoldoutGroup("$HeroBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float attrackCoinMinPressTime=0.5f;//最小需要按键时间



    [FoldoutGroup("$JumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float jumpVelocity = 15;
    [FoldoutGroup("$JumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public int maxJumpNumber = 1;
    [FoldoutGroup("$JumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public JumpHeightMsg[] jumpHeightMsgs;
    [FoldoutGroup("$CrouchStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float standHeight = 1.8f;
    [FoldoutGroup("$CrouchStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float crouchHeight = 0.9f;
    [FoldoutGroup("$CrouchStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float crouchVelocity = 5f;




    [FoldoutGroup("$InAirStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float craceTime = 0.2f;//土狼时间





    [FoldoutGroup("$RollStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float rollSpeed = 7f;
    [FoldoutGroup("$RollStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float roolMinStartTime = 1f;




    [FoldoutGroup("$GroundSlideStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float slideSpeed = 5f;
    [FoldoutGroup("$GroundSlideStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float _slideMinStartTime = 1f;




    [FoldoutGroup("$WallSlideStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float wallSlideVelocity = 2f;




    [FoldoutGroup("$WallClimbStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float wallClimbVelocity = 3f;




    [FoldoutGroup("$LedgeClimbStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 _startOffset;//默认填写正值即可
    [FoldoutGroup("$LedgeClimbStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 _endOffset;




    [FoldoutGroup("$LedgeJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 ledgeJumpDir = new Vector2(1, 2);
    [FoldoutGroup("$LedgeJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float ledgeJumpVeloctiy = 10;


    [FoldoutGroup("$WallJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 wallJumpDir = new Vector2(1, 2);//墙跳方向
    [FoldoutGroup("$WallJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float wallJumpStrength = 20f;//墙跳力度
    [FoldoutGroup("$WallJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float wallJumpTime = 0.4f;//墙跳时间    
    [FoldoutGroup("$WallJumpStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float wallCraceTime = 0.1f;//墙跳土狼时间


    [FoldoutGroup("$DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashTime;
    [FoldoutGroup("$DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashVelocity;
    [FoldoutGroup("$DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashCoolTime;
    [FoldoutGroup("$DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashShadowCreateDis=0.4f;//生成一次残影的距离
    [FoldoutGroup("$DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f),PreviewField(100)]
    public GameObject dashShadowPrefab;

    [FoldoutGroup("$RangeAttackStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float rangeAttackVeloctiy=5f;
    [FoldoutGroup("$RangeAttackStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float oneRangeAttackMagicPowerSpend=10;//一次远程攻击耗费魔法值
}
[System.Serializable]
public class JumpHeightMsg
{
    [EnumToggleButtons,HideLabel]
    public E_JumpHeight _jumpHeight;
    public float _multiple;
    public float _offsetTime;
    public JumpHeightMsg(E_JumpHeight jumpHeight, float multiple, float offsetTime)
    {
        _jumpHeight = jumpHeight;
        _multiple = multiple;
        _offsetTime = offsetTime;
    }
}
