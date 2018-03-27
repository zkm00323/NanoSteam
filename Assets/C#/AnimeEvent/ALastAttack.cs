using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALastAttack : AAttack {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IAttack>().FinishAttack();
    }
}
