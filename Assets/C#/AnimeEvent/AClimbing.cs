using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AClimbing : AnimeCall {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IClimb>().StartClimb();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IClimb>().EndClimb();
    }
}
