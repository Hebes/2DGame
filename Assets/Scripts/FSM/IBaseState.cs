using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBaseState 
{
    void Enter();//进入该状态机需要执行的方法
    void Exit();//离开该状态机需要执行的方法
    void Check();//检测函数 
    void ActionUpdate();//行为帧函数
    void ReasonUpdate();//离开原因帧函数
    void PhysicsUpdate();//物理更新帧函数
    void AnimatorEnterTrigger();
    void AnimatorFinishTrigger();
}
