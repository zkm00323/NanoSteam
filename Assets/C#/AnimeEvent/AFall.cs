using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFall : AnimeCall {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        GetMain(animator).GetComponent<IMove>().StartFall();
    }
}
