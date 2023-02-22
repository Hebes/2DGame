/****************************************************
    文件：TreasureBox.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/24 15:39:8
	功能：游戏中的宝箱
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TreasureBox : MonoBehaviour
{
    [Header("宝箱属性设置")]
    [SerializeField]
    private int _level;
    [SerializeField]
    private int _boxId;
    [Header("道具属性设置")]
    [SerializeField]
    private int _itemId;
    [SerializeField]
    private int _num = 1;
    private float _animTime = 1f;
    private float _startY = 3.5f;
    private float _endY = 3.8f;
    private float _fadeAnimTime = 0.5f;
    private bool _canCheckInput = false;
    private TipCanvas _tipCanvas;
    private Animator _anim;
    private const string _openAnimName = "Open";
    private const string _closeAnimName = "Close";
    private bool _isOpen = false;//箱子是否已经被打开
    private void Awake()
    {
        _isOpen = GameDataModel.Instance.GetTreasureboxUnlockState(_level, _boxId);
        _tipCanvas = transform.Find("TipCanvas").Add<TipCanvas>();
        _tipCanvas.Init(_animTime, _startY, _endY, _fadeAnimTime);
        _tipCanvas.SetTipCanvasActive(false);
        GetComponent<Collider2D>().enabled = !_isOpen;
        _anim = GetComponent<Animator>();
        if(_isOpen)
        _anim.Play(_openAnimName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            _tipCanvas.SetTipCanvasActive(true);
            _canCheckInput = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            _tipCanvas.SetTipCanvasActive(false);
            _canCheckInput = false;
        }
    }
    private void Update()
    {
        if (_isOpen)
            return;
        //进入篝火范围 且 主角处于可以进行交互的状态
        if (!_canCheckInput || !GameStateModel.Instance.CanInteractive)
            return;
        if (InputMgr.Instance.GetAxisRaw(Consts.VERTICALAXIS) == 1f)
        {
            _anim.Play(_openAnimName);
            _tipCanvas.SetTipCanvasActive(false);
            GetComponent<Collider2D>().enabled = false;
            _canCheckInput = false;
        }
    }
    //打开完成
    private void OpenOver()
    {
        if (_isOpen)
            return;
        _isOpen = true;
        AudioMgr.Instance.Play("hero_getItem");
        GameDataModel.Instance.AddOrReduceItem(_itemId,_num);
        DialogMgr.ShowGetItemDialogPanel(_itemId, _num,()=>EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_GETITEM));
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESGAMEPANELCURITEMINFO);
        GameDataModel.Instance.UnlockLevelTreasurebox(_level,_boxId);
    }
}