using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtrl:MonoBehaviour{
    protected int MyFloor;

    protected virtual void Awake(){
        MyFloor = GlobalVar.Mask.GetFloor(gameObject.layer);
    }
    protected virtual void OnCollisionEnter2D(Collision2D col){
        Walker walker = col.gameObject.GetComponent<Walker>();
        if (walker != null) {
            walker.ChangeLayer(MyFloor);
            walker.OnLadder = false;
            walker.SetOnGround();
        }
    }
}
