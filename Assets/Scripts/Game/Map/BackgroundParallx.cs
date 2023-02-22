/****************************************************
    文件：BackgroundParallx.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/9 19:7:52
	功能：地图视觉差生成脚本
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundParallx : MonoBehaviour, IInit
{
    private List<Transform> _backgrounds;
    private float _parallxScale = 5f;
    public float _parallReductionFactor = 0.1f;
    public float _smoothing=1f;
    private Transform _cam;//当前相机的位置
    private Vector3 _previousCamPos;//相机前一帧中的位置
    public void Init()
    {
        _cam = Camera.main.transform;
        InitTrans();
    }
    private void InitTrans()
    {
        _backgrounds = new List<Transform>();
        foreach (Transform trans in transform)
            if (trans.CompareTag(Tags.BackgroundParallx))
                _backgrounds.Add(trans);
        _previousCamPos = _cam.position;
    }
    private void Update()
    {
        if (_backgrounds == null || _backgrounds.Count == 0)
            return;
        ParallX();
    }
    private void ParallX()
    {
        if (_cam == null)
            return;
        //计算视差值
        float parallax = (_previousCamPos.x - _cam.position.x) * _parallxScale;
        //处理每一层背景的移动
        for (int i = 0; i < _backgrounds.Count; i++)
        {
            //定义一个背景图目标x坐标值 计算视差移动前后背景图新的位置
            float backgroundTargetPosX = _backgrounds[i].position.x + parallax * (i * _parallReductionFactor + 1);
            //定义三维背景图目标坐标值  放入新计算出的x坐标 y 和 z坐标不变
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, _backgrounds[i].position.y, _backgrounds[i].position.z);
            //在一帧背景图坐标 与 视差偏移坐标间获取 是差值为背景图新位置坐标
            _backgrounds[i].position = Vector3.Lerp(_backgrounds[i].position, backgroundTargetPos, _smoothing * Time.deltaTime);
        }
        _previousCamPos = _cam.position;
    }
}