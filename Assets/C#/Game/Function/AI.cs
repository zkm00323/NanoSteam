using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour{
    public static bool ClimbToTarget(ref Entity.CtrlInput input, Climber self, Walker target, float safeDis) {
        int selfFloor = GlobalVar.Mask.GetFloor(self.gameObject.layer);
        int targetFloor = GlobalVar.Mask.GetFloor(target.gameObject.layer);

        Vector2 selfPos = self.transform.position;
        Vector2? targetPos = target.transform.position;
        GameCtrl.Ladder? TargetLadder = null;

        if (target.GetState() == GlobalVar.State.Climb) return false;
        if(selfFloor == targetFloor){
            if((selfPos - (Vector2) targetPos).magnitude < safeDis) targetPos = null;
        }else{
            bool upDown = selfFloor < targetFloor;
            if(self.OnLadder){
                GameCtrl.Ladder ladder = self.Ladder;
                if(self.GetState() == GlobalVar.State.Climb)
                    input.UpDnAxis = selfPos.y < (upDown ? ladder.UpPos.position.y : ladder.DownPos.position.y) ? 1 : -1;
                else
                    targetPos = upDown ? ladder.UpPos.position : ladder.DownPos.position;
            }else{
                
                if(target.OnLadder && Mathf.Abs(selfFloor - targetFloor) == 1){
                    if((selfPos - (Vector2) targetPos).magnitude < safeDis)
                        targetPos = null;
                    else
                        TargetLadder = target.Ladder;
                }else{
                    foreach(GameCtrl.Ladder ladder in GameCtrl.LadderList){
                        if(upDown ? ladder.DownFloor == selfFloor : ladder.UpFloor == selfFloor){
                            if(TargetLadder == null ||
                               Mathf.Abs(selfPos.x - (upDown ? ladder.DownPos.position.x : ladder.UpPos.position.x)) <
                               Mathf.Abs(selfPos.x - (upDown
                                                 ? ((GameCtrl.Ladder) TargetLadder).DownPos.position.x
                                                 : ((GameCtrl.Ladder) TargetLadder).UpPos.position.x))){
                                TargetLadder = ladder;
                            }
                        }
                    }
                }

                if(TargetLadder != null){
                   targetPos = upDown
                    ? ((GameCtrl.Ladder) TargetLadder).DownPos.position
                    : ((GameCtrl.Ladder) TargetLadder).UpPos.position;
                    if(((Vector2)targetPos - selfPos).magnitude < 0.1f) input.UpDnAxis = upDown ? 1 : -1;
                }
            }
        }

        if(targetPos != null){
            input.MoveAxis = (TargetLadder != null&&((GameCtrl.Ladder)TargetLadder).AngleType==0?Mathf.Clamp(Mathf.Abs(selfPos.x - ((Vector2)targetPos).x)*0.3f, 0.25f, 1):1) * (selfPos.x < ((Vector2)targetPos).x ? 1 : -1);
            return true;
        }
        return false;
    }

    public static bool MoveToTarget(ref Entity.CtrlInput input, Walker self, Walker target,float safeDis) {
        Vector2 selfPos = self.transform.position;
        Vector2 targetPos = target.transform.position;

        int selfFloor = GlobalVar.Mask.GetFloor(self.gameObject.layer);
        int targetFloor = GlobalVar.Mask.GetFloor(target.gameObject.layer);

        if(selfFloor == targetFloor&&Mathf.Abs(selfPos.x-targetPos.x)>safeDis) {
            input.MoveAxis = selfPos.x < targetPos.x ? 1 : -1;
            return true;
        }
        return false;
    }

    public static bool FaceToTarget(ref Entity.CtrlInput input, Walker self, Walker target){
        if(target.transform.position.x - self.transform.position.x < 0 == self.GetSide()){
            input.MoveAxis = self.GetSide() ? -1 : 1;
            return true;
        }
        return false;
    }


}
