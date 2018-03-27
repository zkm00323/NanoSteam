using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFaceToTarget : Task {
    protected new Walker Self;
    protected List<Entity> TargetTpye;

    public TFaceToTarget(Walker self, List<Entity>targetTpye) : base(self, "T_FaceToTarget"){
        Self = self;
        TargetTpye = targetTpye;
    }
    public override void JustDo(ref Entity.CtrlInput input){
    }

    public override bool WhileDo(ref Entity.CtrlInput input){
        return Self.Target!=null&&Self.Target.transform.position.x - Self.transform.position.x < 0 == Self.GetSide();
    }

    public override void StartDo(ref Entity.CtrlInput input){
        input.MoveAxis = Self.GetSide() ? -1 : 1;
    }

    public override bool UpdateDo(ref Entity.CtrlInput input){
        return false;
    }

    public override void OnFinish(ref Entity.CtrlInput input){
    }
}
