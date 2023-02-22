/****************************************************
    文件：UIEventUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/30 12:23:57
	功能：UI事件注册工具
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UIEventUtil : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDropHandler,IPointerClickHandler
{
    public event Action<PointerEventData> OnEnterEvent;
    public event Action<PointerEventData> OnExitEvent;
    public event Action<PointerEventData> OnDragEvent;
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    public event Action<PointerEventData> OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
        => OnClickEvent?.Invoke(eventData);



    public void OnBeginDrag(PointerEventData eventData)
        => OnBeginDragEvent?.Invoke(eventData);
    public void OnDrop(PointerEventData eventData)
        => OnDragEvent?.Invoke(eventData);
    public void OnEndDrag(PointerEventData eventData)
        => OnEndDragEvent?.Invoke(eventData);

 
    public void OnPointerEnter(PointerEventData eventData)
        => OnEnterEvent?.Invoke(eventData);
    public void OnPointerExit(PointerEventData eventData)
        => OnExitEvent?.Invoke(eventData);
}