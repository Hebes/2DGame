/****************************************************
    文件：Stab.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/6 18:45:19
	功能：游戏中的地刺脚本
*****************************************************/
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stab : MonoBehaviour
{
    [Required,SerializeField]
    private StabData _data;
    private Core Core { get; set; }
    private SpriteRenderer SpriteRender { get; set; }
    protected SubEventMgr SubEventMgr { get; set; }
    private void Awake()
    {
        InitComponent();
        AddEvent();
    }
    private void OnDestroy()
    {
        RemoveEvent();
    }
    private void InitComponent()
    {
        SubEventMgr = gameObject.AddOrGet<SubEventMgr>();
        SpriteRender = Get<SpriteRenderer>();
        Core = Get<Core>("Core");
        Core.Init();
    }
    private void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.STAB_CHANGESPROTE,ChangeBloodSprite);
    }
    private void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.STAB_CHANGESPROTE, ChangeBloodSprite);
    }
    private X Get<X>() where X : Component
    {
        X component = GetComponent<X>();
        if (component == null)
            Debug.LogError($"Bullet物体:{name}身上不存在该组件:{typeof(X)}");
        return component;
    }
    private X Get<X>(string path) where X : Component
    {
        X component = transform.Find(path).GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}的:{path}路径下为未找到该组件:{typeof(X)}");
        return component;
    }
    private void ChangeBloodSprite(params object[] args)
        => SpriteRender.sprite = _data.bloodSprire;
}