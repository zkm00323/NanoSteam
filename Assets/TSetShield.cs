using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSetShield : Task {
    public TSetShield(Mob self):base(self,"T_SetShield"){}
    public override void JustDo(ref Entity.CtrlInput input){
        
    }
    public override bool WhileDo(ref Entity.CtrlInput input){
        return false;
    }
    public override void StartDo(ref Entity.CtrlInput input){

    }
    public override bool UpdateDo(ref Entity.CtrlInput input){
        return false;
    }
    public override void OnFinish(ref Entity.CtrlInput input){

    }
}
