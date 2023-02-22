/****************************************************
    文件：QuitPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/30 21:22:0
	功能：对话框面板
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : PanelBase
{
    public void InitDialog(string title,string content,Action trueAction=null,Action falseAction=null)
    {
        SetTitle(title);
        SetContent(content);
        AddButtonAction(trueAction, falseAction);
    }
    public void InitDialog(string content, Action trueAction = null, Action falseAction = null)
    {
        SetTitle("提示");
        SetContent(content);
        AddButtonAction(trueAction, falseAction);
    }
    public void InitDialog(string title, string content,string sureButtonText,string notSureButtonText, Action trueAction = null, Action falseAction = null)
    {
        SetTitle(title);
        SetContent(content);
        SetSureBtnText(sureButtonText);
        SetNotSureBtnText(notSureButtonText);
        AddButtonAction(trueAction, falseAction);
    }
    public void InitDialog(string content, string sureButtonText, string notSureButtonText, Action trueAction = null, Action falseAction = null)
    {
        SetTitle("提示");
        SetContent(content);
        SetSureBtnText(sureButtonText);
        SetNotSureBtnText(notSureButtonText);
        AddButtonAction(trueAction, falseAction);
    }
    private void SetTitle(string title)
        => GetControl<Text>("txt_Title").SetText(title);
    private void SetContent(string content)
        => GetControl<Text>("txt_Content").SetText(content);
    private void SetSureBtnText(string text)
    {
        GetControl<Button>("btn_Sure")
            .transform.Find("txt_SureText")
            .GetComponent<Text>().SetText(text);
    }
    private void SetNotSureBtnText(string text)
    {
        GetControl<Button>("btn_NotSure")
         .transform.Find("txt_NotSureText")
         .GetComponent<Text>().SetText(text);
    }
    private void AddButtonAction(Action trueAction,Action fasleAction)
    {
        if (trueAction == null && fasleAction == null)
        {
            ChangeButtonState(true);
            AddOneButtonAction(trueAction);
        }
        else if (trueAction != null && fasleAction == null)
        {
            ChangeButtonState(true);
            AddOneButtonAction(trueAction);
        }
        else if (trueAction == null && fasleAction != null)
        {
            ChangeButtonState(true);
            AddOneButtonAction(trueAction);
            Debug.LogError($"当确定的按钮的事件为空的时候，否定按钮不能不为空");
        }
        else
        {
            ChangeButtonState(false);
            AddTwoButtonAction(trueAction,fasleAction);
        }
    }
    private void ChangeButtonState(bool isOne)
        => GetControl<Button>("btn_NotSure").SetActive(!isOne);
    private void AddOneButtonAction(Action trueAction)
    {
        GetControl<Button>("btn_Sure").onClick.AddListener(()=>trueAction?.Invoke());
        GetControl<Button>("btn_Sure").onClick.AddListener(Hide);
    }
    private void AddTwoButtonAction(Action trueAction,Action falseAction)
    {
        GetControl<Button>("btn_Sure").onClick.AddListener(() => trueAction?.Invoke());
        GetControl<Button>("btn_Sure").onClick.AddListener(Hide);
        GetControl<Button>("btn_NotSure").onClick.AddListener(() => falseAction?.Invoke());
        GetControl<Button>("btn_NotSure").onClick.AddListener(Hide);
        ConvertButtonMusic("btn_NotSure",E_UIMusic.NotClick);
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_Sure").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => 
        {
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowOne").SetActive(true);
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowTwo").SetActive(true);
        };
        GetControl<Button>("btn_Sure").AddOrGet<UIEventUtil>().OnExitEvent += (data) =>
        {
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowOne").SetActive(false);
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowTwo").SetActive(false);
        };
        GetControl<Button>("btn_NotSure").AddOrGet<UIEventUtil>().OnEnterEvent += (data) =>
        {
            GetControl<Button>("btn_NotSure").transform.Find("txt_NotSureText/arrowOne").SetActive(true);
            GetControl<Button>("btn_NotSure").transform.Find("txt_NotSureText/arrowTwo").SetActive(true);
        };
        GetControl<Button>("btn_NotSure").AddOrGet<UIEventUtil>().OnExitEvent += (data) =>
        {
            GetControl<Button>("btn_NotSure").transform.Find("txt_NotSureText/arrowOne").SetActive(false);
            GetControl<Button>("btn_NotSure").transform.Find("txt_NotSureText/arrowTwo").SetActive(false);
        };
    }
    public override void Hide()
    {
        base.Hide();
        Destroy(gameObject);
    }
    public override void InitChild()
    {
        InitBtnEnterEvent();
    }
}