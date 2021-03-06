﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AClimbFinish : AnimeCall {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetMain(animator).GetComponent<IClimb>().FinishClimb();
    }
}
