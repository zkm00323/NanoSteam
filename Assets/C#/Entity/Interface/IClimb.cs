using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClimb{
    void ReadyClimb();
    void StartClimb();
    void EndClimb();
    void FinishClimb();
    void LadderMove(GameCtrl.Ladder ladder, bool upDown);
}
