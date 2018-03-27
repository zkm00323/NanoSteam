using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShieldStart : AnimeCall {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IShield>().StartShield();
    }
}
