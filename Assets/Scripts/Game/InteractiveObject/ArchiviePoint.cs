/****************************************************
    文件：ArchiviePoint.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/11 19:21:51
	功能：游戏存档点
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ArchiviePoint : MonoBehaviour
{
    [Header("设置"), SerializeField]
    private int sceneIndex;
    [SerializeField]
    private int placeId;
    private TipCanvas _tipCanvas;
    private float _animTime = 1f;
    private float _startY = 3f;
    private float _endY = 3.3f;
    private bool _canCheckInput = false;
    private CanvasGroup _canvasGroup;
    private float _fadeAnimTime=0.5f;
    private void Awake()
    {
        _tipCanvas = transform.Find("TipCanvas").Add<TipCanvas>();
        _tipCanvas.Init(_animTime,_startY,_endY,_fadeAnimTime);
        _tipCanvas.SetTipCanvasActive(false);
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
    //得到地点的id
    public int GetPlaceId()
        => placeId;
    //得到重生点的位置
    public Vector3 GetResetPos()
        => transform.Find("ResetPos").position;
    private void Update()
    {
        //进入篝火范围 且 主角处于可以进行交互的状态
        if (!_canCheckInput ||!GameStateModel.Instance.CanInteractive)
            return;
        if (UIManager.Instance.GetPanelActive(Paths.PREFAB_UIPANEL_ARCHIVEPOINTPANEL))
            return;
        if (InputMgr.Instance.GetAxisRaw(Consts.VERTICALAXIS) == 1f)
        {
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_ARCHIVEPOINTPANEL);
            GameDataModel.Instance.UnlockScenePlace(sceneIndex, placeId);
            _tipCanvas.SetTipCanvasActive(false);
            UIInfoModel.Instance.CurPlace = new KeyValuePair<int, int>(sceneIndex,placeId);
        }
    }
}