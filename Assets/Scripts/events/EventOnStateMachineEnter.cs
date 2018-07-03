using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// See https://unity3d.com/learn/tutorials/modules/beginner/5-pre-order-beta/state-machine-behaviours
/// </summary>
public class EventOnStateMachineEnter : StateMachineBehaviour
{
    public UnityEvent notifyTarget;


    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        notifyTarget.Invoke();
    }


}