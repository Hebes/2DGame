/****************************************************
    文件：LookBackElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 16:49:42
	功能: ArchivePoint面板下管理的UI子集
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LookBackElement : UIElement
{
    public override void InitChild()
    {
        transform.Find("GameLevelItemsElement").Add<GameLevelItemsElement>();
        transform.Find("GamePlaceItemsElement").Add<GamePlaceItemsElement>();
        GetControl<Button>("btn_Quit").SetEvent(()=>Hide());
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>()
            .OnEnterEvent += (data) => GetControl<Button>("btn_Quit").transform.Find("Arrow").SetActive(true);
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>()
           .OnExitEvent += (data) => GetControl<Button>("btn_Quit").transform.Find("Arrow").SetActive(false);
    }
    public override void Show()
    {
        base.Show();
        GetControl<Button>("btn_Quit").transform.Find("Arrow").SetActive(false);
    }
}