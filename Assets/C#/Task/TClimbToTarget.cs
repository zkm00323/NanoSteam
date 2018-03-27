using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TClimbToTarget : Task {
    protected new Climber Self;
    protected List<Entity> TargetTpye;
    protected float SafeDis;

    public TClimbToTarget(Climber self,List<Entity>targetTpye, float safeDis) : base(self, "T_ClimbToTarget"){
        Self = self;
        SafeDis = safeDis;
        TargetTpye = targetTpye;
    }
    public override void JustDo(ref Entity.CtrlInput input) {}

    public override bool WhileDo(ref Entity.CtrlInput input){
        return UpdateDo(ref input);
    }

    public override void StartDo(ref Entity.CtrlInput input) {}

    public override bool UpdateDo(ref Entity.CtrlInput input){
        if(Self.Target == null || Self.Target.GetState() == GlobalVar.State.Dead) return false;
        int selfFloor = GlobalVar.Mask.GetFloor(Self.gameObject.layer);
        int targetFloor = GlobalVar.Mask.GetFloor(Self.Target.gameObject.layer);

        Vector2 selfPos = Self.transform.position;
        Vector2? targetPos = Self.Target.transform.position;
        GameCtrl.Ladder? TargetLadder = null;

        if (Self.Target.GetState() == GlobalVar.State.Climb) return false;
        if (selfFloor == targetFloor) {
            if ((selfPos - (Vector2)targetPos).magnitude < SafeDis) targetPos = null;
        } else {
            bool upDown = selfFloor < targetFloor;
            if (Self.OnLadder) {
                GameCtrl.Ladder ladder = Self.Ladder;
                if (Self.GetState() == GlobalVar.State.Climb)
                    input.UpDnAxis = selfPos.y < (upDown ? ladder.UpPos.position.y : ladder.DownPos.position.y) ? 1 : -1;
                else
                    targetPos = upDown ? ladder.UpPos.position : ladder.DownPos.position;
            } else if (Self.Target.GetType() == typeof(Walker)) {
                Walker target = Self.Target.GetComponent<Walker>();
                if (target.OnLadder && Mathf.Abs(selfFloor - targetFloor) == 1) {
                    if ((selfPos - (Vector2)targetPos).magnitude < SafeDis)
                        targetPos = null;
                    else
                        TargetLadder = target.Ladder;
                } else {
                    foreach (GameCtrl.Ladder ladder in GameCtrl.LadderList) {
                        if (upDown ? ladder.DownFloor == selfFloor : ladder.UpFloor == selfFloor) {
                            if (TargetLadder == null ||
                               Mathf.Abs(selfPos.x - (upDown ? ladder.DownPos.position.x : ladder.UpPos.position.x)) <
                               Mathf.Abs(selfPos.x - (upDown
                                                 ? ((GameCtrl.Ladder)TargetLadder).DownPos.position.x
                                                 : ((GameCtrl.Ladder)TargetLadder).UpPos.position.x))) {
                                TargetLadder = ladder;
                            }
                        }
                    }
                }

                if (TargetLadder != null) {
                    targetPos = upDown
                     ? ((GameCtrl.Ladder)TargetLadder).DownPos.position
                     : ((GameCtrl.Ladder)TargetLadder).UpPos.position;
                    if (((Vector2)targetPos - selfPos).magnitude < 0.1f) input.UpDnAxis = upDown ? 1 : -1;
                }
            }
        }

        if (targetPos != null) {
            input.MoveAxis = (TargetLadder != null && ((GameCtrl.Ladder)TargetLadder).AngleType == 0 ? Mathf.Clamp(Mathf.Abs(selfPos.x - ((Vector2)targetPos).x) * 0.3f, 0.25f, 1) : 1) * (selfPos.x < ((Vector2)targetPos).x ? 1 : -1);
            return true;
        }
        return false;
    }

    public override void OnFinish(ref Entity.CtrlInput input) {}
}
