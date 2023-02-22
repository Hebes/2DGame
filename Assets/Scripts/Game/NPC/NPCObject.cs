/****************************************************
    文件：NPCObject.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/19 16:34:22
	功能：NPC对象脚本
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NPCObject : MonoBehaviour 
{
    [SerializeField]
    private int npcId;
    private TipCanvas _tipCanvas;//活动显示UI
    private float _animTime = 1f;
    private float _startY = 4f;
    private float _endY = 4.3f;
    private bool _canCheckInput = false;
    private CanvasGroup _canvasGroup;
    private float _fadeAnimTime = 0.5f;
    private List<string> _audioClip=new List<string>()
    {
        "npc_adventure",
        "npc_merchant",
        "npc_blacksmith",
        "npc_witch",
        "npc_beggar",
        "npc_barmaid",
        "npc_shadyguy"
    };
    private void Awake()
    {
        _tipCanvas = transform.Find("TipCanvas").Add<TipCanvas>();
        _tipCanvas.Init(_animTime, _startY, _endY, _fadeAnimTime);
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
    private void Update()
    {
        //进入篝火范围 且 主角处于可以进行交互的状态
        if (!_canCheckInput || !GameStateModel.Instance.CanInteractive)
            return;
        if (UIManager.Instance.GetPanelActive(Paths.PREFAB_UIPANEL_CHATPANEL))
            return;
        if (InputMgr.Instance.GetAxisRaw(Consts.VERTICALAXIS) == 1f)
        {
            UIInfoModel.Instance.CurNpcIndex = npcId;
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_CHATPANEL);
            _tipCanvas.SetTipCanvasActive(false);
            PlayAudio();
        }
    }
    private void PlayAudio()
        => AudioMgr.Instance.PlayOnce(_audioClip[npcId]);
}