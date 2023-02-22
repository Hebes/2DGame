/****************************************************
    文件：BeginnerGuidance.cs
 作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2022/1/13 0:21:54
 功能：新手引导
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BeginnerGuidance : MonoBehaviour
{
    private List<TextMesh> _txtMeshList = new List<TextMesh>();
    private void Awake()
    {
        InitTextMeshList();
        Init();
        EventMgr.Instance.AddEvent(E_EventName.BEGINNERGUIDANCE_RFRESH,Init);
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.BEGINNERGUIDANCE_RFRESH, Init);
    }
    private void InitTextMeshList()
    {
        _txtMeshList = GetComponentsInChildren<TextMesh>().ToList();
    }
    private void Init(params object[] args)
    {
        List<string> _txtGuidanceTips = new List<string>()
       {
        "左右移动:"+$" {InputMgr.Instance.GetAxisNegKey(Consts.HORIZONTALAXIS)} {InputMgr.Instance.GetAxisPosKey(Consts.HORIZONTALAXIS)}".SetColor(E_TextColor.Green),

        "攻击:"+$" {InputMgr.Instance.GetKeyCode(Consts.ATTACKEY)}\n".SetColor(E_TextColor.Red)
        +"连续攻击:"+$" 连按 {InputMgr.Instance.GetKeyCode(Consts.ATTACKEY).ToString().SetColor(E_TextColor.Red)}",

        "跳跃:"+$" {InputMgr.Instance.GetKeyCode(Consts.JUMPKEY)}".SetColor(E_TextColor.Green),

        "交互:"+$" {InputMgr.Instance.GetAxisPosKey(Consts.VERTICALAXIS)}".SetColor(E_TextColor.Blue),

        "冲刺:"+$" {InputMgr.Instance.GetKeyCode(Consts.DASHKEY)}".SetColor(E_TextColor.Green),


        "攀爬:"+$" 靠近墙壁按住 {InputMgr.Instance.GetKeyCode(Consts.GRABKEY)}\n".SetColor(E_TextColor.Green)+"上下移动(在攀爬过程中):"
        +$" {InputMgr.Instance.GetAxisPosKey(Consts.VERTICALAXIS)} {InputMgr.Instance.GetAxisNegKey(Consts.VERTICALAXIS)}".SetColor(E_TextColor.Green),


        "攀登:"+$" 悬挂在墙的边缘,\n攀登或者落下:"
        +$" {InputMgr.Instance.GetAxisPosKey(Consts.HORIZONTALAXIS)} {InputMgr.Instance.GetAxisNegKey(Consts.VERTICALAXIS)}".SetColor(E_TextColor.Green),


        "快速收集金币:"+$" 按住 {InputMgr.Instance.GetKeyCode(Consts.ATTRACTCOIN).ToString().SetColor(E_TextColor.Blue)}",

        "远程攻击:"+$" {InputMgr.Instance.GetKeyCode(Consts.BOWKEY).ToString().SetColor(E_TextColor.Red)}",

        "匍匐:"+$" 按住 {InputMgr.Instance.GetAxisNegKey(Consts.VERTICALAXIS).ToString().SetColor(E_TextColor.Green)}",

        "使用道具:"+$" 装备好道具后按 {InputMgr.Instance.GetKeyCode(Consts.USEITEM).ToString().SetColor(E_TextColor.Blue)}\n"
        +"切换道具:"+$"{InputMgr.Instance.GetKeyCode(Consts.CHANGEITEM).ToString().SetColor(E_TextColor.Blue)}"
       };
        if (_txtGuidanceTips.Count != _txtMeshList.Count)
        {
            Debug.LogError($"新手引导信息条数:{_txtGuidanceTips.Count}和TextMesh个数:{_txtMeshList.Count}不匹配");
            return;
        }
        for (int i = 0; i < _txtMeshList.Count; i++)
        {
            var mesh = _txtMeshList[i];
            mesh.richText = true;
            mesh.text = _txtGuidanceTips[i];
        }
    }
}