/****************************************************
    文件：GameTipElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/29 22:10:26
	功能：LoadingPanel下面的游戏提示语
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTipsElement : UIElement
{
    private string[] _tips;
    public override void InitChild()
    {
        GetControl<Button>("btn_Next").onClick.AddListener(()=>
        {
            UpdateTips(_tips[1]);
            GetControl<Button>("btn_Next").SetActive(false);
        });
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Next", E_UIMusic.None);
    }
    public override void Show()
    {
        base.Show();
        _tips = UIInfoModel.Instance.GetTwoDifferentTip();
        GetControl<Button>("btn_Next").SetActive(true);
        UpdateTips(_tips[0]);
    }
    private void UpdateTips(string content)
    {
        var strs = content.Split('|');
        var title = strs[0];
        var tip = strs[1];
        GetControl<Text>("txt_Title").SetText(title);
        GetControl<Text>("txt_Tip").SetText(tip);
    }
}