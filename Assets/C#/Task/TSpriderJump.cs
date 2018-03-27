using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSpriderJump : Task {
    protected new OurSprider Self;
    protected List<Entity> TargetTpye;

    public TSpriderJump(OurSprider self, List<Entity>targetTpye) : base(self, "T_SpriderJump"){
        Self = self;
        TargetTpye = targetTpye;
    }

    public override void JustDo(ref Entity.CtrlInput input){
    }

    public override bool WhileDo(ref Entity.CtrlInput input){
        return UpdateDo(ref input);
    }

    public override void StartDo(ref Entity.CtrlInput input){
    }

    public override bool UpdateDo(ref Entity.CtrlInput input){
        if (Self.Target.GetState() == GlobalVar.State.Climb && !Self.isBinding() && Self.IsOnGround()) {
            Vector2 jumpVec = Self.JumpToPos(GameCtrl.PlayerCtrl.GetCenterPos());
            if (jumpVec != Vector2.zero) {
                input.JumpButton = true;
                return true;
            }
        }
        return false;
    }

    public override void OnFinish(ref Entity.CtrlInput input){
    }
}
