using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARidicule : AnimeCall {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IRidicule>().StartRidicule();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IRidicule>().FinishRidicule();
    }
}
