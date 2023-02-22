/****************************************************
    文件：ArchiveItemElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/10 19:34:36
	功能：ChooseGameArchivePanel下面的存档选择集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArchiveItemElement : UIElement
{
    private static int id;
    private static int SetId() => id++;
    private int index;//当前位置对应的索引
    private GameArchive _gameArchive;//对应的
    private bool _isDetectingArchive;//是否正在删除当前存档
    public override void InitChild()
    {
        index = SetId();
        InitClickAction();
    }
    private void Start()
    {
        ConvertButtonMusic("btn_NotClear",E_UIMusic.NotClick);
    }
    private void InitClickAction()
    {
        this.transform.Find("enterImg").AddOrGet<UIEventUtil>().OnClickEvent += (data) => OnClickAction();
        GetControl<Button>("btn_Clear").SetEvent(() => _isDetectingArchive = true);
        GetControl<Button>("btn_NotClear").SetEvent(() => _isDetectingArchive = false);
        GetControl<Button>("btn_SureClear").SetEvent(() => 
        {
            _isDetectingArchive = false;
            GameDataMgr.Instance.ClearSelectIndexArchive(index);
        });
    }
    public override void Show()
    {
        base.Show();
        _isDetectingArchive = false;
    }
    public override void Refresh()
    {
        base.Refresh();
        RefreshArchiveInfo();
    }
    //刷新游戏存档信息
    private void RefreshArchiveInfo()
    {
        _gameArchive = GameDataMgr.Instance.GetSelectIndexGameArchive(index);
        //如果没有该索引对应的存档信息
        GetControl<Text>("txt_GameMsg").SetActive(true);
        GetControl<Button>("btn_SureClear").SetActive(false);
        GetControl<Button>("btn_NotClear").SetActive(false);
        if (_gameArchive == null)
        {
            GetControl<Text>("txt_GameMsg").SetText("新游戏");
            GetControl<Text>("txt_Icon").SetActive(false);
            GetControl<Text>("txt_LastPlaceName").SetActive(false);
            GetControl<Text>("txt_Time").SetActive(false);
            GetControl<Button>("btn_Clear").SetActive(false);
        }
        //如果存档该索引对应的存档信息
        else
        {
            if (!_isDetectingArchive)
            {
                var setting = _gameArchive.heroManSettingData;
                var msg =
                    $"攻击力：{setting.attack.ToString().SetColor(E_TextColor.Red)}\n" +
                    $"生    命：{setting.maxHealth.ToString().SetColor(E_TextColor.Green)}\n" +
                    $"恢复力：{setting.lightAddRatio.ToString().SetColor(E_TextColor.Yellow)}\n" +
                    $"魔法值：{setting.maxMagicPower.ToString().SetColor(E_TextColor.Blue)}";
                GetControl<Text>("txt_GameMsg").SetText(msg);
                GetControl<Text>("txt_Icon").SetActive(true);
                GetControl<Text>("txt_Icon").SetText(_gameArchive.coin);
                GetControl<Text>("txt_LastPlaceName").SetActive(true);
                var strs = _gameArchive.lastArchivePos.Split('|');
                var placeName = UIInfoModel.Instance.GetGamePlaceName(int.Parse(strs[0]), int.Parse(strs[1]));
                GetControl<Text>("txt_LastPlaceName").SetText(placeName);
                GetControl<Text>("txt_Time").SetActive(true);
                GetControl<Text>("txt_Time").SetText(_gameArchive.time);
                GetControl<Button>("btn_Clear").SetActive(true);
            }
            else
            {
                GetControl<Button>("btn_NotClear").SetActive(true);
                GetControl<Button>("btn_SureClear").SetActive(true);
                GetControl<Text>("txt_GameMsg").SetText("清除存档?");
                GetControl<Text>("txt_Icon").SetActive(false);
                GetControl<Text>("txt_LastPlaceName").SetActive(false);
                GetControl<Text>("txt_Time").SetActive(false);
                GetControl<Button>("btn_Clear").SetActive(false);
            }
        }
    }
    private void OnClickAction()
    {
        //创建默认新数据
        if (_gameArchive == null)
            _gameArchive = GameDataMgr.Instance.CreateNewDefaultArchive();
        GameDataModel.Instance.SetCurGameArchive(_gameArchive);
        AudioMgr.Instance.PlayUIMusic(E_UIMusic.ChooseArchive);
        EnterGameScene(_gameArchive);
    }
    //进入游戏场景
    private void EnterGameScene(GameArchive archive)
    {
        var strs = archive.lastArchivePos.Split('|');
        int levelIndex = int.Parse(strs[0]);
        switch (levelIndex) 
        {
            case 0:
                GameStateModel.Instance.TargetScene = E_SceneName.LevelOne;
                break;
            case 1:
                GameStateModel.Instance.TargetScene = E_SceneName.LevelTwo;
                break;
        }
        UIInfoModel.Instance.InitCurLookBackLevelId(levelIndex);
        UIManager.Instance.HidePanel(Paths.PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL);
        UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
    }
}