/****************************************************
    文件：DecorationCreator.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 21:22:7
	功能：开始场景中装饰物品生成器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DecorationCreator : MonoBehaviour, IInit
{
    private List<GameObject> _decorationList;
    private int _index;
    private Transform _curDecorationTf;
    private float _curDecorationMoveSpeed=-10f;//当前在屏幕中的物体的移动速度
    protected float _createOffset=2f;
    public void Init()
    {
        _decorationList = new List<GameObject>();
        _index = 0;
        InitDecorationGo();
        CreateDecorationGo();
    }
    public void UpdateDecorationMove(float ratio=1)
    {
        CurDecorationMove(ratio);
        if(CheckIsChangeDecorationGo())
            CreateDecorationGo();
    }
    private void CurDecorationMove(float ratio)
        => _curDecorationTf?
        .Translate(new Vector3(_curDecorationMoveSpeed * Time.deltaTime*ratio, 0, 0),Space.World);
    private void InitDecorationGo()
    {
        var prefabs = ResMgr.Instance.LoadAllRes<GameObject>(Paths.PREFAB_MAPDECORAION_FLODER);
        GameObject prefabGo=null;
        foreach (var prefab in prefabs)
        {
            prefabGo = Instantiate(prefab);
            prefabGo.transform.SetParent(transform);
            prefabGo.SetActive(false);
            _decorationList.Add(prefabGo);
        }
    }
    private bool CheckIsChangeDecorationGo()
    {
        var screeMinX = ScreenPosUtil.GetScreenMinPos().x;
        var playerPosX = _curDecorationTf.transform.position.x;
        if (playerPosX<=screeMinX-_createOffset)
        {
            _curDecorationTf.SetActive(false);
            return true;
        }
        return false;
    }
    //创建当前的装饰物品
    private void CreateDecorationGo()
    {
        var screeMaxX = ScreenPosUtil.GetScreenMaxPos().x;
        var screenMidY = ScreenPosUtil.GetScreenCenterPos().y;
        var groundPos = GroundPosUtil.GetTargetGroundPos(new Vector2(screeMaxX, screenMidY));
        _curDecorationTf = _decorationList[_index].transform;
        _curDecorationTf.SetActive(true);
        groundPos.x += _createOffset;
        _curDecorationTf.position = groundPos;
        _index++;
        if (_index >= _decorationList.Count)
            _index = 0;
    }
}