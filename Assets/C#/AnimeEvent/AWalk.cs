using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : AnimeCall {
    
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IMove>().StartWalk();
	}
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IMove>().FinishWalk();
    }
}
