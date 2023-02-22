/****************************************************
    文件：UIInfoModel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/30 19:21:42
	功能：游戏UI数据管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class UIInfoModel : NormalSingleTon<UIInfoModel>, IInit
{
    private Dictionary<int, List<string>> _gamePlaceNameDic;
    private List<string> _gameLevelName;
    private List<Sprite> _loadingPanelBgSprite;
    private List<string> _loadingPanelTips;
    private Dictionary<E_GameState, Sprite> _gameEndPanelSpriteDic;//游戏结束面板
    public int CurLookBackLevelId { get; set; }   //当前回忆面板所选择的场景Id
    public KeyValuePair<int, int> CurPlace { get; set; }//当前玩家所在的ArchivePoint
    private Dictionary<int, NPCData> _npcDataDic;//当前的NPC数据
    public int CurNpcIndex { get; set; }//当前在对话的NPC索引
    public int CurNpcModelIndex { get; set; }//当前对话NPC model的索引
    public Items _items;//物品信息集合
    public ShopDatas _shopDatas;//商店信息集合
    public int CurItemId { get; set; }//当前所选择的物品的id

    public int CurShopId { get; set; }//对应的 当前所选择的的id
    public void Init()
    {
        InitLoadingPanelBgSprites();
        InitLoadingPanelTips(Paths.CFG_LOADINGPANELTIPS);
        InitGamePlaceNameInfo(Paths.CFG_GAMEPLACENAME);
        InitGameLevelName(Paths.CFG_GAMELEVELNAME);
        InitNPCData(Paths.DATA_NPCDATA_FOLDER);
        InitItemData(Paths.DATA_ITEMDATA);
        InitShopDatas(Paths.DATA_SHOPDATA);
        InitGameEndSprite();
        CurNpcIndex = -1;
        CurItemId = -1;
        CurShopId = -1;
    }
    private void InitLoadingPanelBgSprites()
    {
        _loadingPanelBgSprite = new List<Sprite>();
        for (int i = 0; i < 6; i++)
            _loadingPanelBgSprite.Add(ResMgr.Instance.LoadRes<Sprite>($"{Paths.PICTURE_NORMAL_FOLDER}{Consts.PREFIX_LOADINGPANEL_BG}{i}"));
    }
    private void InitLoadingPanelTips(string path)
    {
        _loadingPanelTips = new List<string>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(path));
        //解析Json
        foreach (JsonData data in jsonData)
        {
            string content = data["content"].ToString();
            _loadingPanelTips.Add(content);
        }
    }
    private void InitGamePlaceNameInfo(string path)
    {
        _gamePlaceNameDic = new Dictionary<int, List<string>>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(path));
        //解析Json
        foreach (JsonData data in jsonData)
        {
            string content = data["content"].ToString();
            var strs = content.Split('|');
            int sceneIndex = int.Parse(strs[0]);
            if (!_gamePlaceNameDic.ContainsKey(sceneIndex))
                _gamePlaceNameDic[sceneIndex] = new List<string>();
            _gamePlaceNameDic[sceneIndex].Add(strs[2]);
        }
    }
    private void InitGameEndSprite()
    {
        _gameEndPanelSpriteDic = new Dictionary<E_GameState, Sprite>();
        for (E_GameState i = E_GameState.Lose; i < E_GameState.COUNT; i++)
            _gameEndPanelSpriteDic[i] = ResMgr.Instance.LoadRes<Sprite>($"{Paths.PICTURE_NORMAL_FOLDER}Game{i.ToString()}");
    }
    private void InitGameLevelName(string path)
    {
        _gameLevelName = new List<string>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(path));
        //解析Json
        foreach (JsonData data in jsonData)
        {
            string content = data["content"].ToString();
            _gameLevelName.Add(content);
        }
    }
    private void InitNPCData(string path)
    {
        _npcDataDic = new Dictionary<int, NPCData>();
        var npcsData = ResMgr.Instance.LoadAllRes<NPCData>(path);
        foreach (var data in npcsData)
            _npcDataDic[data.id] = data;
    }
    private void InitItemData(string path)
        => _items = ResMgr.Instance.LoadRes<Items>(path);
    private void InitShopDatas(string path)
        => _shopDatas = ResMgr.Instance.LoadRes<ShopDatas>(path);
    //得到物品信息
    public Item GetItemData(int index)
    {
        if (_items.items.Count > index)
            return _items.items[index];
        return null;
    }
    public Item GetCurSelectItemData()
        => GetItemData(CurItemId);
    public int GetItemId(Item item) => _items.items.IndexOf(item);
    public string GetGamePlaceName(int sceneIndex, int placeIndex)
    {
        if (_gamePlaceNameDic.TryGetValue(sceneIndex, out List<string> placeList))
        {
            if (placeList.Count > placeIndex)
                return placeList[placeIndex];
            else
                Debug.LogError($"索引为{sceneIndex}的游戏场景不存在索引为:{placeIndex}的地名");
        }
        else
            Debug.LogError($"索引为{sceneIndex}的游戏场景不存在");
        return string.Empty;
    }
    public string GetGameLevelName(int levelIndex)
    {
        if (levelIndex < _gameLevelName.Count)
            return _gameLevelName[levelIndex];
        Debug.LogError($"该索引游戏场景索引:{levelIndex}不正确");
        return string.Empty;
    }
    public Sprite GetLoadingSprite(int index = -1)
    {
        Sprite sp = null;
        if (index < 0 || index > _loadingPanelBgSprite.Count - 1)
            index = Random.Range(0, _loadingPanelBgSprite.Count);
        sp = _loadingPanelBgSprite[index];
        return sp;
    }
    public string GetLoadingTips(int index = -1)
    {
        string str = "";
        if (index < 0 || index > _loadingPanelTips.Count - 1)
            index = Random.Range(0, _loadingPanelTips.Count);
        str = _loadingPanelTips[index];
        return str;
    }
    public Sprite GetGameEndPanelSprite(E_GameState state)
    {
        if (!_gameEndPanelSpriteDic.TryGetValue(state, out Sprite sprite))
            Debug.LogError($"该状态:{state}不存在对应的sprite");
        return sprite;
    }
    public string[] GetTwoDifferentTip()
    {
        if (_loadingPanelTips.Count < 2)
        {
            Debug.LogError($"没有可用的两条提示信息");
            return null;
        }
        string[] strs = new string[2];
        var _intHs = new HashSet<int>();
        while (_intHs.Count < 2)
        {
            var index = Random.Range(0, _loadingPanelTips.Count);
            _intHs.Add(index);
        }
        int strIndex = 0;
        foreach (var item in _intHs)
        {
            strs[strIndex] = _loadingPanelTips[item];
            strIndex++;
        }
        return strs;
    }
    //得到当前NPC的名字
    public string GetCurNpcName()
    {
        if (_npcDataDic.TryGetValue(CurNpcIndex, out NPCData npcData))
            return npcData.name;
        else
            Debug.LogError($"id为:{CurNpcIndex}的NPC不存在");
        return null;
    }
    //得到当前对话模型
    public ChatModel GetCurChatModel()
        => GetNPCData(CurNpcIndex, CurNpcModelIndex);
    public ChatModel GetNPCData(int id, int index)
    {
        if (_npcDataDic.TryGetValue(id, out NPCData npcData))
        {
            if (npcData.dialogModels.Count > index)
                return npcData.dialogModels[index];
            else
                Debug.LogError($"id为:{id}的NPC不存在索引为:{index}的对话数据");
        }
        else
            Debug.LogError($"id为:{id}的NPC不存在");
        return null;
    }
    //得到一个商店的所有商品信息
    public OneShopDatas GetOneShopDatasById(int id)
    {
        if (_shopDatas.shopsData.Count > id)
            return _shopDatas.shopsData[id];
        Debug.LogError($"当前id：{id}对应的商店信息不存在");
        return null;
    }
    public OneShopDatas GetCurSelectedShopDatas()
        => GetOneShopDatasById(CurShopId);
    //转换当前对话事件
    public void ParseChatEvent(E_ChatEvent chatEvent, string args)
    {
        switch (chatEvent)
        {
            case E_ChatEvent.NextChat:
                NextChatEvent();
                break;
            case E_ChatEvent.ExitChat:
                ExitChatEvent();
                break;
            case E_ChatEvent.JumpChat:
                JumpChatEvent(int.Parse(args));
                break;
            case E_ChatEvent.StartShop:
                int shopId = int.Parse(args);
                CurShopId = shopId;
                UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_SHOPPANEL);
                break;
            case E_ChatEvent.ShowDialog:
                int dialog = int.Parse(args);
                switch(dialog)
                {
                    //使用淬炼武器
                    case 0:
                        if (GameDataModel.Instance.CheckCanQueching())
                            DialogMgr.ShowDialog("提示", "当前攻击力得到了提升！");
                        else
                        {
                            DialogMgr.ShowDialog("淬剑石不够！".SetColor(E_TextColor.Red), "至少需要4块淬剑石");
                            AudioMgr.Instance.PlayOnce("error");
                        }
                        break;
                    case 1:
                        //提升魔法值和体力
                        if (GameDataModel.Instance.CheckCanStrength())
                            DialogMgr.ShowDialog("提示", "当前体力和魔法值得到了提升！");
                        else
                        {
                            DialogMgr.ShowDialog("灵魂护符不够！".SetColor(E_TextColor.Red), "至少需要4枚灵魂护符");
                            AudioMgr.Instance.PlayOnce("error");
                        }
                        break;
                }
                break;
        }
    }
    //执行当前NPC的所有事件
    public void ExcuteCurNpcModelEvent()
    {
        var model = GetCurChatModel();
        foreach (var item in model.dialogEventModels)
            ParseChatEvent(item.chatEvent, item.args);
    }
    //执行当前指定选项玩家的所有事件
    public void ExcuteSelectPlayerModelEvent(ChatPlayerSelect playerSelect)
    {
        foreach (var item in playerSelect.dialogEventModels)
            ParseChatEvent(item.chatEvent, item.args);
    }
    private void NextChatEvent()
        => CurNpcModelIndex++;
    private void JumpChatEvent(int index)
        => CurNpcModelIndex = index;
    private void ExitChatEvent()
    {
        EventMgr.Instance.ExecuteEvent(E_EventName.CHAT_OVER);
        CurNpcModelIndex = 0;
        CurNpcIndex = -1;
    }
    //得到当前关卡的名字
    public string GetCurLevelName()
    {
        var curLevelIndex = GameStateModel.Instance.CurLevelIndex;
        return GetGameLevelName(curLevelIndex);
    }
    //设置当前回忆面板的id
    public void InitCurLookBackLevelId(int id)
        => CurLookBackLevelId = id;
}