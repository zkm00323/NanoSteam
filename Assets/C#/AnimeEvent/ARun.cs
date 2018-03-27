using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARun : AnimeCall {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IHumanMove>().StartRun();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IHumanMove>().FinishRun();
    }
}
