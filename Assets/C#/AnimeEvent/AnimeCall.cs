using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeCall : StateMachineBehaviour {
    protected GameObject GetMain(Animator animator){
        return animator.gameObject;
    }
}
