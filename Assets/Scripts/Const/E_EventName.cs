public enum E_EventName
{
    CHARACTER_HIT,//角色受伤
    CHARACTER_HITOVER,//从受伤状态中恢复
    CHARACTER_DEAD,//角色死亡
    CHARACTER_INAIR,//角色在空中
    CHARACTER_CANFLIP,//角色在可以翻转
    CHARACTER_STOPFLIP,//角色在不可以翻转   
    CHARACTER_PARRY,//弹反
    CHARACTER_INVINCIBLE,//无敌帧
    CHARACTER_STOPINVINCIBLE,//无敌帧结束
    CHARACTER_DOMINEERING,//受伤但是不被击退
    CHARACTER_CREATEDEADSPRITE,//创建DeadSprite
    CHARACTER_BEATBACK,//角色受到击退效果
    CHARACTER_RIGIBIDY,//角色进入僵直状态
    CHARACTER_GETITEM,//主角进入拾取物品状态
    CHARACTER_DASH,//主角进入冲刺状态或者结束冲刺状态
    CHARACTER_SETSHIELD,//打开主角护盾



    CHARACTER_REST,//主角在篝火前面进行休息




    CHARACTER_CANTCTROL,//不可控制玩家状态
    CHARACTER_CHECKCANRECOVER,//检测玩家是否可以恢复生命值
    CHARACTER_ADDATTACK,//增加玩家攻击力
    CHARACTER_ADDMAXHEALTH,//增加玩家体力值
    CHARACTER_ADDMAXMAGICPOWER,//增加玩家最大魔法值
    CHARACTER_ADDMAGICPOWER,//增加玩家当前法力值
    CHARACTER_ATTRACTCOIN,//主角开始吸收金币


#region    使用道具相关
    USEITEM_RECOOVERALLHP,//恢复当前所有生命值
    USEITEM_IMPROVERECOVER_TEMPORARY,//暂时提高恢复能力

#endregion


    BULLET_KNOCK,//子弹撞到墙上或者地面上  了
    BULLET_REVERSEDIR,//子弹反向
    BULLET_HIT,//子弹受伤
    BULLET_TAKEDAMAGE,//子弹对目标造成伤害

    BOMB_EXLPOSTION,//炸弹爆炸
    
    STAB_CHANGESPROTE,//地刺改变自身Sprite


    BOSS_DEAD,//Boss死亡
    CHAT_OVER,//对话结束

    #region   UI事件相关
    UI_SETTINGPLANE_CHANGETITLE,//SettingPanel改变Title
    UI_REFRESHPLAYERHP,//更新角色血量
    UI_REFRESHPLAYERMAGIC,//更新角色法力值
    UI_REFRESHPLAYERLIGHT,//更新角色光源值
    UI_REFRESITEMINFO,//更新物品信息UI
    UI_REFRESGAMEPANELCURITEMINFO,//更新
    UI_REFRESCOIN,//更新角色金币
    UI_REFRESSHOPITEMTEXT,//刷新ShopItem的文字文本
    #endregion



    #region   输入事件相关
    INPUTSYS_SETKEYNONE,//置空指定键位 防止重复设置
    INPUTSYS_CLOSEOTHERSETTINGKEY,//关闭当前没有设置的键位的设置显示
    #endregion

    BEGINNERGUIDANCE_RFRESH//刷新新手引导文字
}