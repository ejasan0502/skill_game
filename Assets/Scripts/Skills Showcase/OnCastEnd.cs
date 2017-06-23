using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Notify EffectObj that animation is done
public class OnCastEnd : StateMachineBehaviour {

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        animator.GetComponent<EffectObj>().Trigger();
    }

}