using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AClimbEnterStart : AnimeCall {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IClimb>().ReadyClimb();
    }
}
