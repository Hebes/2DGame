using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolConfig
{
    private static Dictionary<string, PoolConfigData> poolConfigDic = new Dictionary<string, PoolConfigData>()
    {
        #region 预制体名字对应的配置
        //特效
           { Consts.PREFAB_EFFECT_NAME_HERODASHSHADOW,new PoolConfigData(false,5,5,10f)},
        { Consts.PREFAB_EFFECT_NAME_HERODHIT,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_HERODDEAD,new PoolConfigData(false,1,1,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYHIT_WHITE,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYHIT_RED,new PoolConfigData(false,5,5,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYHIT_GREEN,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYHIT_YELLOW,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYDEAD_WHITE,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYDEAD_RED,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYDEAD_GREEN,new PoolConfigData(false,3,3,10f)},
        { Consts.PREFAB_EFFECT_NAME_ENEMYDEAD_YELLOW,new PoolConfigData(false,3,3,10f)},

        //子弹
        {Consts.PREFAB_BULLET_NAME_BOSSMINOTAURBALL, new PoolConfigData(false,5,5,10f)},
        {Consts.PREFAB_BULLET_NAME_DEMONBALL, new PoolConfigData(true,30,20,10f)},
        {Consts.PREFAB_BULLET_NAME_FIREWORMBALL, new PoolConfigData(false,10,10,10f)},
        {Consts.PREFAB_BULLET_NAME_FLYEYEBALL, new PoolConfigData(false,5,5,10f)},
        {Consts.PREFAB_BULLET_NAME_HUNTTRESSSPEAR, new PoolConfigData(false,5,5,10f)},
        {Consts.PREFAB_BULLET_NAME_HEROARROW,new PoolConfigData(false,3,3,5f)},

        //炸弹
        { Consts.PREFAB_BOMB_NAME_BOSSDEMON,new PoolConfigData(true,30,15,10f)},
        { Consts.PREFAB_BOMB_NAME_BOSSMINOTAUR,new PoolConfigData(false,2,2,10f)},







        #endregion
        { Paths.PREFAB_EFFECT_SWORDHITBULLET,new PoolConfigData(false,5,5,10f)},
        { Paths.PREFAB_EFFECT_SWORDHITENEMY,new PoolConfigData(false,5,5,10f)},
        { Paths.PREFAB_EFFECT_HEROJUMP,new PoolConfigData(false,5,5,10f)},
        { Paths.PREFAB_EFFECT_HEROLAND,new PoolConfigData(false,5,5,10f)},
        { Paths.PREFAB_EFFECT_SWORDHITPARRYEFFECT,new PoolConfigData(false,5,5,10f)},
        { Paths.PREFAB_EFFECT_TOUCHINGCOIN,new PoolConfigData(false,15,15,10f)},
        { Paths.PREFAB_EFFECT_BULLETBOUNCE,new PoolConfigData(false,10,10,10f)},
        { Paths.PREFAB_COIN,new PoolConfigData(false,15,15,10f)},
        #region 预制体路径对应的配置
	    #endregion
    };
    public static PoolConfigData GetConfigData(string prefabName)
    {
        if (!poolConfigDic.ContainsKey(prefabName))
        {
            Debug.LogError($"当前名称的预制体:{prefabName}没有对应的预制体配置信息");
            return null;
        }
        return poolConfigDic[prefabName];
    }
}
//对象池配置数据信息
public class PoolConfigData
{
    public bool autoDestroy;
    public int maxNum;
    public float delayDestroyTime;//延迟销毁的时间
    public int preLoadNum;//预加载的数量
    public PoolConfigData(bool autoDestroy, int maxNum, int preLoadNum, float delayDestroyTime)
    {
        this.autoDestroy = autoDestroy;
        this.maxNum = maxNum;
        this.preLoadNum = preLoadNum;
        this.delayDestroyTime = delayDestroyTime;
    }
}


