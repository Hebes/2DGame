public enum E_CharacterState
{
    IDLE,
    MOVE,
    JUMP,
    INAIR,
    CROUCHIDLE,
    CROUCHMOVE,
    SLIDE,
    WALLSLIDE,
    WALLGRAB,
    WALLCLIMB,
    WALLJUMP,
    LAND,
    ROLL,
    LEDGECLIMB,
    LEDGEJUMP,
    DASH,
    DASHTWO,//冲刺第二段
    MELEEATTACK,//近战攻击
    MELEEATTACKTWO,//近战攻击2
    MELEEAIRATTACK,//近战空中攻击
    MELEEAIRATTACK_LOOP,//近战空中攻击



    RANGEATTACK,//远程攻击
    RANGEATTACKTWO,//远程攻击2
    RANGEATTACKTHREE,//远程攻击3
    RANGEATTACKFOUR,//远程攻击4
    RANGEAIRATTACK,//远程空中攻击



    DEAD,//死亡状态
    HIT,//受伤状态



    DETECTED,//警惕状态    
    CHARGE,//追逐状态
    LOOKFOR,//寻找状态
    BACK,//后退状态,
    DODGE,//闪避状态
    WAIT,//等待状态
    SCARED,//受惊状态
    READY,//准备状态
    RIGIDITY,//僵直状态,
    SHIELD,//举盾状态
    APPEAR,//现身状态
    DISAPPEAR,//消失状态
    GETITEM,//拾取物品
}
public enum E_JumpHeight
{
    MAXHEIGHT,
    MIDHEIGHT,
    MINHEIGHT
}
public enum E_Attack
{
    NONE,
    MELEE,
    MELEE_AIR,
    MELEE_AIRLOOP,
    RANGE,
    RANGE_AIR,   
    TRAP//陷阱攻击
}
/// <summary>
/// 阵营
/// </summary>
public enum E_Group
{
    NONE,
    HERO,//英雄
    NORMALENEMY,//普通敌人
    DESTRUCTIBLEITEM,//可销毁的物体
    SUPERENEMY//Boss
}
public enum E_UILayer
{
    BASE,
    MID,
    TOP,
    DIALOG,//对话框
}
//各个场景的名称
public enum E_SceneName
{
    None,
    InitScene,
    StartScene,
    LevelOne,
    LevelTwo,
    TestScene,
    Count
}
//键位类型
public enum E_KeyType
{
    Key,
    ValueKey,
    AxisPosKey,
    AxisNegKey,
}
//游戏状态
public enum E_GameState
{
    NONE,
    Lose,
    Win,
    Pause,
    COUNT
}
//UI音乐类型
public enum E_UIMusic
{
    ChooseArchive,
    NormalClick,
    NotClick,
    SliderChange,
    ToggleClick,
    Enter,
    None
}
//游戏背景音乐
public enum E_BattleMusic
{
    battlemusic_levelOne,
    battlemusic_levelTwo,
}
//游戏战斗音乐
public enum E_BgMusic
{
    bgmusic_levelOne,
    bgmusic_levelTwo,
    bgmusic_startScene,
    bgmusic_gameEnd,
}
    



