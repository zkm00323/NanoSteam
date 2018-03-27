using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdle : AnimeCall {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IIdle>().StartIdle();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IIdle>().FinishIdle();
    }
}
