using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTargetTask : Task {
    protected List<Entity> TargetTpye;

    public TTargetTask(Mob self, List<Entity> targetTpye, string tasknameIn) : base(self, tasknameIn){ }
    public override void JustDo(ref Entity.CtrlInput input){
    }

    public override bool WhileDo(ref Entity.CtrlInput input){
        throw new System.NotImplementedException();
    }

    public override void StartDo(ref Entity.CtrlInput input){
        throw new System.NotImplementedException();
    }

    public override bool UpdateDo(ref Entity.CtrlInput input){
        if (Self.Target == null) { }
        foreach (Entity type in TargetTpye) {
            if (Self.Target.GetType() == type.GetType()) {
                ctn = true;
                break;
            }
        }

    }

    public override void OnFinish(ref Entity.CtrlInput input){
        throw new System.NotImplementedException();
    }
}
