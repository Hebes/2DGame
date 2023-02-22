using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInputHandle : MonoBehaviour
{
    private float _holdTime = 0.2f;
    public float _lastJumpDownTime { get; private set; }
    public float _lastAttackTime { get; private set; }
    public int XInput => _canControl ? (int)InputMgr.Instance.GetAxisRaw(Consts.HORIZONTALAXIS) : 0;
    public int YInput => _canControl ? (int)InputMgr.Instance.GetAxisRaw(Consts.VERTICALAXIS) : 0;
    public bool GrabInput => _canControl ? InputMgr.Instance.GetKey(Consts.GRABKEY) : false;
    public bool DashInput => _canControl ? InputMgr.Instance.GetKeyDown(Consts.DASHKEY) : false;
    public bool BowInput => _canControl ? InputMgr.Instance.GetKeyDown(Consts.BOWKEY) : false;
    public bool AttractInput => _canControl ? InputMgr.Instance.GetKey(Consts.ATTRACTCOIN) : false;
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool AttackInput { get; private set; }
    private bool _canControl = true;//是否可以控制玩家
    private void Awake()
    {
        InputMgr.Instance.Init();
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_CANTCTROL, SetCanControl);
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_CANTCTROL, SetCanControl);
    }
    private void Update()
    {
        CheckKeyInput();
        HoldJumpInput();
        HoldAttackInoput();
    }
    private void SetCanControl(params object[] args)
    {
        bool value = (bool)args[0];
        _canControl = value;
    }
    private void CheckKeyInput()
    {
        if (!_canControl)
            return;
        if (InputMgr.Instance.GetKeyDown(Consts.ATTACKEY))
        {
            _lastAttackTime = Time.time;
            AttackInput = true;
        }
        else if (InputMgr.Instance.GetKeyUp(Consts.ATTACKEY))
            AttackInput = false;
        if (InputMgr.Instance.GetKeyDown(Consts.JUMPKEY))
        {
            _lastJumpDownTime = Time.time;
            JumpInput = true;
            JumpInputStop = false;
        }
        else if (InputMgr.Instance.GetKeyUp(Consts.JUMPKEY))
        {
            JumpInput = false;
            JumpInputStop = true;
        }
    }
    private void HoldJumpInput()
    {
        if (JumpInput && Time.time >= _lastJumpDownTime + _holdTime)
            JumpInput = false;
    }
    private void HoldAttackInoput()
    {
        if (AttackInput && Time.time >= _lastAttackTime + _holdTime)
            AttackInput = false;
    }
    public void SetJumpInputStop(bool value = false) => JumpInputStop = value;
    //在所有跳跃的地方都需要添加 不然 可能出现切换状态依然天月明路
    public void SetJumpInput(bool value = false) => JumpInput = value;
    public void SetAttackInput(bool value = false) => AttackInput = value;
}
