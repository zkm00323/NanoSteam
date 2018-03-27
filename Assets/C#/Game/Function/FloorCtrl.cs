using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCtrl : MonoBehaviour{
    public GameCtrl.Ladder Ladder;
    public bool UpDown;
    protected virtual void OnTriggerStay2D(Collider2D col) {
        if(Ladder.AngleType==0){
            IClimb mob = col.gameObject.GetComponent<IClimb>();
            if(mob != null) mob.LadderMove(Ladder, UpDown);
            
        } else{
            IMove mob = col.gameObject.GetComponent<IMove>();
            if (mob != null) mob.SlapeMove(Ladder, UpDown);
        }

    }
}
