using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADodge : AnimeCall {
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IDodge>().StartDodge();
    }
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IDodge>().FinishDodge();
    }
}
