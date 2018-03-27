using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShieldFinish : AnimeCall {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IShield>().FinishShield();
    }
}
