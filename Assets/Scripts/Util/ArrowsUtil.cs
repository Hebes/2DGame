/****************************************************
    文件：ArrowUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 14:19:19
	功能：UI箭头工具管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class ArrowsUtil : MonoBehaviour
{
    private int _arrowsNum;
    private int _curIndex=-1;
    public void Init()
    {
        //遍历所有子物体
        _arrowsNum = 0;
        foreach (Transform trans in transform)
            if (trans.name.StartsWith(Consts.PREFIX_UIITEM_ARROW))
                _arrowsNum++;
        if(_arrowsNum<=0)
            Debug.LogError($"该物体:{transform.name}身上没有箭头");
    }
    public void ChangeActiveArrows(int index)
    {
        if (index < 0 || index >= _arrowsNum)
        {
            Debug.LogError($"名为{transform.name}的物体使用了错误的箭头索引{index}");
            return;
        }
        if (_curIndex == index)
            return;
        _curIndex = index;
        transform.Find($"{Consts.PREFIX_UIITEM_ARROW}{index}").SetActive(true);
        for (int i = 0; i < _arrowsNum; i++)
        {
            if (i == index)
                continue;
            transform.Find($"{Consts.PREFIX_UIITEM_ARROW}{i}").SetActive(false);
        }
        AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
    }
    public void CloseAllArrows()
    {
            for (int i = 0; i < _arrowsNum; i++)
                transform.Find($"{Consts.PREFIX_UIITEM_ARROW}{i}").SetActive(false);
    }
}