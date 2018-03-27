using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove{
    void MoveCtrl();
    void StartWalk();
    void FinishWalk();

    void StartTurn();
    void FinishTurn();

    void JumpCtrl();
    void StartJump();
    void StartFall();
    void FinishJump();

    void SlapeMove(GameCtrl.Ladder ladder, bool upDown);
    void SetOnGround();
}
