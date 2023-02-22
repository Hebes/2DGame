using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    #region  状态类常量
    public const string CHARACTER_ANM_IDLE = "idle";
    public const string CHARACTER_ANM_MOVE = "move";
    public const string CHARACTER_ANM_JUMP = "inAir";
    public const string CHARACTER_ANM_INAIR = "inAir";
    public const string CHARACTER_ANM_LAND = "land";
    public const string CHARACTER_ANM_CROUCH_IDLE = "crouchIdle";
    public const string CHARACTER_ANM_CROUCH_MOVE = "crouchMove";
    public const string CHARACTER_ANM_SLIDE = "slide";
    public const string CHARACTER_ANM_ROLL = "roll";
    public const string CHARACTER_ANM_WALLSLIDE = "wallSlide";
    public const string CHARACTER_ANM_WALLGRAB = "wallGrab";
    public const string CHARACTER_ANM_WALLCLIMB = "wallClimb";
    public const string CHARACTER_ANM_WALLJUMP = CHARACTER_ANM_INAIR;//墙跳动画和在空中的动画一样
    public const string CHARACTER_ANM_LEDGE_CLIIMB = "ledgeClimb";
    public const string CHARACTER_ANM_LEDGE_HOLD = "ledgeHold";
    public const string CHARACTER_ANM_LEDGE_START = "ledgeStart";//该动画参数通过状态内来设置
    public const string CHARACTER_ANM_LEDGE_JUMP = "ledgeJump";
    public const string CHARACTER_ANM_DASH = "dash";//冲刺状态
    public const string CHARACTER_ANM_DASHLOOP = "dashLoop";//冲刺状态
    private const string CHARACTER_ANM_ATTACK = "attack";
    public const string CHARACTER_ANM_READY = "ready";//准备状态
    public const string CHARACTER_ANM_RIGIDITY = "rigidity";//僵直状态
    public const string CHARACTER_ANM_SHIELD = "shield";//僵直状态
    public const string CHARACTER_ANM_GETITEM = "getItem";//拾取物品状态


    public const string CHARACTER_ANM_MELEEATTACK = CHARACTER_ANM_ATTACK;
    public const string CHARACTER_ANM_AIRMELEEATTACK = CHARACTER_ANM_ATTACK;
    public const string CHARACTER_ANIM_RANGEATTACK = CHARACTER_ANM_ATTACK;
    public const string CHARACTER_ANOM_RANGEAIRATTACK = CHARACTER_ANM_ATTACK;
    public const string CHARACTER_ANM_AIRMELEEATTACK_LOOP = CHARACTER_ANM_ATTACK;




    public const string CHARACTER_ANM_XVleocity = "xVelocity";
    public const string CHARACTER_ANM_YVleocity = "yVelocity";

    public const string CHARACTER_ANM_COMBAT_INDEX = "combatIndex";

    public const string CHARACTER_ANM_DETECTED = CHARACTER_ANM_IDLE;
    public const string CHARACTER_ANM_CHARGE = CHARACTER_ANM_MOVE;
    public const string CHARACTER_ANM_LOOKFOR = CHARACTER_ANM_MOVE;
    public const string CHARACTER_ANM_BACK = CHARACTER_ANM_MOVE;
    public const string CHARACTER_ANM_HIT = "hit";
    public const string CHARACTER_ANM_DEAD = "dead";
    public const string CHARACTER_ANM_WAIT = CHARACTER_ANM_IDLE;
    public const string CHARACTER_ANM_APPEAR = "appear";
    public const string CHARACTER_ANM_DISAPPEAR = "disappear";


    public const string CHARACTER_ANM_EXPLOSITION = "explosion";
    #endregion



    #region  预制体名称
    public const string PREFAB_EFFECT_NAME = "Effect_";
    public const string PREFAB_EFFECT_NAME_HERODASHSHADOW = PREFAB_EFFECT_NAME + "HeroDashShadow";
    public const string PREFAB_EFFECT_NAME_HERODHIT = PREFAB_EFFECT_NAME + "HeroHit";
    public const string PREFAB_EFFECT_NAME_HERODDEAD = PREFAB_EFFECT_NAME + "HeroDead";
    public const string PREFAB_EFFECT_NAME_ENEMYHIT_WHITE = PREFAB_EFFECT_NAME + "EnemyHit_White";
    public const string PREFAB_EFFECT_NAME_ENEMYHIT_RED = PREFAB_EFFECT_NAME + "EnemyHit_Red";
    public const string PREFAB_EFFECT_NAME_ENEMYHIT_GREEN = PREFAB_EFFECT_NAME + "EnemyHit_Green";
    public const string PREFAB_EFFECT_NAME_ENEMYHIT_YELLOW = PREFAB_EFFECT_NAME + "EnemyHit_Yellow";
    public const string PREFAB_EFFECT_NAME_ENEMYDEAD_WHITE = PREFAB_EFFECT_NAME + "EnemyDead_White";
    public const string PREFAB_EFFECT_NAME_ENEMYDEAD_RED = PREFAB_EFFECT_NAME + "EnemyDead_Red";
    public const string PREFAB_EFFECT_NAME_ENEMYDEAD_GREEN = PREFAB_EFFECT_NAME + "EnemyDead_Green";
    public const string PREFAB_EFFECT_NAME_ENEMYDEAD_YELLOW = PREFAB_EFFECT_NAME + "EnemyDead_Yellow";



    public const string PREFAB_BULLET_NAME = "Bullet_";
    public const string PREFAB_BULLET_NAME_HEROARROW = PREFAB_BULLET_NAME + "HeroArrow";
    public const string PREFAB_BULLET_NAME_FIREWORMBALL = PREFAB_BULLET_NAME + "FireWormBall";
    public const string PREFAB_BULLET_NAME_FLYEYEBALL = PREFAB_BULLET_NAME + "FlyEyeBall";
    public const string PREFAB_BULLET_NAME_DEMONBALL = PREFAB_BULLET_NAME + "DemonBall";
    public const string PREFAB_BULLET_NAME_BOSSMINOTAURBALL = PREFAB_BULLET_NAME + "BossMinotaurBall";
    public const string PREFAB_BULLET_NAME_HUNTTRESSSPEAR = PREFAB_BULLET_NAME + "HuntressSpear";



    public const string PREFAB_BOMB_NAME="Bomb_";
    public const string PREFAB_BOMB_NAME_BOSSMINOTAUR = PREFAB_BOMB_NAME + "BossMinotaur";
    public const string PREFAB_BOMB_NAME_BOSSDEMON = PREFAB_BOMB_NAME + "BossDemon";




    #endregion




    #region 其他常量名称
    public const string MATERIAL_ARGNAME_HURT = "_FlashAmount";
    public const string NAME_BULLETDATA = "BulletDatas";
    public const string NAME_ENEMYDATA = "EnemyDatas";
    public const string NAME_HEROMANDATA = "HeroManDatas";
    public const string NAME_COMBATDATA = "CombatDatas";
    public const string NAME_DESTRUCTIBLEITEMDATA = "DestructibleItemData";
    public const string NAME_BOMBDATA = "BombData";
    public const string NAME_NPCDATA = "NPCData";
    public const string NAME_ITEMDATA = "ItemData";
    public const string NAME_SHOPDATA = "ShopData";



    public const string NAME_MELEETATTACK_TYPE = "MELEE";
    public const string NAME_RANGEATTACK_TYPE = "RANGE";



    public const string NAME_DESTRUCTIBLEITEM_DEAD = "deadSprite";
    #endregion



    //弹反的子弹时间增量积
    public const int CHARACTER_PARRY_MULT = 2;
    public const int CHARACTER_ACCELERATION_RATIO = 2;


    public const float CHARACTER_SHOWHURTEFFECT_TIME = 0.5f;//角色受伤显示效果的时间


    //有个坑 这里的RGB必须是不大于1f的小数  否则表示无效
    public static Color ENEMY_DAMAGE_COLOR = new Color(237f / 255f, 90f / 255f, 101f / 255f);
    public static Color HERO_DAMAGE_COLOR = new Color(255f / 255f, 215f / 255f, 0f / 255f);

    #region UI常量相关
    public const string PREFIX_LOADINGPANEL_BG = "bg_";//前缀
    public const string PREFIX_UIITEM_ARROW = "Arrows_";//前缀
    #endregion


    #region    存档文件名称
    public const string DATA_AUDIO = "AudioData";
    public const string DATA_ALLGAMEARCHIVE = "AllGameArchive";
    #endregion


    #region 输入系统键位名称
    public const string ATTACKEY = "Attack";
    public const string GRABKEY = "Grab";
    public const string DASHKEY = "Dash";
    public const string BOWKEY = "Bow";
    public const string JUMPKEY = "Jump";
    public const string USEITEM = "UseItem";
    public const string CHANGEITEM = "ChangeItem";
    public const string ATTRACTCOIN = "AttractCoin";

    public const string HORIZONTALAXIS = "Horizontal";
    public const string VERTICALAXIS = "Vertical";





    #endregion

}
