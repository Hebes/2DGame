using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class DestructibleItemBase : MonoBehaviour,IDestructibleItem
{
    [SerializeField,InlineEditor]
    protected DestructibleItemBaseData _data;
    protected Core Core { get; private set; }
    protected SubEventMgr SubEventMgr { get; private set; }
    protected Animator Anim { get; private set; }
    protected SpriteRenderer SpriteRender { get; private set; }
    private HashSet<string> _audioClips = new HashSet<string>()
    {
        "destructibleItem_barrol",
        "destructibleItem_bottle",
        "destructibleItem_box",
        "destructibleItem_indicator_signpost",
        "destructibleItem_treasureBox",
    };
    private string _brokenClip = "destructibleItem_woodBroken";//木质破碎的音效
    protected string _deadAudio="";
    protected virtual void Awake()
    {
        InitComponent();
        AddEvent();
        InitDeadAudio();
    }
    protected virtual void OnDestroy()
    {
        transform.DOKill();
        RemoveEvent();
    }
    protected virtual void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.CHARACTER_HIT, Hurt);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DEAD, Dead);
    }
    protected virtual void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_HIT, Hurt);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DEAD, Dead);
    }
    protected void InitComponent()
    {

        Core = transform.Find("Core").GetComponent<Core>();
        if (Core == null)
        {
            Debug.LogError($"当前Bullet物体:{name}不存在Core组件");
            return;
        }
        Core.Init();
        SubEventMgr = transform.AddOrGet<SubEventMgr>();
        Anim = transform.GetComponent<Animator>();
        if (Anim == null)
        {
            Debug.LogError($"当前Bullet物体:{name}不存在Anim组件");
            return;
        }
        SpriteRender = GetComponent<SpriteRenderer>();
        if (SpriteRender == null)
        {
            Debug.LogError($"当前Bullet物体:{name}不存在SpriteRender组件");
            return;
        }
    }
    protected virtual void Hurt(params object[] args)
    {
        Anim.Play(Consts.CHARACTER_ANM_IDLE);
        AudioMgr.Instance.PlayOnce(_brokenClip);
        transform.DOKill();
        transform.DOShakePosition(_data.shakeTime,_data.shakeForce,_data.vibrato,_data.randomness);
    }
    protected virtual void Dead(params object[] args)
    {
        Anim.Play(Consts.CHARACTER_ANM_DEAD);
        AudioMgr.Instance.PlayOnce(_deadAudio);
    }
    //死亡动画播放完毕
    protected virtual void DeadAnimOver()
    {
        CreateDeadSprite();
        Destroy(gameObject);
    }
    //产生遗骸
    protected void CreateDeadSprite()
    {
        var deadGo = new GameObject(transform.name+Consts.NAME_DESTRUCTIBLEITEM_DEAD);
        var render = deadGo.AddComponent<SpriteRenderer>();
        render.sprite = SpriteRender.sprite;
        render.sortingLayerID = SpriteRender.sortingLayerID;
        render.sortingOrder = SpriteRender.sortingOrder;
        render.color = SpriteRender.color;
        deadGo.transform.position = transform.position;
        deadGo.transform.rotation = transform.rotation;
        CreateDeadGoods();
    }
    //生成死亡物品
    protected virtual void CreateDeadGoods()
    {


    }
    protected virtual void InitDeadAudio()
    {
        var strs = transform.name.Split('_');
        var name = strs[1];
        name = name[0].ToString().ToLower() + name.Substring(1, name.Length - 1);
        foreach (var item in _audioClips)
        {
            if (item.Contains(name))
            {
                _deadAudio = item;
                break;
            }
        }
        if (_deadAudio == "")
            Debug.LogError($"名为{this.name}的可摧毁物体没有找到合适的死亡音效");
    }
}
