using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[InlineEditor]//加上该特性才能在面板中正确的显示
public class EnemyBaseData : ScriptableObject
{
    #region  常量
    protected const string EnemyBaseProperty = "敌人的基本属性";
    protected const string IdleStateProperty = "Idle状态数据相关";
    protected const string MoveStateProperty = "Move状态数据相关";
    protected const string DetectedStateProperty = "Detected状态数据相关";
    protected const string LookForPlayerStateProperty = "LookForPlayer状态数据相关";
    protected const string ChargeStateProperty = "Charge状态数据相关";
    protected const string DodgeStateProperty = "Dodge状态数据相关";
    protected const string BackStateProperty = "Back状态数据相关";
    protected const string MeleeAttackStateProperty = "MeleeAttack状态数据相关";
    protected const string RangeAttackStateProperty = "RangeAttack状态数据相关";
    protected const string InAirStateProperty = "InAir状态数据相关";
    protected const string DashStateProperty = "Dash状态数据相关";
    protected const string ScaredStateProperty = "Scared状态数据相关";
    protected const string HitStateProperty = "Hit状态数据相关";
    protected const string RigidityStateProperty = "Rigidity状态数据相关";
    protected const string ShieldStateProperty = "Shield状态数据相关";
    protected const string OtherSetting = "其他设置";

    #endregion




    [PreviewField(100, ObjectFieldAlignment.Left), ShowInInspector, Required, DisableIf("@sprite!=null")]
    public Sprite sprite;
    [FoldoutGroup("@EnemyBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float moveVelocity = 10;
    [FoldoutGroup("@EnemyBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float jumpVelocity = 15;
    [FoldoutGroup("@EnemyBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public int deadCoin=2;//死亡后的金币数量
    [FoldoutGroup("@EnemyBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float deadMagicPower=2;//死亡后主角恢复的魔力值
    [FoldoutGroup("@EnemyBaseProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 deadCoinCreateOffset;

    [FoldoutGroup("@IdleStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float minIdleTime = 1f;//Idle最小持续时间
    [FoldoutGroup("@IdleStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float maxIdleTime = 2f;//Idle最大持续时间




    [FoldoutGroup("@DetectedStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float maxDetectedTime = 1f;//最大观察时间
    [FoldoutGroup("@DetectedStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float detectedMinFlipTime = 0.5f;//观察状态下的转向CD





    [FoldoutGroup("@LookForPlayerStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float turnNumber = 4;//需要转向的次数
    [FoldoutGroup("@LookForPlayerStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float turnTime = 1f;//每次转向的间隔时间
    [FoldoutGroup("@LookForPlayerStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float lookForPlayerVelocity = 15;//寻找状态的移动速度




    [FoldoutGroup("@ChargeStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float chargeVelocity;//追逐速度
    [FoldoutGroup("@ChargeStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float chargeTime;//追逐时间
    [FoldoutGroup("@ChargeStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float chargeMinFlipTime = 0.5f;



    [FoldoutGroup("@DodgeStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dodgeStrength = 15;//闪避状态力量  
    [FoldoutGroup("@DodgeStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 dodgeDir = new Vector2(1, 2);//闪避状态方向
    [FoldoutGroup("@DodgeStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dodgeCoolDownTime=2f;


    [FoldoutGroup("@BackStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float backMovementVelocity = 5;
    [FoldoutGroup("@BackStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float backTime = 1.5f;//后退持续时间
    [FoldoutGroup("@BackStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float minBackTime = 0.5f;//最小后退时间
    [FoldoutGroup("@BackStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float minBackFlipTime=0.5f;//最小后退转向时间




    [FoldoutGroup("@MeleeAttackStateProperty"), LabelWidth(180), GUIColor(0.1f, 1f, 0.1f)]
    public int maxMeleeAttackNumber = 1;
    [FoldoutGroup("@MeleeAttackStateProperty"), LabelWidth(180), GUIColor(0.1f, 1f, 0.1f)]
    public float meleeAttackCoolDownTime=2f;//近战攻击冷却时间
    [FoldoutGroup("@MeleeAttackStateProperty"), LabelWidth(180), GUIColor(0.1f, 1f, 0.1f)]
    public float meleeAttackContinueCoolDownTime = 4f;//近战攻击冷却时间



    [FoldoutGroup("@RangeAttackStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public int rangeAttackNum = 1;//远程攻击的次数
    [FoldoutGroup("@RangeAttackStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public float rangeAttackCoolDownTime = 3f;//远程攻击的冷却时间
    [FoldoutGroup("@RangeAttackStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 createBulletOffset;//位置修正
    [FoldoutGroup("@RangeAttackStateProperty"), LabelWidth(150), GUIColor(0.1f, 1f, 0.1f)]
    public Vector2 createBombOffset;//位置修正

    [FoldoutGroup("@InAirStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float inAirDeadTime = 2f;
    [FoldoutGroup("@DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float maxDashTime = 1f;//最大冲刺时间
    [FoldoutGroup("@DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashCoolDownTime = 5f;//冲刺冷却时间
    [FoldoutGroup("@DashStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float dashVelocity = 20f;//冲刺速度







    [FoldoutGroup("@ScaredStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float scaredVelocity=7;//受惊时的速度
    [FoldoutGroup("@ScaredStateProperty"), LabelWidth(100), GUIColor(0.1f, 1f, 0.1f)]
    public float scaredTime=3f;//受惊持续时间






    [FoldoutGroup("@HitStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f), Range(0, 100f)]
    public float hitToBackStateProbability = 30f;


    [FoldoutGroup("@RigidityStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float maxRigidityTime=3f;//最大僵直时间




    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float maxShieldTime=4f;
    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float shieldMoveVelocity=2f;//举盾状态的下的移动速度
    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float shieldminFlipTime=2f;
    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float shieldCoolDownTime = 8f;
    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float shieldBeatBackForece = 3f;//受到击退产生的力
    [FoldoutGroup("@ShieldStateProperty"), LabelWidth(200), GUIColor(0.1f, 1f, 0.1f)]
    public float shieldBeatBackForeceTime = 0.5f;//受到该力的时间

}
