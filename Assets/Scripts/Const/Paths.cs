using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Paths
{
    #region Assets路径下的资源相关
    public readonly static string STREAMINGASSETS_FOLDER = Application.streamingAssetsPath + "/";
    public readonly static string CFG_FOLDER = STREAMINGASSETS_FOLDER + "Cfg/";
    public readonly static string GAMEDATA_FOLDER = STREAMINGASSETS_FOLDER + "GameData/";
    public readonly static string JSONDATA_FOLDER = GAMEDATA_FOLDER + "JsonData/";
    public readonly static string BINARYDATA_FOLDER = GAMEDATA_FOLDER + "BinaryData/";
    //配置数据
    public readonly static string CFG_AUDIO = CFG_FOLDER + "AudioCfgData.json";
    public readonly static string CFG_LOADINGPANELTIPS = CFG_FOLDER + "LoadingPanelTips.json";
    public readonly static string CFG_INPUTSYSTEM = CFG_FOLDER + "InputSystem.json";
    public readonly static string CFG_GAMEPLACENAME = CFG_FOLDER + "GamePlaceName.json";
    public readonly static string CFG_GAMELEVELNAME = CFG_FOLDER + "GameLevelName.json";
    //游戏数据

    #endregion
    #region Resource资源路径相关
    //预制体
    public const string PREFAB_FLODER = "Prefabs/";
    public const string PREFAB_ENEMY_FLODER = PREFAB_FLODER + "Enemy/";
    public const string PREFAB_BULLET_FLODER = PREFAB_FLODER + "Bullet/";
    public const string PREFAB_HERO_FLODER = PREFAB_FLODER + "Hero/";
    public const string PREFAB_EFFECT_FLODER = PREFAB_FLODER + "Effects/";
    public const string PREFAB_COIN_FLODER = PREFAB_FLODER + "Coin/";
    public const string PREFAB_DESTRUCTIBLEITEM_FLODER = PREFAB_FLODER + "DestructibleItem/";
    public const string PREFAB_MAPDECORAION_FLODER = PREFAB_FLODER + "MapDecoration/";
    public const string PREFAB_UI_FLODER = PREFAB_FLODER + "UI/";
    private const string PREFAB_UIPANEL_FLODER = PREFAB_UI_FLODER + "UIPanel/";
    private const string PREFAB_UIELEMENT_FLODER = PREFAB_UI_FLODER + "UIElement/";


    //特效预制体
    public const string PREFAB_EFFECT_SWORDHITBULLET= PREFAB_EFFECT_FLODER+"Effect_SwordHitBullet";
    public const string PREFAB_EFFECT_SWORDHITENEMY= PREFAB_EFFECT_FLODER+"Effect_SwordHitEnemy";
    public const string PREFAB_EFFECT_SWORDHITPARRYEFFECT= PREFAB_EFFECT_FLODER+"Effect_SwordHitParryEffect";
    public const string PREFAB_EFFECT_TOUCHINGCOIN= PREFAB_EFFECT_FLODER+ "Effect_CoinTouching";
    public const string PREFAB_EFFECT_HEROJUMP = PREFAB_EFFECT_FLODER + "Effect_HeroJump";
    public const string PREFAB_EFFECT_HEROLAND = PREFAB_EFFECT_FLODER + "Effect_HeroLand";
    public const string PREFAB_EFFECT_BULLETBOUNCE = PREFAB_EFFECT_FLODER + "Effect_BulletBounce";

    //UI预制体



    //UIPanel
    public const string PREFAB_UIPANEL_LOADINGPANEL = PREFAB_UIPANEL_FLODER + "LoadingPanel";
    public const string PREFAB_UIPANEL_STARTPANEL = PREFAB_UIPANEL_FLODER + "StartPanel";
    public const string PREFAB_UIPANEL_ABOUTPANEL = PREFAB_UIPANEL_FLODER + "AboutPanel";
    public const string PREFAB_UIPANEL_DIALOGPANEL = PREFAB_UIPANEL_FLODER + "DialogPanel";
    public const string PREFAB_UIPANEL_GETITEMDIALOGPANEL = PREFAB_UIPANEL_FLODER + "GetItemDialogPanel";
    public const string PREFAB_UIPANEL_GAMELEVELTIPDIALOGPANEL = PREFAB_UIPANEL_FLODER + "GameLevelTipDialogPanel";
    public const string PREFAB_UIPANEL_SETTINGPANEL = PREFAB_UIPANEL_FLODER + "SettingPanel";
    public const string PREFAB_UIPANEL_BAGPANEL = PREFAB_UIPANEL_FLODER + "BagPanel";
    public const string PREFAB_UIPANEL_GAMEPANEL = PREFAB_UIPANEL_FLODER + "GamePanel";
    public const string PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL = PREFAB_UIPANEL_FLODER + "ChooseGameArchivePanel";
    public const string PREFAB_UIPANEL_ARCHIVEPOINTPANEL = PREFAB_UIPANEL_FLODER + "ArchivePointPanel";
    public const string PREFAB_UIPANEL_GAMEENDPANEL = PREFAB_UIPANEL_FLODER + "GameEndPanel";
    public const string PREFAB_UIPANEL_GAMESETTINGPANEL = PREFAB_UIPANEL_FLODER + "GameSettingPanel";
    public const string PREFAB_UIPANEL_CHATPANEL = PREFAB_UIPANEL_FLODER + "ChatPanel";
    public const string PREFAB_UIPANEL_SHOPPANEL = PREFAB_UIPANEL_FLODER + "ShopPanel";
    public const string PREFAB_UIPANEL_GAMEBLACKPANEL = PREFAB_UIPANEL_FLODER + "GameBlackPanel";
    //UIElement
    public const string PREFAB_UIELEMENT_HPITEMELEMENT = PREFAB_UIELEMENT_FLODER+"HpItemElement";
    public const string PREFAB_UIELEMENT_GAMELEVEITEM = PREFAB_UIELEMENT_FLODER+"GameLevelItem";
    public const string PREFAB_UIELEMENT_GAMEPLACEITEM = PREFAB_UIELEMENT_FLODER+"GamePlaceItem";
    public const string PREFAB_UIELEMENT_SELECTIONITEM = PREFAB_UIELEMENT_FLODER+ "SelectionItemElement";
    public const string PREFAB_UIELEMENT_ITEMELEMENT= PREFAB_UIELEMENT_FLODER+ "ItemElement";




    public const string PREFAB_HERO_STATRSCENE = PREFAB_HERO_FLODER + "StartScenePlayer";
    public const string PREFAB_HERO_GAMESCENE = PREFAB_HERO_FLODER + "GameScenePlayer";

    //金币
    public const string PREFAB_COIN = PREFAB_COIN_FLODER + "Coin";



    //音频
    public const string AUDIO_FOLIDER = "Audio/";



    //图片
    public const string PICTURE_FOLDER = "Pictures/";
    public const string PICTURE_ICON_FOLDER = PICTURE_FOLDER + "Icon/";
    public const string PICTURE_NORMAL_FOLDER = PICTURE_FOLDER + "Normal/";





    //数据
    public const string DATA_FOLDER = "Data/";
    public const string DATA_INPUT_FOLDER = DATA_FOLDER + "InputData/";
    public const string DATA_NPCDATA_FOLDER = DATA_FOLDER + "NPCData/";
    public const string DATA_ITEMSDATA_FOLDER = DATA_FOLDER + "ItemData/";
    public const string DATA_SHOPDATAS_FOLDER = DATA_FOLDER + "ShopsData/";


    public const string DATA_ITEMDATA = DATA_ITEMSDATA_FOLDER + "ItemsData";
    public const string DATA_SHOPDATA = DATA_SHOPDATAS_FOLDER + "ShopDatas";
    public const string DATA_INPUT = DATA_INPUT_FOLDER + "InputData";
    public const string DATA_DEFAULTINPUT = DATA_INPUT_FOLDER + "DefaultInputData";
    #endregion


    #region   游戏数据相关
    public const string ASSETS_FOLDER = "Assets/";
    public const string RESOURCES_FOLDER = ASSETS_FOLDER + "Resources/";



    public const string GAMEDATA_FLODER = ASSETS_FOLDER + "Data/";
    public const string GAMEDATA_BULLET_FOLDER = GAMEDATA_FLODER + "BulletData";
    public const string GAMEDATA_ENEMY_FOLDER = GAMEDATA_FLODER + "EnemyData";
    public const string GAMEDATA_HERO_FOLDER = GAMEDATA_FLODER + "HeroManData";

    #endregion
}
