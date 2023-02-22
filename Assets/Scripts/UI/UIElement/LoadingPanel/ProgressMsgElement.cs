/****************************************************
    文件：ProgressMsgElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/29 22:10:39
	功能：LoadinPanel下面的游戏进度条管理
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMsgElement : UIElement
{
    private float _heroHegiht;
    private float _progressWidth;
    public override void InitChild()
    {
        _heroHegiht = GetControl<Image>("img_Hero").GetAnchorPos().y;
        _progressWidth = GetControl<Image>("img_Progress").rectTransform.sizeDelta.x;
    }
    public override void Show()
    {
        base.Show();
        GetControl<Image>("img_Progress").fillAmount = 0f;
        GetControl<Image>("img_Hero").SetAnchorPos(new Vector2(0, _heroHegiht));
    }
    public override void Refresh()
    {
        base.Refresh();
        UpdateProgress();
    }
    private void UpdateProgress()
    {
        float progress = SceneMgr.Instance.Process();
        GetControl<Image>("img_Progress").fillAmount=progress;
        GetControl<Image>("img_Hero").SetAnchorPos(new Vector2(_progressWidth*progress,_heroHegiht));
    }
}