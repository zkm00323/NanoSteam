using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurSpriderAI : OurSprider {
    protected override void SetTask(ref List<Task> taskList){
        taskList.Add(new TFaceToTarget(this));
        taskList.Add(new TSpriderJump(this));
        taskList.Add(new TWalkToTarget(this, GlobalVar.MobValue.Our.Sprider.SafeRange));
    }

    protected override CtrlInput Input(){
        Entity.CtrlInput input = new Entity.CtrlInput();
        if (JumpOrNot(ref input)
//            ||
//            AI.MoveToTarget(ref input, this, GameCtrl.PlayerCtrl, GlobalVar.MobValue.Our.Sprider.SafeRange)
//            ||
//            AI.FaceToTarget(ref input, this, GameCtrl.PlayerCtrl)
        ) { }
        return input;
    }

    protected override void OurSpriderUpDate(){
    }

    private bool JumpOrNot(ref Entity.CtrlInput input) {
        if(GameCtrl.PlayerCtrl.GetState() == GlobalVar.State.Climb&&!Binding&&OnGround){
            Vector2 jumpVec = JumpToPos(GameCtrl.PlayerCtrl.GetCenterPos());
            if(jumpVec != Vector2.zero){
                input.JumpButton = true;
                return true;
            }
        }
        return false;
    }
}


