using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurArcherAI : OurArcher{
    protected override void SetTask(ref List<Task> taskList) {
        taskList.Add(new TFaceToTarget(this));
        taskList.Add(new TClimbToTarget(this, GlobalVar.MobValue.Our.Archer.SafeRange));

    }
    protected override void ClimberUpDate(){
    }

    
}
