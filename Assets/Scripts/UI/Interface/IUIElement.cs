/****************************************************
    文件：IUIElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 11:12:44
	功能：基础UI集合接口
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IUIElement :IUIInit,IUIShow,IUIHide,IUIRefresh
{
    Transform GetTrans();
    void Reacquire();//再次获取子类身上的接口
}