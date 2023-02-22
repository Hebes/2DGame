/****************************************************
    文件：NPCData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/18 21:8:49
	功能：NPC数据类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
[CreateAssetMenu(fileName = "newNPCData", menuName = "数据/NPC/单个NPC数据相关")]
public class NPCData : ScriptableObject
{
    public int id;
    [HorizontalGroup("NPC", 75, LabelWidth = 50), HideLabel, PreviewField(75), DisableIf("@sprite!=null")]
    public Sprite sprite;
    [VerticalGroup("NPC/NPCField"), LabelText("名字")]
    public new string name;
    [LabelText("对话数据")]
    [VerticalGroup("NPC/NPCField"),ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<ChatModel> dialogModels;
}
//单个对话选项
[Serializable]
public class ChatModel
{
    [Required(ErrorMessage = "NPC必须说点什么")]
    [HideLabel, MultiLineProperty(4)]
    public string npcContent;
    [LabelText("NPC事件")]
    public List<ChatEventModel> dialogEventModels;
    [LabelText("玩家选择")]
    public List<ChatPlayerSelect> dialogPlayerSelects;
}
//对话事件枚举
public enum E_ChatEvent
{
    [LabelText("下一条对话")]
    NextChat,
    [LabelText("退出对话")]
    ExitChat,
    [LabelText("跳转对话")]
    JumpChat,
    [LabelText("打开商店")]
    StartShop,
    [LabelText("显示对话框")]
    ShowDialog,
}
//对话事件数据
[Serializable]
public class ChatEventModel
{
    [HideLabel, HorizontalGroup("事件", MaxWidth = 100)]
    public E_ChatEvent chatEvent;
    [HideLabel, HorizontalGroup("事件")]
    public string args;//对应的事件参数
}
//玩家选项
[Serializable]
public class ChatPlayerSelect
{
    [LabelText("选项文字"), MultiLineProperty(2), LabelWidth(50)]
    public string selectionName;
    [LabelText("事件")]
    public List<ChatEventModel> dialogEventModels;
}
