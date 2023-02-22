/****************************************************
    文件：UIElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 11:11:53
	功能：UI管理集合
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIElement : MonoBehaviour, IUIElement
{
    private float _startApha = 0f;
    private float _animTime = 0.3f;
    private CanvasGroup _canvasGroup;
    private HashSet<IUIInit> _initHs;
    private HashSet<IUIShow> _showHs;
    private HashSet<IUIHide> _hideHs;
    private HashSet<IUIRefresh> _refreshHs;
    private UIUtil _uitil;
    protected UIUtil UIUtil
    {
        get
        {
            if (_uitil == null)
            {
                _uitil = gameObject.AddOrGet<UIUtil>();
                _uitil.Init();
            }
            return _uitil;
        }
    }
    private ArrowsUtil _arrowsUtil;
    protected ArrowsUtil ArrowsUtil
    {
        get
        {
            if (_arrowsUtil == null)
            {
                _arrowsUtil = gameObject.AddOrGet<ArrowsUtil>();
                _arrowsUtil.Init();
            }
            return _arrowsUtil;
        }
    }
    private AudioMgr _audioMgr;
    protected AudioMgr AudioMgr
    {
        get
        {
            if (_audioMgr == null)
                _audioMgr = AudioMgr.Instance;
            return _audioMgr;
        }
    }
    public abstract void InitChild();
    public virtual void Init()
    {
        InitChild();
        InitChildItemInterface();
        InitAllChildItem();
        InitCanvasGroup();
    }
    protected virtual void OnDestroy()
    {
        _canvasGroup.DOKill();
    }
    public virtual void Show()
    {
        SetCanvasInteractable(true);
        _canvasGroup.alpha = _startApha;
        SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1,_animTime)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
        foreach (var show in _showHs)
            show.Show();
        //每次显示的时候都进行刷新一下
        Refresh();
    }
    private void InitCanvasGroup()
      => _canvasGroup = gameObject.AddOrGet<CanvasGroup>();
    public virtual void Hide()
    {
        foreach (var hide in _hideHs)
            hide.Hide();
        SetCanvasInteractable(false);
        _canvasGroup.DOKill();
        //_canvasGroup.alpha = 1f;
        _canvasGroup.DOFade(_startApha, _animTime)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() => SetActive(false));
    }
    public virtual void Refresh()
    {
        foreach (var refresh in _refreshHs)
            refresh.Refresh();
    }
    private void InitChildItemInterface()
    {
        _initHs = new HashSet<IUIInit>();
        _showHs = new HashSet<IUIShow>();
        _hideHs = new HashSet<IUIHide>();
        _refreshHs = new HashSet<IUIRefresh>();
        GetAllChildItemInterface();
    }
    private void GetAllChildItemInterface()
    {
        GetChildItemInterface(_initHs);
        GetChildItemInterface(_showHs);
        GetChildItemInterface(_hideHs);
        GetChildItemInterface(_refreshHs);
    }
    private void GetChildItemInterface<T>(HashSet<T> hs)
    {
        T temp;
        foreach (Transform trans in transform)
        {
            temp = trans.GetComponent<T>();
            if (temp != null)
                hs.Add(temp);
        }
    }
    //通过初始化接口对子物体进行初始化
    private void InitAllChildItem()
    {
        foreach (var init in _initHs)
            init.Init();
    }
    public Transform GetTrans() => transform;
    public virtual void Reacquire()
    {
        GetAllChildItemInterface();
        //再次获取之后需要重新执行初始化函数
        InitAllChildItem();
        //重新绑定所有按钮的Button函数
        transform.GetComponentInParent<PanelBase>()?.InitRefreshAction();
    }
    private void SetActive(bool value)
        => gameObject.SetActive(value);
    public bool GetActive()
        => gameObject.activeSelf;
    protected T GetControl<T>(string name) where T : UIBehaviour
        => UIUtil.GetControl<T>(name);
    //设置交互状态
    private void SetCanvasInteractable(bool value)
        => _canvasGroup.interactable = value;
    public void ConvertButtonMusic(string buttonName, E_UIMusic type)
    {
        Button btn = GetControl<Button>(buttonName);
        transform.GetComponentInParent<PanelBase>()?.ChangeButtonMusic(btn, type);
    }
    public void ConvertToggleMusic(string toggleName, E_UIMusic type)
    {
        Toggle tgl = GetControl<Toggle>(toggleName);
        transform.GetComponentInParent<PanelBase>()?.ChangeToggleMusic(tgl, type);
    }
}