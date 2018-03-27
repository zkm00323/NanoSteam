using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATurn : AnimeCall {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IMove>().StartTurn();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IMove>().FinishTurn();
    }
}
