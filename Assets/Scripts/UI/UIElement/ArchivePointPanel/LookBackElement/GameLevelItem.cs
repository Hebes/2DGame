/****************************************************
    文件：GameLevelItem.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 17:45:47
	功能：GameLevelItemsElement下面的单个关卡名管理集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLevelItem : UIElement
{
    private static Vector2 _offset = new Vector2(-480f,310f);
    private static int _id=-1;
    private static void AddId() => _id++;
    private int _levelIndex;
    public override void InitChild()
    {

    }
    private void Awake()
    {
        AddId();
        SetPos();
        GetControl<Button>("btn_SelectLevel").SetEvent(() => UIInfoModel.Instance.CurLookBackLevelId = _levelIndex);
    }
    public void Init(int id)
        => _levelIndex = id;
    private void SetPos()
    {
        var rect = transform.Rect();
        float height = rect.rect.height;
        var pos = _offset+new Vector2(0,-height*_id);
        rect.anchoredPosition = pos;
    }
    public override void Show()
    {
        base.Show();
        GetControl<Text>("txt_LevelName").SetText(UIInfoModel.Instance.GetGameLevelName(_levelIndex));
    }
    public override void Refresh()
    {
        base.Refresh();
        GetControl<Image>("img_Arrow").SetActive(_levelIndex == UIInfoModel.Instance.CurLookBackLevelId);
    }
}